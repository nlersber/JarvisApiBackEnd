using Newtonsoft.Json;

namespace JarvisNG.DTO {
    public class OrderItemDTO {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}