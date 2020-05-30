using LendCar.DBContext;
using LendCar.Models;
using System.Collections.Generic;

namespace LendCar.Repository
{
    public interface IClientRepository
    {
        ICarRepository CarRepository { get; }
        LendCarDBContext Context { get; }

        List<BookingMoneyHelper> GetUserMoney();
        void DeliveringMoneyToVechicleOnwer(string clientId);
        void Save();
        List<BookingMoneyHelper> GetClientsCanceledBooking();
        void CancelMoneyBack(string clientId);
    }
}