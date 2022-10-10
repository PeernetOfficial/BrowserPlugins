using Microsoft.Extensions.DependencyInjection;
using Peernet.Browser.Plugins.TextViewer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.TextViewer
{
    public class TextViewerPlugin : IPlugin
    {
        public void Load(IServiceCollection services)
        {
            services.AddSingleton<IPlayButtonPlug, TextViewerService>();
        }
    }
}