using Microsoft.Extensions.DependencyInjection;
using Peernet.Browser.Plugins.ByteViewer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.ImageViewer
{
    public class ByteViewerPlugin : IPlugin
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<IPlayButtonPlug, ByteViewerService>();
        }
    }
}