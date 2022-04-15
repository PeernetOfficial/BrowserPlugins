extern alias DependencyInjection;

using Peernet.Browser.Plugins.ExcelViewer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.ExcelViewer
{
    public class ExcelViewerPlugin : IPlugin
    {
        public void Load(DependencyInjection::Microsoft.Extensions.DependencyInjection.ServiceCollection services)
        {
            DependencyInjection::Microsoft.Extensions.DependencyInjection.ServiceCollectionServiceExtensions.AddSingleton<IPlayButtonPlug, ExcelViewerService>(services);
        }
    }
}