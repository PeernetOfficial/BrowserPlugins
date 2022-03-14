using System.IO;

namespace Peernet.Browser.Plugins.ImageViewer.ViewModels.Parameters
{
    public class ByteViewerViewModelParameter
    {
        public Stream FileStream { get; set; }

        public string Title { get; set; }
    }
}