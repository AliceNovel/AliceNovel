using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;

namespace Alice_Novel;

public partial class DevPage : ContentPage
{
	public DevPage()
	{
		InitializeComponent();
	}

    private async void File_Clicked(object sender, EventArgs e)
    {
        await PickFolder(CancellationToken.None);

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
    }
}