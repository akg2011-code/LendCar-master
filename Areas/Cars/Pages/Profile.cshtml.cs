using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Pages
{
    public class userProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepo;
        private string _uid;
        private ICarRepository _carRepo;
        public ApplicationUser CurrentUser { get; set; }
        public List<Vehicle> UserVehicles { get; set; }
        public userProfileModel(UserManager<ApplicationUser> userManager,
                                IUserRepository userRepo,
                                ICarRepository carRepo)
        {
            _userManager = userManager;
            _userRepo = userRepo;
            _carRepo = carRepo;
        }
        public IActionResult OnGet()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("Login",new { area = "Account"});
            }
            _uid = _userManager.GetUserId(User);
            CurrentUser = _userRepo.FindById(_uid);
            UserVehicles = _carRepo.GetAllVehiclesAccepted().Where(v => v.OwnerId == _uid).ToList();
            return Page();
        }
    }
}