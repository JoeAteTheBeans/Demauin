namespace Demauin.EventArgs;

public class RubyLabelHtmlInvocationEventArgs(string invocation, string data) : System.EventArgs
{
    public string Invocation { get; } = invocation;
    public string Data { get; } = data;
}