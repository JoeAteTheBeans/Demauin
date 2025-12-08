using Demauin.EventArgs;

namespace Demauin.Controls;

/// <summary>
/// A control that displays text with optional ruby annotations.
/// </summary>
/// <remarks>
/// Uses a WebView to display the text.
/// </remarks>
public partial class RubyLabel
{
    //Bindable Properties
    public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: string.Empty);
    
    public static readonly BindableProperty TextColourProperty = BindableProperty.Create(nameof(TextColour), typeof(Color), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: Colors.White);

    public static readonly BindableProperty TextSizeProperty = BindableProperty.Create(nameof(TextSize), typeof(double), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: 18.0);
    
    public static readonly BindableProperty TextAlignmentProperty = BindableProperty.Create(nameof(TextAlignment), typeof(TextAlignment), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: TextAlignment.Start);
    
    public static readonly BindableProperty RubyTextColourProperty = BindableProperty.Create(nameof(RubyTextColour), typeof(Color), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: Colors.DarkGrey);
    
    public static readonly BindableProperty RubyTextSizeProperty = BindableProperty.Create(nameof(RubyTextSize), typeof(double), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: 12.0);
    
    public static readonly BindableProperty FontProperty = BindableProperty.Create(nameof(Font), typeof(string), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: null);
    
    public static readonly BindableProperty LineHeightProperty = BindableProperty.Create(nameof(LineHeight), typeof(double), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: 1.25);
    
    public static readonly BindableProperty CharacterSpacingProperty = BindableProperty.Create(nameof(CharacterSpacing), typeof(double), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: 0.0);
    
    public static readonly BindableProperty RubyMarginProperty = BindableProperty.Create(nameof(RubyMargin), typeof(double), typeof(RubyLabel), propertyChanged:OnAnyPropertyChanged, defaultValue: 2.5);
    
    //Properties
    
    /// <summary>
    /// Gets or sets the main text displayed by the control.
    /// </summary>
    /// <value>
    /// A <see cref="string"/> containing raw text or HTML markup text.
    /// Defaults to an empty string.
    /// </value>
    /// <remarks>
    /// Use the ruby and rt HTML tags to add ruby annotations.
    /// </remarks>
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the colour of the main text.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> value.
    /// Defaults to <see cref="Colors.White"/>.
    /// </value>
    public Color TextColour
    {
        get => (Color)GetValue(TextColourProperty);
        set => SetValue(TextColourProperty, value);
    }

    /// <summary>
    /// Gets or sets the font size of the main text in device-independent pixels.
    /// </summary>
    /// <value>
    /// A <see cref="double"/>.  
    /// Defaults to <c>18.0</c>.
    /// </value>
    public double TextSize
    {
        get => (double)GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }

    /// <summary>
    /// Gets or sets the horizontal text alignment of the rendered text.
    /// </summary>
    /// <value>
    /// A <see cref="TextAlignment"/> value.  
    /// Defaults to <see cref="TextAlignment.Start"/>.
    /// </value>
    public TextAlignment TextAlignment
    {
        get => (TextAlignment)GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    /// <summary>
    /// Gets or sets the colour of the ruby text.
    /// </summary>
    /// <value>
    /// A <see cref="Color"/> value.
    /// Defaults to <see cref="Colors.DarkGrey"/>
    /// </value>
    public Color RubyTextColour
    {
        get => (Color)GetValue(RubyTextColourProperty);
        set => SetValue(RubyTextColourProperty, value);
    }

    /// <summary>
    /// Gets or sets the size of the ruby text in device-independent units.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value.
    /// Defaults to <c>12.0</c>
    /// </value>
    public double RubyTextSize
    {
        get => (double)GetValue(RubyTextSizeProperty);
        set => SetValue(RubyTextSizeProperty, value);
    }
    
    /// <summary>
    /// Gets or sets the font family for the text and ruby text.  
    /// </summary>
    /// <value>
    /// A <see cref="string"/> representing the font family's file name in embedded resources.
    /// Defaults to <c>null</c>.
    /// </value>
    public string? Font
    {
        get => (string?)GetValue(FontProperty);
        set => SetValue(FontProperty, value);
    }

    /// <summary>
    /// Gets or sets the line height multiplier of the text.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value.
    /// Defaults to <c>1.2</c>
    /// </value>
    public double LineHeight
    {
        get => (double)GetValue(LineHeightProperty);
        set => SetValue(LineHeightProperty, value);
    }

    /// <summary>
    /// Gets or sets the spacing between characters, in device-independent units.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value.
    /// Defaults to <c>0.0</c>
    /// </value>
    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    /// <summary>
    /// Gets or sets the margin above ruby text.
    /// </summary>
    /// <value>
    /// A <see cref="double"/> value.
    /// Defaults to <c>2.5</c>
    /// </value>
    public double RubyMargin
    {
        get => (double)GetValue(RubyMarginProperty);
        set => SetValue(RubyMarginProperty, value);
    }
    
    //Event Handlers
    
    /// <summary>
    /// Occurs when the embedded WebView requests an invocation
    /// using the "<c>app://navfunc/invoke/{functionName}/{value}</c>" protocol.
    /// </summary>
    /// <remarks>
    /// Use this event to respond to JavaScript events inside the control.
    /// </remarks>
    public event EventHandler<RubyLabelHtmlInvocationEventArgs> HtmlInvocation = delegate { };
    
    //Constructors
    public RubyLabel()
    {
        InitializeComponent();
        Loaded += OnLoaded!;
        WebView.Navigating += OnNavigating!;
    }
    
    //OnPropertyChangedEvents
    private static void OnAnyPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RubyLabel rubyLabel)
            rubyLabel.RenderWebView();
    }
    
    //Methods
    protected virtual void OnLoaded(object sender, System.EventArgs e)
        => RenderWebView();

    /// <summary>
    /// Rebuilds the internal Webview, applying current property values,
    /// and loads the content into the embedded WebView.
    /// </summary>
    protected void RenderWebView()
    {
        WidthRequest = -1;
        HeightRequest = -1;
        WebView.Source = new HtmlWebViewSource {Html = $@"
<html lang='ja'>
    <head>
        <meta charset='utf-8'>
        <style type='text/css'>
            @font-face {{
                font-family: RubyLabelFont;
                src: url(""{Font}.ttf"");
            }}
            body {{
                font-family: 'RubyLabelFont', sans-serif;
                background-color: transparent;
                width: 100%;
                overflow: hidden;
                margin: 0;
                padding: 0;
            }}
            p {{
                font-size: {TextSize}px;
                color: {TextColour.ToHex()};
                letter-spacing: {CharacterSpacing}px;
                line-height: {LineHeight};
                text-align: {TextAlignment.ToString().ToLower()};
                display: block;
                white-space: normal;
                word-break: break-word;
                margin: 0;
                padding: 0;
                overflow: hidden;
            }}
            ruby rt {{
                font-size: {RubyTextSize}px;
                color: {RubyTextColour.ToHex()};
                margin-top: {RubyMargin}px;
            }}
        </style>
    </head>
    <body>
        <p id=""paragraph"">{Text}</p>
        <script>
            window.onload = () => {{
                window.location.href = ""app://navfunc/setSize"";
            }};

            func invoke(invocation) {{
                window.location.href = ""app://navfunc/invoke/"" + invocation;
            }}
        </script>
    </body>
</html>
        "};
    }

    /// <summary>
    /// Queries the rendered HTML to determine its required width
    /// and adjusts the WebView size if needed.
    /// </summary>
    /// <remarks>
    /// Only runs when <see cref="View.HorizontalOptions"/> is not set to <see cref="LayoutOptions.Fill"/>.
    /// </remarks>
    protected async void SetSize()
    {
        if (HorizontalOptions != LayoutOptions.Fill)
        {
            string? widthStr = await WebView.EvaluateJavaScriptAsync("document.getElementById(\"paragraph\").scrollWidth;");
            if (double.TryParse(widthStr, out double width))
                WebView.WidthRequest = width;
        }
    }
    
    private void OnNavigating(object sender, WebNavigatingEventArgs e)
    {
        if (e.Url.StartsWith("app://navfunc/"))
        {
            e.Cancel = true;
            string[] components = e.Url[14..].Split("/");
            switch (components[0])
            {
                case "setSize":
                    SetSize();
                    break;
                case "invoke":
                    HtmlInvocation.Invoke(this, new RubyLabelHtmlInvocationEventArgs(components[1], components[2]));
                    break;
            }
        }
    }
}