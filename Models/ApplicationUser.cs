using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Models
{
    public class ApplicationUser : IdentityUser
    {
       
        public string NationalId { get; set; }
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string Address { get; set; }
        public int TripsNumber { get; set; }
        
        public string Region { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        [Required, Display(Name = "Driver License Number")]
        public string DriverLicenseNumber { get; set; }
        [DataType(DataType.MultilineText)]
        public string BriefAboutMe { get; set; }
        public string BirthDate { get; set; }
        [Required,Display(Name ="Phone Number")]
        public new  string PhoneNumber { get; set; }
        public string JoinedAt { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        public int GenderId { get; set; }
        [ForeignKey("GenderId")]
        public Gender Gender { get; set; }
        
        public virtual ICollection<Vehicle> VehiclesOwnedByHim { get; set; }
    }
}
