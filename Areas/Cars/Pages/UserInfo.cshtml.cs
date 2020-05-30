using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LendCar.Pages
{


    public class UserInfo : PageModel
    {

        public SignInManager<ApplicationUser> SignInManager { get; }
        public UserManager<ApplicationUser> Usermanger { get; }
        public IUserRepository UserRerpository { get; }
        public ICityRepostiory CityRepository { get; }
        public ICarRepository CarRepsitory { get; }
        public Contact Contact { get; set; }
        [BindProperty]
        public ApplicationUser LoggedUserInfo { get; set; }

        public static SelectList Days => new SelectList(Enumerable.Range(1, 31), 4);
        public static SelectList Months => new SelectList(Enumerable.Range(1, 12), 4);
        public static SelectList Years =>
            new SelectList(Enumerable.Range(1990, DateTime.Now.Year - 1989), 1994);

        public SelectList Cites => new SelectList(CityRepository.GetAllCites(), "Id", "Name");


        [BindProperty]
        public string Day { get; set; }
        [BindProperty]
        public string Month { get; set; }
        [BindProperty]
        public string Year { get; set; }


        [BindProperty]
        public string EndBookingDate { get; set; }
        [BindProperty]
        public string StartBookingDate { get; set; }

        public Vehicle Vehicle { get; set; }
        public int CarId { get; set; }
        public UserInfo(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> usermanger, IUserRepository userRerpository,
            ICityRepostiory cityRepository, ICarRepository carRepsitory, IContactRepository IContactRepsotiory)
        {
            SignInManager = signInManager;
            Usermanger = usermanger;
            UserRerpository = userRerpository;
            CityRepository = cityRepository;
            CarRepsitory = carRepsitory;
            Contact = IContactRepsotiory.GetCompanyContact();

        }
        public async Task OnGetAsync(int carId, string EndBookingDate, string StartBookingDate)
        {

            CarId = carId;
            await SignInManager.PasswordSignInAsync("Moneim2", "Sara@ask123.com", false, false);


            LoggedUserInfo = UserRerpository.FindById(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            this.Vehicle = CarRepsitory.GetVehicle(CarId);

            string[] dateTime = LoggedUserInfo?.BirthDate?.Split('/');
            if (dateTime?.Length == 3)
            {
                Day = dateTime[0];
                Month = dateTime[1];
                Year = dateTime[2];
            }

        }
        public void OnPost(int carId)
        {
            this.Vehicle = CarRepsitory.GetVehicle(carId);
            CarId = carId;

            string[] dateTime = UserRerpository.FindById(this.User.FindFirstValue(ClaimTypes.NameIdentifier))?.BirthDate?.Split('/');
            if (dateTime?.Length == 3)
            {
                Day = dateTime[0];
                Month = dateTime[1];
                Year = dateTime[2];
            }



            if (!ModelState.IsValid)
            {
                LoggedUserInfo.Id = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

                LoggedUserInfo.BirthDate = (Day.Length == 2 ? Day : "0" + Day) + "/" +
                    (Month.Length == 2 ? Month : "0" + Month) + "/" + Year;


                UserRerpository.EditBookingInfo(LoggedUserInfo);
                UserRerpository.Save();


            }

        }
    }
}