using System.Text.Json.Serialization;

namespace Vehicles.Api.Models.Api
{
    public class RegisteredRequest
    {
        [JsonPropertyName("dateFrom")]
        public DateTime DateFrom { get; set; }

        [JsonPropertyName("dateTo")]
        public DateTime DateTo { get; set; }
    }
}
