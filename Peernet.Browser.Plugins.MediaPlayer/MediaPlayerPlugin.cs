using LibVLCSharp.Shared;
using Microsoft.Extensions.DependencyInjection;
using Peernet.Browser.Plugins.MediaPlayer.Services;
using Peernet.SDK.Models.Plugins;

namespace Peernet.Browser.Plugins.MediaPlayer
{
    public class MediaPlayerPlugin : IPlugin
    {
        public void Load(ServiceCollection services)
        {
            services.AddSingleton<IPlayButtonPlug, MediaPlayerService>();
            Core.Initialize();
        }
    }
}