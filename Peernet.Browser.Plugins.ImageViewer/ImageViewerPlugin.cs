using Microsoft.Extensions.DependencyInjection;
using Peernet.Browser.Plugins.ImageViewer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.ImageViewer
{
    public class ImageViewerPlugin : IPlugin
    {
        public void Load(ServiceCollection services)
        {
            services.AddSingleton<IPlayButtonPlug, ImageViewerService>();
        }
    }
}