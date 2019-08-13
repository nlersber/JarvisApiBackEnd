using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Models.Domain {
    public class HistoryOrder {
        public string Date { get; set; }
        public IList<HistoryOrderItem> Items { get; set; }
    }
}
