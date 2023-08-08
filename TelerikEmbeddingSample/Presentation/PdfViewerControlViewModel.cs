using Telerik.Maui.Controls.PdfViewer;

namespace TelerikEmbeddingSample.Presentation;

internal partial class PdfViewerControlViewModel : ObservableObject
{
    public PdfViewerControlViewModel()
    {
        var assembly = typeof(PdfViewerControlViewModel).Assembly;
        var resourceName = assembly.GetManifestResourceNames().FirstOrDefault(x => x.EndsWith("pdfviewer-firstlook.pdf"));
        if(!string.IsNullOrEmpty(resourceName))
        {
            using var stream = assembly.GetManifestResourceStream(resourceName);
            var data = stream.ReadBytes();
            Source = new ByteArrayDocumentSource(data);
        }
    }

    [ObservableProperty]
    private DocumentSource? source;
}
