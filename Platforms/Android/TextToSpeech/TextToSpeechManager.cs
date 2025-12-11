using Android.OS;
using Android.Speech.Tts;

namespace Demauin.TextToSpeech;

public static partial class TextToSpeechManager
{
    private static Android.Speech.Tts.TextToSpeech? _textToSpeech;

    public static void Initialise() 
        => _textToSpeech = new Android.Speech.Tts.TextToSpeech(Android.App.Application.Context, null);
    
    public static partial async Task SpeakAsync(string text, SpeechOptions? options, CancellationToken cancelToken)
    {
        if (_textToSpeech is null)
            Initialise();
        
        var utteranceId = Guid.NewGuid().ToString();
        var listener = new TextToSpeechListener();

        _textToSpeech!.SetOnUtteranceProgressListener(listener);

        if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            _textToSpeech.Speak(text, QueueMode.Flush, null, utteranceId);
        else
        {
#pragma warning disable CS0618
            _textToSpeech.Speak(text, QueueMode.Flush, new Dictionary<string, string>
            {
                { Android.Speech.Tts.TextToSpeech.Engine.KeyParamUtteranceId, utteranceId }
            });
#pragma warning restore CS0618
        }

        await listener.Task;
        cancelToken.ThrowIfCancellationRequested();
    }
}