using Peernet.Browser.Plugins.ExcelViewer;
using Peernet.Browser.Plugins.ExcelViewer.ViewModels;
using Peernet.Browser.Plugins.ExcelViewer.ViewModels.Parameters;
using Peernet.SDK.Client.Clients;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System.IO;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.ExcelViewer.Services
{
    public class ExcelViewerService : IPlayButtonPlug
    {
        private readonly IFileClient fileClient;

        public ExcelViewerService(IFileClient fileClient)
        {
            this.fileClient = fileClient;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            var viewModel = new ExcelViewerViewModel();
            var stream = await fileClient.Read(file);
            await viewModel.Prepare(new ExcelViewerViewModelParameter { FileStream = stream, Title = file.Name });
            new ExcelViewerWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Format is HighLevelFileType.Excel)
            {
                return true;
            }

            return false;
        }
    }
}