using JarvisNG.Models.Domain.Enums;

namespace JarvisNG.DTO {
    public class ItemDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Category { get; set; }
        public int Count { get; set; }
    }
}