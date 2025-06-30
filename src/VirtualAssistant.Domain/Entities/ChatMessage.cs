namespace VirtualAssistant.Domain.Entities
{
    public class ChatMessage
    {
        public enum MessageAuthor { User, Assistant }
        public MessageAuthor Author { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
