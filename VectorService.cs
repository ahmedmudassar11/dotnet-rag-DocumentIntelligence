using Qdrant.Client;
using Qdrant.Client.Grpc;

namespace WebApplication1
{
    public class VectorService
    {
        private readonly QdrantClient _client;

        public VectorService()
        {
            _client = new QdrantClient("localhost", 6334);
        }

        public async Task CreateCollectionAsync()
        {
            await _client.CreateCollectionAsync(
                collectionName: "documents",
                vectorsConfig: new VectorParams
                {
                    Size = 1536,
                    Distance = Distance.Cosine
                });
        }

        public async Task StoreAsync(string text, float[] embedding)
        {
            await _client.UpsertAsync(
                collectionName: "documents",
                points: new[]
                {
            new PointStruct
            {
                Id = Guid.NewGuid(),

                Vectors = embedding,

                Payload =
                {
                    ["text"] = text,
                    ["file"] = "HRPolicy.pdf"
                }
            }
                });
        }
    }
}
