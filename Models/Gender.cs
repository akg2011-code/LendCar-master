using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class Gender
    {
        public int Id { get; set; }
        public string Type { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }
    }
}
