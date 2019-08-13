using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;
using Newtonsoft.Json;

namespace JarvisNG.DTO {
    public class OrderDTO {
        [JsonProperty("items")]
        public List<OrderItemDTO> Items { get; set; }
        public string Date { get; set; }
        public string User { get; set; }
    }
}
