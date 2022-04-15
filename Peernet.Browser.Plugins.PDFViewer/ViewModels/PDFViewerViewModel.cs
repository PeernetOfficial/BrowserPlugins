using Peernet.Browser.Plugins.PDFViewer.ViewModels.Parameters;
using Peernet.SDK.Models.Presentation;
using System.IO;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.PDFViewer.ViewModels
{
    public class PDFViewerViewModel : GenericViewModelBase<PDFViewerViewModelParameter>
    {
        private Stream filreStream;
        private string title;

        public Stream FileStream
        {
            get => filreStream;
            set
            {
                filreStream = value;
                OnPropertyChanged(nameof(FileStream));
            }
        }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public override Task Prepare(PDFViewerViewModelParameter parameter)
        {
            FileStream = parameter.FileStream;
            Title = parameter.Title;

            return Task.CompletedTask;
        }
    }
}