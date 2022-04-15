using System.IO;

namespace Peernet.Browser.Plugins.PDFViewer.ViewModels.Parameters
{
    public class PDFViewerViewModelParameter
    {
        public Stream FileStream { get; set; }

        public string Title { get; set; }
    }
}