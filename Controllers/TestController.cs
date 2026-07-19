using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly PdfService _pdfService;
        private readonly EmbeddingService _embeddingService;
        private readonly VectorService _vectorService;

        public TestController(
            PdfService pdfService,
            EmbeddingService embeddingService, VectorService vectorService)
        {
            _pdfService = pdfService;
            _embeddingService = embeddingService;
            _vectorService = vectorService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {

            await _vectorService.CreateCollectionAsync();
            var text = _pdfService.ReadPdf("Documents/HRPolicy.pdf");

            var embedding = await _embeddingService.GenerateEmbeddingAsync(text);
            await _vectorService.StoreAsync(text, embedding);

            return Ok("Document indexed successfully.");
        }
    }
}
