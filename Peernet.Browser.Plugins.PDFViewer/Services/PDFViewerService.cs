using Peernet.Browser.Plugins.PDFViewer.ViewModels;
using Peernet.Browser.Plugins.PDFViewer.ViewModels.Parameters;
using Peernet.SDK.Client.Clients;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System.IO;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.PDFViewer.Services
{
    public class PDFViewerService : IPlayButtonPlug
    {
        private readonly IFileClient fileClient;

        public PDFViewerService(IFileClient fileClient)
        {
            this.fileClient = fileClient;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            var viewModel = new PDFViewerViewModel();
            var stream = await fileClient.Read(file);
            await viewModel.Prepare(new PDFViewerViewModelParameter { FileStream = stream, Title = file.Name });
            new PDFViewerWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Format is HighLevelFileType.PDF)
            {
                return true;
            }

            return false;
        }
    }
}