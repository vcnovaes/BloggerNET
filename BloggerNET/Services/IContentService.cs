namespace BloggerNET.Services;

public interface IContentService
{ public Task<List<string>> GetContent(CancellationToken token, uint numberOfParagraphs);
 public Task<List<string>> GetContent(CancellationToken token);
}