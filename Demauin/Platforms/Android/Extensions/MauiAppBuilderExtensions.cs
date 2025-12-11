using Demauin.TextToSpeech;
using Microsoft.Maui.LifecycleEvents;

namespace Demauin.Extensions;

public static partial class MauiAppBuilderExtensions
{
    public static partial void UseDemauin(this MauiAppBuilder builder, DemauinOptions options)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
            events.AddAndroid(android => { android.OnCreate((_, _) => { TextToSpeechManager.Initialise(); }); });
        });
    }
}