using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.UI.Services
{
    public interface IMailService
    {
        void Send(EmailMessage emailMessage);
    }
}
