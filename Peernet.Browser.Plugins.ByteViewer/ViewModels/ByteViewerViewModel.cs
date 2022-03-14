using Peernet.Browser.Plugins.ImageViewer.ViewModels.Parameters;
using Peernet.SDK.Models.Presentation;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Peernet.Browser.Plugins.ImageViewer.ViewModels
{
    public class ByteViewerViewModel : GenericViewModelBase<ByteViewerViewModelParameter>
    {
        private Stream fileStream;
        private string title;

        public Stream FileStream
        {
            get => fileStream;
            set
            {
                fileStream = value;
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

        public override Task Prepare(ByteViewerViewModelParameter parameter)
        {
            FileStream = parameter.FileStream;
            Title = parameter.Title;

            return Task.CompletedTask;
        }
    }
}