using Common.Configurations;
using DTOs.Share;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Abstractions.Interfaces.Mail
{
    public interface ISendMailService
    {
        Task SendMails(SendEmailInput inputDto);

        Task SendEmailAsync(
            string senderName,
            string senderEmail,
            string recipientName,
            string recipientEmail,
            string subject,
            string body,
            SmtpConfig config = null,
            FileDescription[] attachments = null);
    }
}
