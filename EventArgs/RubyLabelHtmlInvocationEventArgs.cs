namespace Demauin.EventArgs;

/// <summary>
/// Data for a method invocation from the WebView in a RubyLabel control.
/// </summary>
/// <param name="invocation">identifier of the method to be invoked</param>
/// <param name="data">data to be used by the invoked method</param>
public class RubyLabelHtmlInvocationEventArgs(string invocation, string data) : System.EventArgs
{
    /// <summary>
    /// Gets the identifier of the method to be invoked.
    /// </summary>
    public string Invocation { get; } = invocation;
    
    /// <summary>
    /// Gets the data to be used by the invoked method.
    /// </summary>
    public string Data { get; } = data;
}