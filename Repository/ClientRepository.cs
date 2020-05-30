using LendCar.DBContext;
using LendCar.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class ClientRepository : IClientRepository
    {
        public ClientRepository(LendCarDBContext context,
            ICarRepository carRepository,
            IUserRepository userRepository)
        {
            Context = context;
            CarRepository = carRepository;
            UserRepository = userRepository;
        }

        public LendCarDBContext Context { get; }
        public ICarRepository CarRepository { get; }
        public IUserRepository UserRepository { get; }

        public void DeliveringMoneyToVechicleOnwer(string clientId)
        {
            var clientVechicles = CarRepository.GetAllVehicles(clientId);
            var bookingVechicles = CarRepository.GetAllBooking()
                .Where(c => !c.IsBookingCanceled).ToList();

            for (int i = 0; i < clientVechicles.Count; i++)
            {
                for (int j = 0; j < bookingVechicles.Count; j++)
                {
                    if (clientVechicles[i].Id == bookingVechicles[j].VehicleId)
                    {
                        bookingVechicles[j].IsOwnerRecivedHisMoney = true;
                    }
                }
            }

        }

        public List<BookingMoneyHelper> GetUserMoney()
        {
            var bookingVechciles = Context.VehicleBookings
                .Where(c => !c.IsBookingCanceled && !c.IsOwnerRecivedHisMoney).ToList();

            List<BookingHelper> vechicleBookingMoney = new List<BookingHelper>();

            for (int i = 0; i < bookingVechciles.Count; i++)
            {
                if (vechicleBookingMoney.Any(cc => cc.VechicleId == bookingVechciles[i].VehicleId))
                {
                    vechicleBookingMoney.SingleOrDefault(x => x.VechicleId == bookingVechciles[i].VehicleId)
                        .TotalMoney +=
                        CalculateBookingPrice(bookingVechciles[i].ReturnData,
                        bookingVechciles[i].HireDate, bookingVechciles[i].VehicleId);
                }
                else
                {
                    var bookingPrice = CalculateBookingPrice(bookingVechciles[i].ReturnData,
                        bookingVechciles[i].HireDate, bookingVechciles[i].VehicleId);

                    vechicleBookingMoney.Add(new BookingHelper() { VechicleId = bookingVechciles[i].VehicleId, TotalMoney = bookingPrice });
                }
            }

            List<BookingMoneyHelper> clientBooingMoney = new List<BookingMoneyHelper>();
            var Users = UserRepository.GetAllUsers();

            for (int i = 0; i < Users.Count; i++)
            {
                for (int j = 0; j < vechicleBookingMoney.Count; j++)
                {
                    bool? isOwnerOwnedThisVechilcle = (Users[i]?.VehiclesOwnedByHim?.Any(c => c.Id == vechicleBookingMoney[j].VechicleId));
                    if (isOwnerOwnedThisVechilcle.HasValue)
                    {
                        if (isOwnerOwnedThisVechilcle.Value)
                        {
                            if (clientBooingMoney.Exists(c => c.User.Id == Users[i].Id))
                            {

                                clientBooingMoney.SingleOrDefault(c => c.User.Id == Users[i].Id).TotalMoney += vechicleBookingMoney[j].TotalMoney;
                            }
                            else
                            {
                                clientBooingMoney.Add(new BookingMoneyHelper()
                                {
                                    User = Users[i],
                                    TotalMoney = vechicleBookingMoney[j].TotalMoney
                                    - (vechicleBookingMoney[j].TotalMoney / 10)
                                });
                            }
                        }
                    }
                }
            }
            return clientBooingMoney;

        }

        decimal CalculateBookingPrice(string endDate, string startDate, int vechicleId)
            => Convert.ToDecimal((Convert.ToDateTime(endDate).Date -
                         Convert.ToDateTime(startDate))
                         .TotalDays + 1) * CarRepository.GetVehiclePricePerDay(vechicleId);

        public List<BookingMoneyHelper> GetClientsCanceledBooking()
        {
            var client = new List<BookingMoneyHelper>();

            var allBookingProccess = CarRepository.GetAllBooking()
                .Where(c => c.IsBookingCanceled && !c.IsOwnerRecivedHisMoney)
                .ToList();

            for (int i = 0; i < allBookingProccess.Count; i++)
            {
                decimal totalPrice = 0;
                if (client.Any(c => c.User.Id == allBookingProccess[i].RenterId))
                {
                    totalPrice = CalculateBookingPrice(allBookingProccess[i].ReturnData,
                        allBookingProccess[i].HireDate, allBookingProccess[i].VehicleId);

                    client.SingleOrDefault(c => c.User.Id == allBookingProccess[i].RenterId)
                        .TotalMoney += totalPrice - (totalPrice / 5);

                }
                else
                {
                    totalPrice = CalculateBookingPrice(allBookingProccess[i].ReturnData,
                    allBookingProccess[i].HireDate, allBookingProccess[i].VehicleId);

                    client.Add(new BookingMoneyHelper()
                    {

                        User = UserRepository.FindById(allBookingProccess[i].RenterId),
                        TotalMoney =totalPrice-( totalPrice / 5)

                    });
                }
            }

            return client;
        }
        public void Save() => Context.SaveChanges();

        public void CancelMoneyBack(string clientId)
        {

            var bookingVechicles = CarRepository.GetAllBooking()
                .Where(c => !c.IsOwnerRecivedHisMoney && c.IsBookingCanceled && c.RenterId == clientId)
                .ToList();

            for (int i = 0; i < bookingVechicles.Count; i++)
            {
                bookingVechicles[i].IsOwnerRecivedHisMoney = true;
            }

        }
    }
    class BookingHelper
    {
        internal int VechicleId { get; set; }
        internal decimal TotalMoney { get; set; }
    }
    public class BookingMoneyHelper
    {
        public ApplicationUser User { get; set; }
        public decimal TotalMoney { get; set; }
    }
}
