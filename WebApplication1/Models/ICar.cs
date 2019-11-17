using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models {
    interface ICar {
        int Id { get; set; }
        string Make { get; set; }
        string Model { get; set; }
    }
}
