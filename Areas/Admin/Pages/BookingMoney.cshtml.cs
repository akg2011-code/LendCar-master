using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Areas.Admin.Pages
{
    public class BookingMoneyModel : PageModel
    {
        public BookingMoneyModel(IClientRepository clientRepository, UserManager<ApplicationUser> s)
        {
            ClientRepository = clientRepository;
            S = s;
        }

        public IClientRepository ClientRepository { get; }
        public UserManager<ApplicationUser> S { get; }
        public List<BookingMoneyHelper> Clients { get; private set; }
        public List<BookingMoneyHelper> ClientCanceled { get;private set; }
        public void OnGet()
        {
            Clients = ClientRepository.GetUserMoney();
            ClientCanceled = ClientRepository.GetClientsCanceledBooking();
        }
    }
}