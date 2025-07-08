using VirtualAssistant.Domain.Entities;

namespace VirtualAssistant.Application.Interfaces;

public interface IPromptBuilder
{
    string Build(string query, IEnumerable<DocumentChunk> contextChunks, IEnumerable<ChatMessage> history, UserProfile user);
}