namespace VirtualAssistant.Domain.Entities
{
    public class DocumentChunk
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
