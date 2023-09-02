using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BloggerNET.Services;

public class BaconIpsumService : IContentService
{
    private readonly HttpClient _httpClient;
    
    public BaconIpsumService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri($"https://baconipsum.com/api/");
    }

    public async Task<List<string>> GetContent(CancellationToken token, uint numberOfParagraphs = 1)
    {
        var httpResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/?type=none&paras={numberOfParagraphs}", token);
        
        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync(token);

        return JsonSerializer.Deserialize<List<string>>(stringResponse) ?? throw new SerializationException();
    }
}