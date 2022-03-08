using Peernet.Browser.Plugins.ImageViewer.ViewModels.Parameters;
using Peernet.SDK.Models.Presentation;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Peernet.Browser.Plugins.ImageViewer.ViewModels
{
    public class ImageViewerViewModel : GenericViewModelBase<ImageViewerViewModelParameter>
    {
        private BitmapImage imageSource;
        private string title;

        public BitmapImage ImageSource
        {
            get => imageSource;
            set
            {
                imageSource = value;
                OnPropertyChanged(nameof(ImageSource));
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

        public override Task Prepare(ImageViewerViewModelParameter parameter)
        {
            ImageSource = parameter.ImageSource;
            Title = parameter.Title;

            return Task.CompletedTask;
        }
    }
}