namespace Demauin.Controls.Extensions;

/// <summary>
/// Common methods for view feedback animations.
/// </summary>
public static class FeedbackAnimations
{
    /// <summary>
    /// Animates a view by scaling it to and from target values.
    /// </summary>
    /// <param name="view">The view to animate.</param>
    /// <param name="scaleFrom">The initial value to scale from (and return to). Defaults to the current size of the view if omitted or left <see langword="null"/>.</param>
    /// <param name="scaleTo">The target value to scale towards. Defaults to the current scale of the view plus <c>0.1</c> if omitted or left <see langword="null"/>.</param>
    /// <param name="length">The length of the animation in milliseconds. Defaults to <c>100</c> if omitted.</param>
    /// <param name="easing">The function used to ease the animation. Defaults to <see cref="Easing.Linear"/> if omitted or left <see langword="null"/>.</param>
    /// <param name="disableDuringAnimation">Whether the view is disabled while the animation is in progress. Defaults to <see langword="true"/> if omitted.</param>
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

    /// <summary>
    /// Animates a view by shaking it back and forth on the x-axis.
    /// </summary>
    /// <param name="view">The view to animate.</param>
    /// <param name="shakeFrom">The x-translation to shake from. Defaults to the current x-translation of the view if omitted or left <see langword="null"/>.</param>
    /// <param name="shakeDistance">The distance to shake by on either side. Defaults to <c>10.0</c>.</param>
    /// <param name="shakeAmount">The amount of times to shake on both sides. Defaults to <c>2</c>.</param>
    /// <param name="shakeLength">The length of a single shake in milliseconds. Defaults to <c>20</c>.</param>
    /// <param name="easing">The function used to ease the animation. Defaults to <see cref="Easing.Linear"/> if omitted or left <see langword="null"/>.</param>
    /// <param name="disableDuringAnimation">Whether the view is disabled while the animation is in progress. Defaults to <see langword="true"/> if omitted.</param>
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
                await view.TranslateToAsync(shakeFrom.Value - shakeDistance, view.TranslationY, shakeLength / 2, easing);
                await view.TranslateToAsync(shakeFrom.Value, view.TranslationY, shakeLength / 2, easing);
                shakeAmount--;
            }
        }
        view.IsEnabled = wasEnabled;
    }
}