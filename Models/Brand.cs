using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Required,StringLength(50),DisplayName("Brand name")]
        public string Name { get; set; }
        public ICollection<BrandModel> BrandModels { get; set; }
    }
}
