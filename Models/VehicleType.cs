using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class VehicleType
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
