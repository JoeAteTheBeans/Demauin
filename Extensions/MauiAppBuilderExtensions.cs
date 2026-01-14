namespace Demauin.Extensions;

public static partial class MauiAppBuilderExtensions
{
    /// <summary>
    /// Initialises Demauin class library for use in a Maui app.
    /// </summary>
    /// <param name="builder">The .NET MAUI app builder to apply to.</param>
    /// <param name="options">Options for Demauin library use.</param>
    public static partial void UseDemauin(this MauiAppBuilder builder, DemauinOptions options);
}