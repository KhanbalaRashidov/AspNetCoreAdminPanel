using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.UI.Services
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; } 

        public int SmtpPort { get; set; } 

        public string SmtpUserName { get; set; } 

        public string Password { get; set; } 
    }
}
