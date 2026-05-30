


namespace MiniPay.API.Domain;

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ReferenceId { get; set; } = string.Empty;
}