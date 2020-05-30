using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LendCar.ViewModels
{
    public class ContactViewModel
    {
        [Required,EmailAddress,Display(Name ="Email")]
        public string UserEmail { get; set; }
        [Required, Display(Name = "Subject")]
        public string MailSubjct { get; set; }
        [Required, Display(Name = "Message")]
        public string Message { get; set; }
    }
}
