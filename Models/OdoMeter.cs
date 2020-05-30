using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class OdoMeter
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
