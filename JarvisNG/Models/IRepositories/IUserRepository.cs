using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JarvisNG.Models.Domain;

namespace JarvisNG.Models.IRepositories {
    interface IUserRepository {
        User GetBy(string name);
    }
}
