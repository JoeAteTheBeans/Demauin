namespace Demauin.Controls.Extensions;

/// <summary>
/// Common methods for view feedback animations.
/// </summary>
public static class FeedbackAnimations
{
    public static async Task ScaleAsync(this View view, 
        double scaleFrom = 1.0, double scaleTo = 1.1, 
    public static async Task ScaleFeedbackAsync(this View view, 
        uint length = 100, Easing? easing = null,
        bool disableDuringAnimation = true)
    {
        easing ??= Easing.Linear;
        bool wasEnabled = view.IsEnabled;
        view.IsEnabled &= !disableDuringAnimation;
        await view.ScaleToAsync(scaleTo, length, easing);
        await view.ScaleToAsync(scaleFrom, length, easing);
        view.IsEnabled = wasEnabled;
    }

    public static async Task ShakeXAsync(this View view,
        double shakeFrom = 0, double shakeDistance = 10.0, uint shakeAmount = 2,
    public static async Task ShakeFeedbackAsync(this View view,
        uint shakeLength = 20, Easing? easing = null,
        bool disableDuringAnimation = true)
    {
        easing ??= Easing.Linear;
        bool wasEnabled = view.IsEnabled;
        view.IsEnabled &= !disableDuringAnimation;
        while (shakeAmount > 0)
        {
            await view.TranslateToAsync(shakeFrom + shakeDistance, 0, shakeLength / 2, easing);
            await view.TranslateToAsync(shakeFrom, 0, shakeLength / 2, easing);
            if (--shakeAmount > 0)
            {
                await view.TranslateToAsync(shakeFrom - shakeDistance, 0, shakeLength / 2, easing);
                await view.TranslateToAsync(shakeFrom, 0, shakeLength / 2, easing);
                shakeAmount--;
            }
        }
        view.IsEnabled = wasEnabled;
    }
}