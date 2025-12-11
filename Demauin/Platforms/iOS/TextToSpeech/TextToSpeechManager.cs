namespace Demauin.TextToSpeech;

public static partial class TextToSpeechManager
{
    public static void Initialise()
        => Microsoft.Maui.Media.TextToSpeech.SpeakAsync("initialise", new SpeechOptions() { Volume = 0 });
    
    public static partial async Task SpeakAsync(string text, SpeechOptions? options, CancellationToken cancelToken)
        => await Microsoft.Maui.Media.TextToSpeech.SpeakAsync(text, options, cancelToken);
}