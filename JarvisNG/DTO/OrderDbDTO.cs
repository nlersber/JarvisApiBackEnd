using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.DTO {
    public class OrderDbDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string Time { get; set; }
    }
}
