namespace WebApplication1
{
    public class DocumentChunk
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Text { get; set; } = string.Empty;

        public float[] Embedding { get; set; } = [];
    }
}
