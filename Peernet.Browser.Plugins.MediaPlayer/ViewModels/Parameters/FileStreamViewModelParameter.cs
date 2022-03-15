using Peernet.SDK.Models.Domain.Common;
using System;

namespace Peernet.Browser.Plugins.MediaPlayer.ViewModels.Parameters
{
    public class FileStreamViewModelParameter
    {
        public FileStreamViewModelParameter(Uri source, LowLevelFileType type, string fileName)
        {
            Source = source;
            FileType = type;
            FileName = fileName;
        }

        public LowLevelFileType FileType { get; set; }

        public string FileName { get; set; }

        public Uri Source { get; set; }
    }
}
