using VirtualAssistant.Domain.Entities;

namespace VirtualAssistant.Application.Interfaces;
public interface ITextMessageProcessor
{
    Task<string> ProcessTextMessageAsync(string query, string conversationId, UserProfile user);
}
public interface IAudioMessageProcessor
{
    Task<byte[]> ProcessAudioMessageAsync(Stream audioStream, string fileName, string contentType, string conversationId, UserProfile user);
}