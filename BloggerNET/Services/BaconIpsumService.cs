using System.Runtime.Serialization;
using System.Text.Json;

namespace BloggerNET.Services;

public class BaconIpsumService : IContentService
{
    private readonly HttpClient _httpClient;

    public BaconIpsumService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public BaconIpsumService()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri($"https://baconipsum.com/api/");
    }

    public static string ConvertListOfStringsToParagraph( List<string> list)
    {
        return list.Aggregate(
            (s, s1) => s + '\n' + s1
        );
    }
    public async Task<List<string>> GetContent(CancellationToken token)
    {
        return await GetContent(token, numberOfParagraphs:1);
    }
    public async Task<List<string>> GetContent(CancellationToken token, uint numberOfParagraphs)
    {
        var httpResponse = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/?type=none&paras={numberOfParagraphs}", token);
        
        httpResponse.EnsureSuccessStatusCode();
        var stringResponse = await httpResponse.Content.ReadAsStringAsync(token);

        return JsonSerializer.Deserialize<List<string>>(stringResponse) ?? throw new SerializationException();
    }
}