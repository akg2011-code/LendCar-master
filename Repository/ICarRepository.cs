using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public interface ICarRepository
    {
        LendCarDBContext Context { get; }

        Vehicle GetVehicle(int id);
        IQueryable<Vehicle> GetAllVehiclesAccepted();
        IList<Vehicle> GetAllAvailableVechilces();
        IQueryable<Vehicle> GetAllVehiclesRequests();
        void Add(Vehicle vehicle);
        void Delete(int id);
        void Save();
        string ChangeDateFormatToYearsMonthDays(string date);
        string ChangeDateFormatToDaysMonthYears(string date);
        Dictionary<string, string> AvailableDays(string startDate, string endDate, int carId);
        List<VehicleBooking> GetAllBookingByUserID(string userId);
        List<Vehicle> GetAllUserCar(string userID);
        void VehicleBook(VehicleBooking vehicleBooking);
        decimal GetVehiclePricePerDay(int vId);
        List<Vehicle> GetAllVehicles(string ownerId);
        List<VehicleBooking> GetAllBooking();
    }
}
