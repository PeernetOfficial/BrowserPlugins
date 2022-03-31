using Peernet.Browser.Plugins.HTMLViewer.ViewModels.Parameters;
using Peernet.SDK.Models.Presentation;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.HTMLViewer.ViewModels
{
    public class HTMLViewerViewModel : GenericViewModelBase<HTMLViewerViewModelParameter>
    {
        private string content;

        private string title;

        public string Content
        {
            get => content;
            set
            {
                content = value;
                OnPropertyChanged(nameof(Content));
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

        public override Task Prepare(HTMLViewerViewModelParameter parameter)
        {
            Content = parameter.Content;
            Title = parameter.Title;

            return Task.CompletedTask;
        }
    }
}