using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Backend.Controllers.Resources;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("user")]
public class UserManagementApiController: ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;
    private readonly string _auth0DomainUrl;
    private readonly string _auth0Domain;

    public UserManagementApiController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
        
        _auth0DomainUrl = _config["Auth0:Domain"];
        _auth0Domain = _auth0DomainUrl.Replace("https://", "").TrimEnd('/');
    }
    
    [HttpPatch("update-profile")]
    public async Task<IActionResult> UpdateUserMetadata([FromBody] UpdateUserResource request)
    {
        var token = await GetManagementApiToken();

        if (string.IsNullOrEmpty(token))
        {
            return StatusCode(500, "Unable to get Auth0 token");
        }

        var client = _httpClientFactory.CreateClient();

        var payload = new
        {
            user_metadata = new
            {
                first_name = request.FirstName,
                last_name = request.LastName,
                phone_number = request.PhoneNumber
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var userId = request.UserId;

        var req = new HttpRequestMessage(HttpMethod.Patch, $"https://{_auth0Domain}/api/v2/users/{userId}");
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        req.Content = content;

        var response = await client.SendAsync(req);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, error);
        }

        return Ok(await response.Content.ReadAsStringAsync());
    }
    
    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var token = await GetManagementApiToken();

        if (string.IsNullOrEmpty(token))
        {
            return StatusCode(500, "Unable to get Auth0 token");
        }

        var client = _httpClientFactory.CreateClient();

        var request = new HttpRequestMessage(HttpMethod.Get, $"https://{_auth0Domain}/api/v2/users");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            return StatusCode((int)response.StatusCode, error);
        }

        var result = await response.Content.ReadAsStringAsync();
        return Ok(JsonDocument.Parse(result));
    }

    
    private async Task<string> GetManagementApiToken()
    {
        var client = _httpClientFactory.CreateClient();
        Console.WriteLine($"Auth0 DOMAIN: {_auth0Domain}");
        var clientId = _config["Auth0:ClientId"];
        var clientSecret = _config["Auth0:ClientSecret"];
        var audience = _config["Auth0:ManagementAPIAudience"];

        var body = new Dictionary<string, string>
        {
            {"grant_type", "client_credentials"},
            {"client_id", clientId},
            {"client_secret", clientSecret},
            {"audience", audience}
        };

        var request = new HttpRequestMessage(HttpMethod.Post, $"https://{_auth0Domain}/oauth/token")
        {
            Content = new FormUrlEncodedContent(body)
        };

        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode) return null;

        var json = await response.Content.ReadAsStringAsync();
        var tokenObj = JsonSerializer.Deserialize<JsonElement>(json);
        Console.WriteLine($"Auth0 ACCESS TOKEN: {tokenObj}");
        return tokenObj.GetProperty("access_token").GetString();
    }
}