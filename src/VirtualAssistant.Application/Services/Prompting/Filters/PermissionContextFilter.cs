using VirtualAssistant.Application.Interfaces;
using VirtualAssistant.Domain.Entities;

namespace VirtualAssistant.Application.Services.Prompting.Filters;

public class PermissionContextFilter : IContextFilter
{
    public IEnumerable<DocumentChunk> Filter(IEnumerable<DocumentChunk> chunks, UserProfile user)
    {
        return chunks.Where(chunk =>
            !chunk.Metadata.TryGetValue("permission", out var requiredPermission) ||
            user.Permissions.Contains(requiredPermission.ToString()!));
    }
}