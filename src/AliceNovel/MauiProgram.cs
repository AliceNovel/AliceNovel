using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;

namespace AliceNovel;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.Services.AddSingleton(FolderPicker.Default);
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("NotoSans-Regular.ttf", "NotoSansRegular");
                fonts.AddFont("NotoSans-SemiBold.ttf", "NotoSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
