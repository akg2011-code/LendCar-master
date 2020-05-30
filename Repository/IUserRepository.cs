using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.DBContext;
namespace LendCar.Repository
{
    public interface IUserRepository
    {
        LendCarDBContext Context { get; }
        List<ApplicationUser> GetAllUsers();
        ApplicationUser FindByNatId(string id);
        ApplicationUser FindById(string id);
        void EditBookingInfo(ApplicationUser user);
        void EditPhotoPath(string userId, string phtoPath);
        void Save();
    }
}
