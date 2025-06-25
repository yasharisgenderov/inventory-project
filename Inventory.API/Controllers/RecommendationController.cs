using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Inventory.Application.DTOs;

[ApiController]
[Route("api/[controller]")]
public class RecommendationController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public RecommendationController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("OpenAI");
    }

    [HttpPost]
    public async Task<IActionResult> RecommendAsync([FromBody] UserInputDto input)
    {
        var prompt = $"Mənim büdcəm {input.Budget} AZN-dir. Mən {input.Purpose} üçün bir maşın axtarıram. Mənə uyğun maşınlar tövsiyə et və bu axtarışı edərkən yalnızca Azərbaycan bazarını nəzərə al";
        
        var request = new
        {
            model = "gpt-4o",
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        var response = await _httpClient.PostAsJsonAsync("chat/completions", request,
            new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var responseText = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, responseText);

        var result = JsonSerializer.Deserialize<OpenAiResponse>(responseText, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return Ok(result?.Choices.FirstOrDefault()?.Message.Content);
    }
}