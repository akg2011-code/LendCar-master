using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace LendCar.Controllers
{
    [Route("Payment")]
    public class PaymentController : Controller
    {
        public PaymentController(ICarRepository ICarRepository)
        {
            this.ICarRepository = ICarRepository;
        }

        public ICarRepository ICarRepository { get; }

        [Route("Pay")]
        public IActionResult Pay(VehicleBooking bookingInfo, long amount, string stripeEmail, string stripeToken)
        {
            var c = new CustomerService();
            var customer = c.Create(new CustomerCreateOptions()
            {
                Email = stripeEmail,
                Source = stripeToken
            });
            var ch = new ChargeService();
            var change = ch.Create(new ChargeCreateOptions
            {
                Amount = amount*100,
                Currency = "usd",
                Description = "Vechile booking ",
                Customer = customer.Id,
                ReceiptEmail = stripeEmail
            });
            if (change.Status == "succeeded")
            {
                ICarRepository.VehicleBook(new VehicleBooking()
                {
                    HireDate=bookingInfo.HireDate,
                    ReturnData=bookingInfo.ReturnData,
                    VehicleId=bookingInfo.VehicleId,
                    RenterId=bookingInfo.RenterId
                });
                ICarRepository.Save();

                return RedirectToPage("succeeded");
            }
            return RedirectToPage("failed");
        }
    }
}