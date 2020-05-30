using LendCar.DBContext;
using LendCar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public class ContactRepository : IContactRepository
    {
        public LendCarDBContext Context { get; }
        public ContactRepository(LendCarDBContext context)
        {
            Context = context;
        }
        public Contact GetCompanyContact() => Context.Contact.FirstOrDefault();

    }
}
