using System.Text;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;

namespace Alice_Novel;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();//ページの読み込み
    }

    private async void Game_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//GamePage");//画面遷移
    }

    private async void Develop_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//DevPage");//画面遷移

        //(DevelopPageへ移動処理)
        await PickFolder(CancellationToken.None);
        //await SaveFile(CancellationToken.None);

        static async Task PickFolder(CancellationToken cancellationToken)
        {
            var result = await FolderPicker.Default.PickAsync(cancellationToken);
            if (result.IsSuccessful)
            {
                //await Toast.Make($"フォルダーを確認しました。名称:{result.Folder.Name}, 場所:{result.Folder.Path}", ToastDuration.Long).Show(cancellationToken);
                string location = result.Folder.Path;
            }
            /*
            else
            {
                await Toast.Make($"予期せぬエラーが発生しました。エラー内容: {result.Exception.Message}").Show(cancellationToken);
            }
            */
        }
        /*
        async Task SaveFile(CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream(Encoding.Default.GetBytes("Hello from the Community Toolkit!"));
            var fileSaverResult = await FileSaver.Default.SaveAsync("test.txt", stream, cancellationToken);
            if (fileSaverResult.IsSuccessful)
            {
                await Toast.Make($"The file was saved successfully to location: {fileSaverResult.FilePath}").Show(cancellationToken);
            }
            else
            {
                await Toast.Make($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}").Show(cancellationToken);
            }
        }
        */
    }

    private async void Setting_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//SettingPage");//画面遷移
    }
}
