using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Areas.Account.Pages
{
    public class HistoryModel : PageModel
    {
        public List<VehicleBooking> UserBookingCars;
        public List<Vehicle> UserCars;
        private readonly UserManager<ApplicationUser> UserManager;

        public ICarRepository ICarRepository { get; }
        public HistoryModel(ICarRepository ICarRepository, UserManager<ApplicationUser> userManager)
        {
            this.ICarRepository = ICarRepository;
            UserManager = userManager;
        }


        public void OnGet()
        {
            var userId = UserManager.GetUserId(User);
            UserBookingCars = ICarRepository.GetAllBookingByUserID(userId);
            UserCars = ICarRepository.GetAllUserCar(userId);
        }
    }
}
