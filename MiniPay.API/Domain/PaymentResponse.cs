
namespace MiniPay.API.Domain;

public class PaymentResponse
{
    public string Status { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string Message { get; set; } = string.Empty;
    public string ReferenceId { get; set; } = string.Empty;
}