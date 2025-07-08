using VirtualAssistant.Domain.Entities;

namespace VirtualAssistant.Application.Interfaces;
public interface IContextFilter
{
    IEnumerable<DocumentChunk> Filter(IEnumerable<DocumentChunk> chunks, UserProfile user);
}