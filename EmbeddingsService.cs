using OpenAI.Embeddings;

namespace WebApplication1
{
    public class EmbeddingService
    {
        private readonly EmbeddingClient _embeddingClient;

        public EmbeddingService(IConfiguration configuration)
        {
            var apiKey = configuration["OpenAI:ApiKey"];

            _embeddingClient = new EmbeddingClient(
                model: "text-embedding-3-small",
                apiKey: apiKey);
        }

        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var response = await _embeddingClient.GenerateEmbeddingAsync(text);

            return response.Value.ToFloats().ToArray();
        }
    }
}
