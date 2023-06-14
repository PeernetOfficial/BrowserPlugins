using Peernet.Browser.Plugins.ImageViewer.ViewModels;
using Peernet.Browser.Plugins.ImageViewer.ViewModels.Parameters;
using Peernet.SDK.Client.Clients;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Peernet.Browser.Plugins.ImageViewer.Services
{
    public class ImageViewerService : IPlayButtonPlug
    {
        private readonly IFileClient fileClient;

        public ImageViewerService(IFileClient fileClient)
        {
            this.fileClient = fileClient;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            var viewModel = new ImageViewerViewModel();
            var stream = await fileClient.Read(Convert.ToHexString(file.Hash), Convert.ToHexString(file.NodeId));
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            await viewModel.Prepare(new ImageViewerViewModelParameter { ImageSource = bitmapImage, Title = file.Name });
            new ImageViewerWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Type is LowLevelFileType.Picture)
            {
                return true;
            }
             
            return false;
        }
    }
}