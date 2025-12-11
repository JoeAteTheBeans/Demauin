namespace Demauin.TextToSpeech;

public class TextToSpeechException(string message, string? utteranceId) : Exception(message)
{
    public string? UtteranceId { get; } = utteranceId;
}