using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models {
    public class Car : ICar {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }

    public enum FuelType {
        None,
        Petrol,
        Diesel,
        Electric,
        Hybrid,
        Other
    }
}
