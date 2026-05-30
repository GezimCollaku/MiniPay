using Microsoft.AspNetCore.Mvc;
using MiniPay.API.Data;
using MiniPay.API.Domain;
using MiniPay.API.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MiniPay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly MiniPayDbContext _context;
    private readonly HttpClient _httpClient;

    public TransactionController(MiniPayDbContext context, HttpClient httpClient)
    {
        _context = context;
        _httpClient = httpClient;
    }

    [HttpPost("simulate/{providerId}")]
    public async Task<IActionResult> SimulateTransaction(int providerId, [FromBody] PaymentRequestDto request)
    {
        
        var provider = _context.PaymentProviders.FirstOrDefault(x => x.Id == providerId);
        
        if (provider == null)
        {
            return NotFound(new { message = "Ofruesi i pagesave nuk ekziston." });
        }

        
        if (!provider.IsActive)
        {
            return BadRequest(new { message = "Ky ofrues nuk mund të përdoret sepse është çaktivizuar." });
        }

        
        if (!string.Equals(provider.Currency, request.Currency, StringComparison.OrdinalIgnoreCase))
        {
            return BadRequest(new { message = $"Ky ofrues mbështet vetëm monedhën {provider.Currency}." });
        }

        
        if (string.IsNullOrEmpty(request.ReferenceId))
        {
            request.ReferenceId = $"ORDER-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }

        try
        {
            
            if (!string.IsNullOrEmpty(provider.EndpointUrl) && Uri.IsWellFormedUriString(provider.EndpointUrl, UriKind.Absolute))
            {
                var httpResponse = await _httpClient.PostAsJsonAsync(provider.EndpointUrl, request);
                
                if (httpResponse.IsSuccessStatusCode)
                {
                    var paymentResult = await httpResponse.Content.ReadFromJsonAsync<PaymentResponseDto>();
                    return Ok(paymentResult);
                }
                
                return BadRequest(new { message = "Ofruesi i jashtëm ktheu gabim gjatë procesimit." });
            }

            
            var mockResponse = new PaymentResponseDto
            {
                Status = "Success",
                TransactionId = $"TX{new Random().Next(10000000, 99999999)}",
                Timestamp = DateTime.UtcNow,
                Message = "Pagesa u përpunua me sukses (Simulim)",
                ReferenceId = request.ReferenceId
            };

            return Ok(mockResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = $"Gabim gjatë thirrjes së ofruesit: {ex.Message}" });
        }
    }
}