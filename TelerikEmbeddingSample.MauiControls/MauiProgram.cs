using CommunityToolkit.Maui;
using Telerik.Maui.Controls.Compatibility;

namespace TelerikEmbeddingSample.MauiControls;

public static class MauiProgram
{
    public static void ConfigureMauiEmbedding(this MauiAppBuilder maui)
    {
        maui.Services.AddSingleton<IApplication>(sp =>
        {
            var app = new Application();
            app.Resources.MergedDictionaries.Add(new EmbeddedResources());
            return app;
        });
        maui.UseMauiCommunityToolkit()
            .UseTelerik();
    }
}
