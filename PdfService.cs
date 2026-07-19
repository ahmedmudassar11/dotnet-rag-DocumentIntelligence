using UglyToad.PdfPig;

namespace WebApplication1
{
    public class PdfService
    {
        public string ReadPdf(string path)
        {
            using var pdf = PdfDocument.Open(path);

            var text = "";

            foreach (var page in pdf.GetPages())
            {
                text += page.Text;
            }

            return text;
        }
    }
}
