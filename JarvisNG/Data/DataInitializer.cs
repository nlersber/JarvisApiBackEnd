using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JarvisNG.Data {
    public class DataInitializer {
        private readonly DataContext context;

        public DataInitializer(DataContext context) {
            this.context = context;
        }

        public void InitializeData() {
            context.Database.EnsureDeleted();
            if (context.Database.EnsureCreated()) {

            }
        }
    }
}
