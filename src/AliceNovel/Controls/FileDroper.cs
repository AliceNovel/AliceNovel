#if WINDOWS
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
#elif IOS || MACCATALYST
using UIKit;
using Foundation;
using System.Diagnostics;
#endif

namespace AliceNovel.Controls;

internal class FileDroper
{
    public static async Task<string> DropAsync(DropEventArgs e)
    {
        var filePaths = new List<string>();

        #if WINDOWS
        if (e.PlatformArgs is not null && e.PlatformArgs.DragEventArgs.DataView.Contains(StandardDataFormats.StorageItems))
        {
            var items = await e.PlatformArgs.DragEventArgs.DataView.GetStorageItemsAsync();
            if (items.Any())
            {
                foreach (var item in items)
                {
                    if (item is StorageFile file)
                        filePaths.Add(item.Path);
                }
            }
        }
        #elif IOS || MACCATALYST
        var session = e.PlatformArgs?.DropSession;
        if (session == null)
            return null;

        foreach (UIDragItem item in session.Items)
        {
            var result = await LoadItemAsync(item.ItemProvider, item.ItemProvider.RegisteredTypeIdentifiers.ToList());
            if (result is not null)
                filePaths.Add(result.FileUrl?.Path!);
        }
        foreach (var item in filePaths)
        {
            Debug.WriteLine($"Path: {item}");
        }

        static async Task<LoadInPlaceResult> LoadItemAsync(NSItemProvider itemProvider, List<string> typeIdentifiers)
        {
            if (typeIdentifiers is null || typeIdentifiers.Count == 0)
                return null;

            var typeIdent = typeIdentifiers.First();

            if (itemProvider.HasItemConformingTo(typeIdent))
                return await itemProvider.LoadInPlaceFileRepresentationAsync(typeIdent);

            typeIdentifiers.Remove(typeIdent);
            return await LoadItemAsync(itemProvider, typeIdentifiers);
        }
        #endif

        return filePaths.FirstOrDefault();
    }
}
