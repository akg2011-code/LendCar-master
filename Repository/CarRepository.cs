using LendCar.DBContext;
using LendCar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class CarRepository : ICarRepository
    {
        public CarRepository(LendCarDBContext Context)
        {
            this.Context = Context;
        }
        public LendCarDBContext Context { get; }

        public Vehicle GetVehicle(int id) => Context.Vehicles.Include(v => v.Color)
                                     .Include(c => c.Owner)
                                     .Include(v => v.Photos)
                                     .Include(v => v.Model)
                                     .ThenInclude(v => v.Brand)
                                     .SingleOrDefault(c => c.Id == id);
        public IQueryable<Vehicle> GetAllVehiclesAccepted() => Context.Vehicles
                                     .Include(v => v.Photos)
                                     .Include(v => v.Color)
                                     .Include(c => c.City)
                                     .Include(v => v.Model)
                                    .ThenInclude(v => v.Brand).Where(v => v.AcceptedAdmin == true);
        public IQueryable<Vehicle> GetAllVehiclesRequests() => Context.Vehicles
                                     .Include(v => v.Photos)
                                     .Include(v => v.Color)
                                     .Include(c => c.City)
                                     .Include(v => v.Model)
                                     .ThenInclude(v => v.Brand).Where(v => v.AcceptedAdmin == false);

        public Dictionary<string, string> AvailableDays(string startDate, string endDate, int carId)
        {
            DateTime startTrip = Convert.ToDateTime(startDate);
            DateTime endTrip = Convert.ToDateTime(endDate);

            if (endTrip.CompareTo(startTrip) > 0)
            {
                Vehicle vehicle = GetVehicle(carId);

                DateTime vechicleStartRentDate = Convert.ToDateTime(vehicle.StartDate);
                DateTime vechcleEndRentDate = Convert.ToDateTime(vehicle.EndDate);

                var dateBeweenStartAndEndHire = GetDateBetweenTwoDates(vechicleStartRentDate, vechcleEndRentDate);

                var bookingDays = GetAllBookingDays(vehicle.Id);

                var allBookingDays = new List<DateTime>();

                for (int i = 0; i < bookingDays.Count; i++)
                {
                    var s = GetDateBetweenTwoDates(Convert.ToDateTime(bookingDays[i].HireDate), Convert.ToDateTime(bookingDays[i].ReturnData));
                    allBookingDays.AddRange(s);
                }

                for (int i = 0; i < dateBeweenStartAndEndHire.Count; i++)
                {
                    for (int j = 0; j < allBookingDays.Count; j++)
                    {
                        if (dateBeweenStartAndEndHire[i] == allBookingDays[j])
                        {
                            dateBeweenStartAndEndHire.RemoveAt(i);
                        }
                    }
                }

                var tripDays = GetDateBetweenTwoDates(startTrip, endTrip);
                Dictionary<string, string> tableOfAllDays = new Dictionary<string, string>();

                bool exists;

                for (int i = 0; i < tripDays.Count; i++)
                {
                    exists = false;

                    for (int j = 0; j < dateBeweenStartAndEndHire.Count; j++)
                    {
                        if (tripDays[i].CompareTo(dateBeweenStartAndEndHire[j]) == 0)
                        {
                            exists = true;
                        }
                    }
                    if (exists)
                        tableOfAllDays.Add(tripDays[i].ToString("dd-MM-yyyy"), "Available");
                    else
                        tableOfAllDays.Add(tripDays[i].ToString("dd-MM-yyyy"), "Not Available");

                }
                return tableOfAllDays;
            }
            return null;
        }
        public string ChangeDateFormatToYearsMonthDays(string date) => Convert.ToDateTime(date).ToString("yyyy-MM-dd");
        public string ChangeDateFormatToDaysMonthYears(string date) => Convert.ToDateTime(date).ToString("dd-MM-yyyy");
        public List<VehicleBooking> GetAllBookingDays(int vId) => Context.VehicleBookings.Where(c => c.VehicleId == vId).ToList();
        public List<DateTime> GetDateBetweenTwoDates(DateTime start, DateTime end)
        {
            List<DateTime> dateBetweenStartAndEndDate = new List<DateTime>();

            for (var date = start; date <= end; date = date.AddDays(1))
            {
                dateBetweenStartAndEndDate.Add(date);
            }
            return dateBetweenStartAndEndDate;
        }
        public List<VehicleBooking> GetAllBookingByUserID(string userId) =>
         Context.VehicleBookings.Where(c => c.RenterId == userId).Include(c => c.Vehicle)
            .ThenInclude(c => c.Model).ThenInclude(c => c.Brand).ToList();

        public void Add(Vehicle vehicle) => Context.Vehicles.Add(vehicle);
        public void Delete(int id) => Context.Vehicles.Remove(GetVehicle(id));

        public void Save() => Context.SaveChanges();

        public List<Vehicle> GetAllUserCar(string ownerId) =>
            Context.Vehicles.Where(c => c.OwnerId == ownerId)
            .Include(c => c.Model).ThenInclude(c => c.Brand).ToList();

        public void VehicleBook(VehicleBooking vehicleBooking)
        {
            Context.VehicleBookings.Add(vehicleBooking);
        }

        public IList<Vehicle> GetAllAvailableVechilces()
        {
            //dont remove First ToList() ^_^ 
            return GetAllVehiclesAccepted().ToList()
                 .Where(c => Convert.ToDateTime(c.EndDate).Date.CompareTo(DateTime.Now.Date) >= 0)
                 .ToList();
        }

        public decimal GetVehiclePricePerDay(int vId)=>
            Context.Vehicles.SingleOrDefault(c => c.Id == vId).PricePerDay;

        public List<Vehicle> GetAllVehicles(string ownerId) =>
            Context.Vehicles.Where(c => c.OwnerId == ownerId).ToList();

        public List<VehicleBooking> GetAllBooking()
        {
            return Context.VehicleBookings.Include(c=>c.Renter).ToList();
        }
    }
}
