using LibVLCSharp.Shared;
using Peernet.SDK.Models.Presentation;
using System;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.MediaPlayer.ViewModels
{
    public class FileStreamViewModel : GenericViewModelBase<Uri>
    {
        public Uri Source { get; set; }

        static FileStreamViewModel()
        {
            Core.Initialize();
        }

        public LibVLCSharp.Shared.MediaPlayer MediaPlayer { get; set; }

        public override Task Prepare(Uri source)
        {
            Source = source;
            Core.Initialize();

            var libVlc = new LibVLC();
            var media = new Media(libVlc, source);
            MediaPlayer = new LibVLCSharp.Shared.MediaPlayer(media)
            {
                EnableHardwareDecoding = true
            };

            return Task.CompletedTask;
        }
    }
}