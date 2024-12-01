using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class GitHubService
{
    private readonly HttpClient _httpClient;

    public GitHubService()
    {
        _httpClient = new HttpClient();

        // Lägg till en User-Agent-header, GitHub kräver det
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "C# Console App");
    }

    public async Task<List<GitHubRepository>> GetRepositoriesAsync()
    {
        var url = "https://api.github.com/orgs/dotnet/repos";
        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"API-förfrågan misslyckades: {response.StatusCode}");
        }

        var json = await response.Content.ReadAsStringAsync();

        // Deserialisera JSON till lista av GitHubRepository
        var repositories = JsonSerializer.Deserialize<List<GitHubRepository>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return repositories;
    }
}
