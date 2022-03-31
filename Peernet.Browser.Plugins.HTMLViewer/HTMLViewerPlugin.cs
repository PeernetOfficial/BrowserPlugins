using Microsoft.Extensions.DependencyInjection;
using Peernet.Browser.Plugins.HTMLViewer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.HTMLViewer
{
    public class HTMLViewerPlugin : IPlugin
    {
        public void Load(ServiceCollection services)
        {
            services.AddSingleton<IPlayButtonPlug, HTMLViewerService>();
        }
    }
}