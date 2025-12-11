using Android.Speech.Tts;

namespace Demauin.TextToSpeech;

public class TextToSpeechListener : UtteranceProgressListener
{
    private readonly TaskCompletionSource<object?> _taskCompletionSource = new();

    public Task Task => _taskCompletionSource.Task;

    public override void OnDone(string? utteranceId)
        => _taskCompletionSource.TrySetResult(null);

    [Obsolete("Use OnError(string?, int errorCode) instead. This method is no longer invoked on modern versions of Android.")]
    public override void OnError(string? utteranceId)
        => _taskCompletionSource.TrySetException(new TextToSpeechException("Android's text-to-speech engine reported an error.", utteranceId));

    public override void OnStart(string? utteranceId) { }
}