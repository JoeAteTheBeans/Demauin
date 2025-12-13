namespace Demauin.Controls.Extensions;

/// <summary>
/// Common methods for view feedback animations.
/// </summary>
public static class FeedbackAnimations
{
    public static async Task ScaleAsync(this View view, 
        double scaleFrom = 1.0, double scaleTo = 1.1, 
    public static async Task ScaleFeedbackAsync(this View view, 
        double? scaleFrom = null, double? scaleTo = null, 
        uint length = 100, Easing? easing = null,
        bool disableDuringAnimation = true)
    {
        scaleFrom ??= view.Scale;
        scaleTo ??= view.Scale + 0.1;
        easing ??= Easing.Linear;
        bool wasEnabled = view.IsEnabled;
        view.IsEnabled &= !disableDuringAnimation;
        await view.ScaleToAsync(scaleTo.Value, length / 2, easing);
        await view.ScaleToAsync(scaleFrom.Value, length / 2, easing);
        view.IsEnabled = wasEnabled;
    }

    public static async Task ShakeXAsync(this View view,
        double shakeFrom = 0, double shakeDistance = 10.0, uint shakeAmount = 2,
    public static async Task ShakeFeedbackAsync(this View view,
        double? shakeFrom = null, double shakeDistance = 10.0, uint shakeAmount = 2,
        uint shakeLength = 20, Easing? easing = null,
        bool disableDuringAnimation = true)
    {
        shakeFrom ??= view.TranslationX;
        easing ??= Easing.Linear;
        bool wasEnabled = view.IsEnabled;
        view.IsEnabled &= !disableDuringAnimation;
        while (shakeAmount > 0)
        {
            await view.TranslateToAsync(shakeFrom.Value + shakeDistance, view.TranslationY, shakeLength / 2, easing);
            await view.TranslateToAsync(shakeFrom.Value, view.TranslationY, shakeLength / 2, easing);
            if (--shakeAmount > 0)
            {
                await view.TranslateToAsync(shakeFrom - shakeDistance, 0, shakeLength / 2, easing);
                await view.TranslateToAsync(shakeFrom, 0, shakeLength / 2, easing);
                await view.TranslateToAsync(shakeFrom.Value - shakeDistance, view.TranslationY, shakeLength / 2, easing);
                await view.TranslateToAsync(shakeFrom.Value, view.TranslationY, shakeLength / 2, easing);
                shakeAmount--;
            }
        }
        view.IsEnabled = wasEnabled;
    }
}