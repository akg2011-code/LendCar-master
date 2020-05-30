using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LendCar.Models;
using LendCar.Repository;
using LendCar.ViewModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LendCar.Pages
{
    public class contactModel : PageModel
    {
        public Contact Contact { get; set; }
        public IContactRepository ContactRep { get; }
        public IEmail Email { get; }

        [BindProperty,BindRequired]
        public ContactViewModel ContactViewModel { get; set; }
        public contactModel(IContactRepository contactRep, IEmail email)
        {
            ContactRep = contactRep;
            Email = email;
            Contact = ContactRep.GetCompanyContact();
        }

        public  void OnPost()
        {
            if (ModelState.IsValid) 
            {
                 Email.SendEmail("ahmedmoneim094@gmail.com", 
                    ContactViewModel.MailSubjct, ContactViewModel.UserEmail+"\n\n"+ContactViewModel. Message);
            }
        }
    }
}