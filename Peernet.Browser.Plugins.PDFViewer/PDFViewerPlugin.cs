using Microsoft.Extensions.DependencyInjection;
using Peernet.Browser.Plugins.PDFViewer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.PDFViewer
{
    public class PDFViewerPlugin : IPlugin
    {
        public void Load(ServiceCollection services)
        {
            services.AddSingleton<IPlayButtonPlug, PDFViewerService>();
        }
    }
}