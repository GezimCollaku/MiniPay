using System;
using System.Text.Json.Serialization;

namespace MiniPay.API.Models
{
    public class PaymentResponseDto
    {
        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("transactionId")]
        public string TransactionId { get; set; } = string.Empty;

        [JsonPropertyName("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("referenceId")]
        public string ReferenceId { get; set; } = string.Empty;
    }
}