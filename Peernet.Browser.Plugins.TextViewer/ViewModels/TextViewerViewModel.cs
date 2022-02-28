using Peernet.Browser.Plugins.TextViewer.ViewModels.Parameters;
using Peernet.SDK.Models.Presentation;
using System.Threading.Tasks;

namespace Peernet.Browser.Plugins.TextViewer.ViewModels
{
    public class TextViewerViewModel : GenericViewModelBase<TextViewerViewModelParameter>
    {
        private string fileContent;
        private string title;

        public string FileContent
        {
            get => fileContent;
            set
            {
                fileContent = value;
                OnPropertyChanged(nameof(FileContent));
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

        public override Task Prepare(TextViewerViewModelParameter parameter)
        {
            FileContent = parameter.Content;
            Title = parameter.Title;

            return Task.CompletedTask;
        }
    }
}