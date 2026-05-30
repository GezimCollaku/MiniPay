using Microsoft.AspNetCore.Mvc;
using MiniPay.API.Data;
using MiniPay.API.Domain;
using System.Text;
using System.Text.Json;

namespace MiniPay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly MiniPayDbContext _context;
    private readonly HttpClient _httpClient;
    
    public PaymentController(MiniPayDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    [HttpPost("simulate/{providerId}")]
    public async Task<ActionResult<PaymentResponse>> SimulatePayment(int providerId, [FromBody] PaymentRequest request)
    {
       
        var provider = _context.PaymentProviders.FirstOrDefault(p => p.Id == providerId);
        if (provider == null)
        {
            return NotFound(new { message = "Ofruesi i pagesave nuk ekziston." });
        }

        
        if (!provider.IsActive)
        {
            return BadRequest(new { message = $"Ofruesi '{provider.Name}' është aktualisht i çaktivizuar." });
        }

        try
        {
            
            var jsonPayload = JsonSerializer.Serialize(request);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            
            var response = await _httpClient.PostAsync(provider.EndpointUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, new { message = "Gabim gjatë lidhjes me portën e jashtme të pagesave." });
            }

            
            var responseString = await response.Content.ReadAsStringAsync();
            var paymentResponse = JsonSerializer.Deserialize<PaymentResponse>(responseString, new JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true 
            });

            return Ok(paymentResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Ndodhi një gabim gjatë simulimit: {ex.Message}" });
        }
    }
}