using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
using LendCar.Models;
using Microsoft.EntityFrameworkCore;

namespace LendCar.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository(LendCarDBContext Context)
        {
            this.Context = Context;
        }
        public LendCarDBContext Context { get; }
        public void EditBookingInfo(ApplicationUser user)
        {
            var appUser = FindById(user.Id);
            appUser.FirstName = user.FirstName;
            appUser.LastName = user.LastName;
            appUser.PhoneNumber = user.PhoneNumber;
            appUser.DriverLicenseNumber = user.DriverLicenseNumber;
            appUser.CityId = user.CityId;
            appUser.BirthDate = user.BirthDate;
        }

        public List<ApplicationUser> GetAllUsers() => Context.Users.Include(u => u.City)
                                                                    .Include(u => u.Gender)
                                                                    .ToList();
        public ApplicationUser FindByNatId(string id) => Context.Users.Include(u => u.City)
                                                                       .Include(u => u.Gender)
                                                                       .SingleOrDefault(p => p.NationalId == id);
        public ApplicationUser FindById(string id) => Context.Users.Include(u=>u.City)
                                                                   .Include(u=>u.Gender)
                                                                   .SingleOrDefault(p => p.Id == id);
        public void EditPhotoPath(string userId, string phtoPath)
        {
            FindById(userId).ImageUrl = phtoPath;
        }
        public void Save() => Context.SaveChanges();
       

    }
}
