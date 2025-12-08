using Demauin.EventArgs;

namespace Demauin.Controls;

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
    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    
    public Color TextColour
    {
        get => (Color)GetValue(TextColourProperty);
        set => SetValue(TextColourProperty, value);
    }

    public double TextSize
    {
        get => (double)GetValue(TextSizeProperty);
        set => SetValue(TextSizeProperty, value);
    }

    public TextAlignment TextAlignment
    {
        get => (TextAlignment)GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    public Color RubyTextColour
    {
        get => (Color)GetValue(RubyTextColourProperty);
        set => SetValue(RubyTextColourProperty, value);
    }

    public double RubyTextSize
    {
        get => (double)GetValue(RubyTextSizeProperty);
        set => SetValue(RubyTextSizeProperty, value);
    }
    
    public string? Font
    {
        get => (string?)GetValue(FontProperty);
        set => SetValue(FontProperty, value);
    }

    public double LineHeight
    {
        get => (double)GetValue(LineHeightProperty);
        set => SetValue(LineHeightProperty, value);
    }

    public double CharacterSpacing
    {
        get => (double)GetValue(CharacterSpacingProperty);
        set => SetValue(CharacterSpacingProperty, value);
    }

    public double RubyMargin
    {
        get => (double)GetValue(RubyMarginProperty);
        set => SetValue(RubyMarginProperty, value);
    }
    
    //Event Handlers
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

    protected async void SetSize()
    {
        if (HorizontalOptions != LayoutOptions.Fill)
        {
            string? widthStr = await WebView.EvaluateJavaScriptAsync("document.getElementById(\"paragraph\").scrollWidth;");
            if (double.TryParse(widthStr, out double width))
                WebView.WidthRequest = width;
        }
    }
}