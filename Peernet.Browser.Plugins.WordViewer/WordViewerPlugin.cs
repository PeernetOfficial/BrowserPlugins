using Microsoft.Extensions.DependencyInjection;
using Peernet.Browser.Plugins.WordViewer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.WordViewer
{
    public class WordViewerPlugin : IPlugin
    {
        public void Load(ServiceCollection services)
        {
            services.AddSingleton<IPlayButtonPlug, WordViewerService>();
        }
    }
}