using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.Repository
{
    public interface IEmail
    {
         void SendEmail(string to, string subject, string emailBody);
    }
}
