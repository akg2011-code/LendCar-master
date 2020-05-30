using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required, RegularExpression("[A-Z|0-9]{17}", ErrorMessage = "VIN is invalid")]
        public string VIN { get; set; }
        public string Region { get; set; }
        [Required, DisplayName("Plate number")]
        public string PlateNumber { get; set; }
        [Required, RegularExpression("^[1-2][8-9|0][0-9]{2}$", ErrorMessage = "Year should be four digits (e.g 2010)")]
        public string Year { get; set; }

        [DisplayName("Number of seats")]

        public int NumberOfSeats { get; set; }
        [DisplayName("Number of doors")]

        public int NumberOfDoors { get; set; }
        [DisplayName("Gas Mileage (KMPL)")]
        public double GasMileage { get; set; }
        public string EnergyMakeCarMove { get; set; }
        public int? TripsNumber { get; set; }
        [DisplayName("Price per day")]
        public decimal PricePerDay { get; set; }
        [Required(ErrorMessage = "Please specify when your car will be available"),DisplayName("Start Date")]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "Please specify when your car will not be available"),DisplayName("End Date")]
        public string EndDate { get; set; }
    
        [Range(1, 5)]
        public double? Rate { get; set; }
        public string ImageUrl { get; set; }

        [DefaultValue("false")]
        public bool AcceptedAdmin { get; set; }
        public ICollection<CarImage> Photos { get; set; }

        public string OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public ApplicationUser Owner { get; set; }

        public int VehicleTypeId { get; set; }
        [ForeignKey("VehicleTypeId")]
        public VehicleType VehicleType { get; set; }

        public int OdoMeterId { get; set; }
        [ForeignKey("OdoMeterId")]
        public OdoMeter OdoMeter { get; set; }

        public int ModelId { get; set; }
        [ForeignKey("ModelId")]
        public BrandModel Model { get; set; }

        public int ColorId { get; set; }
        [ForeignKey("ColorId")]
        public Color Color { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
    }
}
