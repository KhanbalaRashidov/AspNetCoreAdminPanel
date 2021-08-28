using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.UI.Models.Security
{
    public class ResetPasswordViewModel
    {
      
        public string Code {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }

    }
}
