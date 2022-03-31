using Peernet.Browser.Plugins.HTMLViewer.ViewModels;
using Peernet.SDK.Client.Clients;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Plugins;
using System.IO;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.HTMLViewer.Services
{
    public class HTMLViewerService : IPlayButtonPlug
    {
        private readonly IFileClient fileClient;

        public HTMLViewerService(IFileClient fileClient)
        {
            this.fileClient = fileClient;
        }

        // Open Window on Execute
        public async Task Execute(ApiFile file)
        {
            using var stream = await fileClient.Read(file);
            var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();
            var viewModel = new HTMLViewerViewModel();
            await viewModel.Prepare(new(content, file.Name));
            new HTMLViewerWindow(viewModel).Show();
        }

        public bool IsSupported(ApiFile file)
        {
            if (file?.Format is HighLevelFileType.HTML)
            {
                return true;
            }

            return false;
        }
    }
}