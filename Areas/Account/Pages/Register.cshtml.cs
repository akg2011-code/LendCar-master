using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LendCar.Models;
using Microsoft.Extensions.Logging;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using LendCar.DBContext;

namespace LendCar.Pages
{
    [AllowAnonymous]

    public class RegisterModel : PageModel
    {
  
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SelectList Cities { get; set; }
        public SelectList Genders { get; set; }
        public LendCarDBContext _context { get; }
      
        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            LendCarDBContext Context,
            RoleManager<IdentityRole> roleManager


            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = Context;
            _roleManager = roleManager;

            Cities = new SelectList(_context.Cities.OrderBy(c => c.Name).ToList(), "Id", "Name");
            Genders = new SelectList(_context.Genders.ToList(), "Id", "Type");
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            //mohamedESAM9397..
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Phone Number")]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            [Display(Name = "DriverLicenseNumber")]
            public string DriverLicenseNumber { get; set; }


            [Required]
            [Display(Name = "Address")]
            public string Address { get; set; }
            public int CityId { get; set; }
            public int GenderId { get; set; }
        }

        public async Task OnGetAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                     

        }

        public async Task<IActionResult> OnPostAsync()
        {
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {

             var user = new ApplicationUser { UserName = Input.UserName, Email = Input.Email 
                    ,JoinedAt = DateTime.Now.ToString("MMMM yyyy"),
                    DriverLicenseNumber = Input.DriverLicenseNumber,
                    PhoneNumber = Input.PhoneNumber, Address = Input.Address,
                   GenderId=Input.GenderId,CityId=Input.CityId
              };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                  
                    if (!await _roleManager.RoleExistsAsync("user"))
                    {
                        await _roleManager.CreateAsync(new  IdentityRole("user") );

                    }

                    await _userManager.AddToRoleAsync(user, "user");
                    await _signInManager.PasswordSignInAsync(user.UserName, Input.Password, false, false);
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

    }
}