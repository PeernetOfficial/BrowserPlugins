using LibVLCSharp.Shared;
using Peernet.Browser.Plugins.MediaPlayer.ViewModels.Parameters;
using Peernet.SDK.Models.Domain.Common;
using Peernet.SDK.Models.Presentation;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.MediaPlayer.ViewModels
{
    public class FileStreamViewModel : GenericViewModelBase<FileStreamViewModelParameter>
    {
        public LowLevelFileType FileType { get; set; }

        public string FileName { get; set; }

        static FileStreamViewModel()
        {
            Core.Initialize();
        }

        public LibVLCSharp.Shared.MediaPlayer MediaPlayer { get; set; }

        public override Task Prepare(FileStreamViewModelParameter parameter)
        {
            FileType = parameter.FileType;
            FileName = parameter.FileName;
            Core.Initialize();

            var libVlc = new LibVLC();
            var media = new Media(libVlc, parameter.Source);
            MediaPlayer = new LibVLCSharp.Shared.MediaPlayer(media)
            {
                EnableHardwareDecoding = true,
                NetworkCaching = 300
            };

            return Task.CompletedTask;
        }
    }
}