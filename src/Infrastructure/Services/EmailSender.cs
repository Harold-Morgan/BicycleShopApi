using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // TODO: Добавить непосредственную реализацию интерфейса (SMTPEmailSender и т.д и т.п.)
            return Task.CompletedTask;
        }
    }
}
