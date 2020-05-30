using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class Color
    {
        public int Id { get; set; }
        [Required,MaxLength(25)]
        public string Name { get; set; }
        [Required,MaxLength(7),RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$",ErrorMessage ="Please enter a valid hex value (ex. #FF0000)")]
        public string HexValue { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
