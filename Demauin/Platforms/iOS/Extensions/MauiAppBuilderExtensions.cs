using Demauin.TextToSpeech;
using Microsoft.Maui.LifecycleEvents;

namespace Demauin.Extensions;

public static partial class MauiAppBuilderExtensions
{
    public static partial void UseDemauin(this MauiAppBuilder builder, DemauinOptions options)
    {
        builder.ConfigureLifecycleEvents(events =>
        {
            if (options.InitialiseTextToSpeechManagerOnStartup)
            {
                events.AddiOS(ios =>
                {
                    ios.FinishedLaunching((_, _) =>
                    {
                        TextToSpeechManager.Initialise();
                        return true;
                    });
                });
            }
        });
    }
}