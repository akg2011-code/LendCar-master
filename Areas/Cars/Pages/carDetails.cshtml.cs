using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Pages
{
    public class carDetailsModel : PageModel
    {
        public Vehicle CurrentCar { get; set; }
        public ApplicationUser Owner { get; set; }

        public List<CarImage> CurrentCarImges { get; set; }
        public ICarRepository CarRepository { get; }
        public IUserRepository UserRepository { get; }

        [BindProperty]
        public string EndBookingDate { get; set; }
        [BindProperty]
        public string StartBookingDate { get; set; }
        public bool DateIsNotCorrect { get; private set; }

        public Dictionary<string, string> avilabeldays;
        public carDetailsModel(ICarRepository _CarRepository, IUserRepository _UserRepository)
        {
            CarRepository = _CarRepository;
            UserRepository = _UserRepository;
        }
        public IActionResult OnGet(int id)
        {
            CurrentCar = CarRepository.GetVehicle(id);

            StartBookingDate = CarRepository.ChangeDateFormatToYearsMonthDays(CurrentCar.StartDate);
            EndBookingDate = CarRepository.ChangeDateFormatToYearsMonthDays(CurrentCar.EndDate);

            CurrentCarImges = CurrentCar?.Photos.ToList();
            Owner = UserRepository.FindById(CurrentCar?.OwnerId);

            if (TempData["avilabeldays"] != null)
            {
                avilabeldays =(Dictionary<string,string>)TempData["avilabeldays"];
            }
            if (TempData["DateIsNotCorrect"] != null)
            {
                DateIsNotCorrect = true;
            }
            return Page();
        }
        public IActionResult OnPost(string endBookingDate, string startBookingDate, int carId)
        {
            if (ModelState.IsValid)
            {
                var availableDays = CarRepository.AvailableDays(
                    CarRepository.ChangeDateFormatToDaysMonthYears(startBookingDate),
                    CarRepository.ChangeDateFormatToDaysMonthYears(endBookingDate), carId);

                if (availableDays != null)
                {
                    if (!availableDays.Any(c => c.Value == "Not Available"))
                    {
                        return RedirectToPage("booking",
                            new
                            {
                                area = "Cars",
                                EndBookingDate = endBookingDate,
                                StartBookingDate = startBookingDate,
                                CarId = carId
                            });
                    }
                    else
                    {

                        TempData["avilabeldays"] = availableDays;
                        return OnGet(carId);
                    }

                }
                TempData["DateIsNotCorrect"] = true;
            }
            TempData["DateIsNotCorrect"] = true;
            return OnGet(carId);
        }
    }
}