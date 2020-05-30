using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Pages
{
    public class LoginModel : PageModel
    {
        [Required]       
        [BindProperty]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [BindProperty]
        public string Password { get; set; }

        public SignInManager<ApplicationUser> SignInManager { get; }
        public UserManager<ApplicationUser> Usermanger { get; }
       
        private readonly RoleManager<IdentityRole> _roleManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
                          UserManager<ApplicationUser> usermanger,
                          RoleManager<IdentityRole> roleManager
                         )
        {
            SignInManager = signInManager;
            Usermanger = usermanger;
            _roleManager = roleManager;

        }
        public IActionResult OnGet()
        {
            if (!User.Identity.IsAuthenticated)
                return Page();
            else
                return RedirectToPage("Index");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            #region AddUserAndRolesAndAssign
            //ApplicationUser user1 = new ApplicationUser()
            //{
            //    Email = "mohamedesam9397@gmail.com",
            //    UserName = "MohamedEsam",
            //    ImageUrl = "https://lh3.googleusercontent.com/-xkin9yi5v5E/AAAAAAAAAAI/AAAAAAAAAAA/AMZuucnOElStaOw_H_2SsTTYEoZyntSIyQ/photo.jpg?sz=46",
            //    NationalId = "12345678998752",
            //    FirstName = "Mohamed",
            //    GenderId = 1,
            //    TripsNumber = 2334,

            //    DriverLicenseNumber = "828282828",
            //    PhoneNumber = "0124869196",

            //    LastName = "Esam",
            //    CityId = 1


            //};
            //await Usermanger.CreateAsync(user1, "Sara@ask123.com");

            //ApplicationUser user2 = new ApplicationUser()
            //{
            //    Email = "akg9397@gmail.com",
            //    UserName = "akg",
            //    ImageUrl = "https://lh3.googleusercontent.com/-xkin9yi5v5E/AAAAAAAAAAI/AAAAAAAAAAA/AMZuucnOElStaOw_H_2SsTTYEoZyntSIyQ/photo.jpg?sz=46",
            //    NationalId = "12345678998652",
            //    FirstName = "Ahmed",
            //    GenderId = 1,
            //    TripsNumber = 2334,

            //    DriverLicenseNumber = "828282828",
            //    PhoneNumber = "0124869196",

            //    LastName = "Korany",
            //    CityId = 1

            //};
            //await Usermanger.CreateAsync(user2, "Sara@ask123.com");

            //var role = new IdentityRole("Admin");
            //await _roleManager.CreateAsync(role);

            //var SecondRole = new IdentityRole("Member");
            //await _roleManager.CreateAsync(SecondRole);

            //await Usermanger.AddToRoleAsync(user1, "Admin");

            //await Usermanger.AddToRoleAsync(user2, "Member");
            #endregion


            var result = await SignInManager.PasswordSignInAsync(UserName, Password, false, false);

            if (result.Succeeded)
            {
                if (User.IsInRole("Admin"))
                    return RedirectToPage("Admin", new { area = "Admin"});
                else
                    return RedirectToPage("Index");
            }
            else
                return RedirectToPage("Login", new { area = "Account"});
        }
    }
}