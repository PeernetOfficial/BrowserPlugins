using Peernet.Browser.Plugins.WordViewer.ViewModels;
using Peernet.Browser.Plugins.WordViewer.ViewModels.Parameters;
using Peernet.SDK.Client.Clients;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.WordViewer.Services
{
    public class WordViewerService : IPlayButtonPlug
    {
        private readonly IFileClient fileClient;

        public WordViewerService(IFileClient fileClient)
        {
            this.fileClient = fileClient;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            var viewModel = new WordViewerViewModel();
            var stream = await fileClient.Read(file);
            await viewModel.Prepare(new WordViewerViewModelParameter { FileStream = stream, Title = file.Name });
            new WordViewerWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Format is HighLevelFileType.Word)
            {
                return true;
            }

            return false;
        }
    }
}