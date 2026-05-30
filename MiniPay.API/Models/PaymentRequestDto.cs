using System.Text.Json.Serialization;

namespace MiniPay.API.Models
{
    public class PaymentRequestDto
    {
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("referenceId")]
        public string ReferenceId { get; set; } = string.Empty;
    }
}