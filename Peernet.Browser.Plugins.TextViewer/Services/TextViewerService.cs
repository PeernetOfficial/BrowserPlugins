using Peernet.Browser.Plugins.TextViewer.ViewModels;
using Peernet.Browser.Plugins.TextViewer.ViewModels.Parameters;
using Peernet.SDK.Client.Clients;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System.IO;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.TextViewer.Services
{
    public class TextViewerService : IPlayButtonPlug
    {
        private readonly IFileClient fileClient;

        public TextViewerService(IFileClient fileClient)
        {
            this.fileClient = fileClient;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            var viewModel = new TextViewerViewModel();
            using var stream = await fileClient.Read(file);
            var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            await viewModel.Prepare(new TextViewerViewModelParameter { Content = content, Title = file.Name });
            new TextViewerWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Type is LowLevelFileType.Text)
            {
                return true;
            }

            return false;
        }
    }
}