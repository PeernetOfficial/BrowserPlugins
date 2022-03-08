using Peernet.SDK.Models.Domain.Common;
using System;

namespace Peernet.Browser.Plugins.MediaPlayer.ViewModels.Parameters
{
    public class FileStreamViewModelParameter
    {
        public FileStreamViewModelParameter(Uri source, LowLevelFileType type)
        {
            Source = source;
            FileType = type;
        }

        public LowLevelFileType FileType { get; set; }

        public Uri Source { get; set; }
    }
}
