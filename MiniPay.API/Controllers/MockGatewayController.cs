using Microsoft.AspNetCore.Mvc;
using MiniPay.API.Domain;

namespace MiniPay.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MockGatewayController : ControllerBase
{
    
    [HttpPost]
    public ActionResult<PaymentResponse> ProcessExternalPayment(PaymentRequest request)
    {
        var response = new PaymentResponse
        {
            Status = "Success",
            TransactionId = "TX" + Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
            Timestamp = DateTime.UtcNow,
            Message = "Pagesa u përpunua me sukses nga porta e jashtme",
            ReferenceId = request.ReferenceId
        };

        return Ok(response);
    }
}