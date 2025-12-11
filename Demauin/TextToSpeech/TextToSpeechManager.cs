namespace Demauin.TextToSpeech;

public static partial class TextToSpeechManager
{
    public static partial Task SpeakAsync(string text, SpeechOptions? options = null, 
        CancellationToken cancelToken = default);
}