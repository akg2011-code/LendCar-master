using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LendCar.Controllers
{
    [Route("Client")]
    public class ClientController : Controller
    {
        public ClientController(IClientRepository clientRepository)
        {
            ClientRepository = clientRepository;
        }

        public IClientRepository ClientRepository { get; }

        [Route("Deliver")]
        public PartialViewResult DeliverMoneyToClient(string clientId)
        {
            ClientRepository.DeliveringMoneyToVechicleOnwer(clientId);
            ClientRepository.Save();
            return PartialView("_ClientMoney", ClientRepository.GetUserMoney());
        }
        [Route("Cancel")]
        public PartialViewResult ClientCancelMoney(string clientId)
        {
            ClientRepository.CancelMoneyBack(clientId);
            ClientRepository.Save();
            return PartialView("_ClientCancel", ClientRepository.GetClientsCanceledBooking());
        }
    }
}