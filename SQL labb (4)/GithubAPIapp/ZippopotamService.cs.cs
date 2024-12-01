using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ZippopotamService
{
    private readonly HttpClient _httpClient;

    public ZippopotamService()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Hämtar platsinformation från Zippopotam API baserat på landskod och postnummer.
    public async Task<ZippopotamResponse?> GetLocationInfoAsync(string countryCode, string zipCode)
    {
        try
        {
            var response = await _httpClient.GetStringAsync($"https://api.zippopotam.us/{countryCode}/{zipCode}");
            if (!string.IsNullOrEmpty(response))
            {
                return JsonSerializer.Deserialize<ZippopotamResponse>(response);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel inträffade: {ex.Message}");
        }

        return null;
    }
}
