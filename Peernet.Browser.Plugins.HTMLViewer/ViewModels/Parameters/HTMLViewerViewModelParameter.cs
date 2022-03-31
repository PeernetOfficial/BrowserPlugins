namespace Peernet.Browser.Plugins.HTMLViewer.ViewModels.Parameters
{
    public class HTMLViewerViewModelParameter
    {
        public HTMLViewerViewModelParameter(string content, string title)
        {
            Content = content;
            Title = title;
        }

        public string Content { get; set; }

        public string Title { get; set; }
    }
}