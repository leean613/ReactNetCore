﻿using Abstractions.Interfaces.Mail;
using Common.Configurations;
using Common.Exceptions;
using Common.Extentions;
using DTOs.Share;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Services.Implementations.Mail
{
    public class SendMailService : ISendMailService
    {
        private readonly SmtpConfig _config;

        public SendMailService(IOptions<SmtpConfig> config)
        {
            _config = config.Value;
        }

        public async Task SendMails(SendEmailInput inputDto)
        {
            inputDto.Subject = inputDto.Subject.GetValueOrDefault("[Thư gửi không có tiêu đề]");

            var body = inputDto.Content
                + $"</br><p><i>* Đây là email được gửi thông qua hệ thống, nếu có phản hồi, vui lòng gửi về email: binhlyoko@gmail.com </i></p>";

            var emailsToSend = inputDto.RecipientEmails.Concat(inputDto.CcEmails.IfNotNull(x => x, new string[0]))
                .Concat(inputDto.BccEmails.IfNotNull(x => x, new string[0]));

            foreach(var recipientEmail in emailsToSend)
            {
                await SendEmailAsync(
                    "Binh Nguyen",
                    "ndbinh280697@gmail.com",
                    "",
                    recipientEmail,
                    inputDto.Subject,
                    body,
                    null,
                    inputDto.AttachmentFiles);
            }    
        }

        public async Task SendEmailAsync(
            string senderName,
            string senderEmail,
            string recipientName,
            string recipientEmail,
            string subject,
            string body,
            SmtpConfig config = null,
            FileDescription[] attachments = null)
        {
            var from = new MailboxAddress(senderName, _config.EmailAddress);
            var to = new MailboxAddress(recipientName.GetValueOrDefault(string.Empty), recipientEmail);

            await SendEmailAsync(from, new[] { to }, subject, body, config, attachments);
        }

        public async Task SendEmailAsync(
            MailboxAddress sender,
            MailboxAddress[] recipients,
            string subject,
            string body,
            SmtpConfig config = null,
            FileDescription[] attachmentFiles = null)
        {
            MimeMessage message = new MimeMessage();

            message.From.Add(sender);
            message.To.AddRange(recipients);
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = body
            };

            if (!attachmentFiles.IsNullOrEmpty())
            {
                foreach (var attachmentFile in attachmentFiles)
                {
                    builder.Attachments.Add(
                        new MimePart
                        {
                            Content = new MimeContent(new MemoryStream(attachmentFile.Data), ContentEncoding.Default),
                            ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                            ContentTransferEncoding = ContentEncoding.Base64,
                            FileName = attachmentFile.FileName
                        });
                }
            }

            message.Body = builder.ToMessageBody();

            try
            {
                if (config == null)
                    config = _config;

                using (var client = new SmtpClient())
                {
                    if (!config.UseSSL)
                        client.ServerCertificateValidationCallback = (object sender2, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

                    await client.ConnectAsync(config.Host, config.Port, config.UseSSL).ConfigureAwait(false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");

                    if (!string.IsNullOrWhiteSpace(config.Username))
                        await client.AuthenticateAsync(config.Username, config.Password).ConfigureAwait(false);

                    await client.SendAsync(message).ConfigureAwait(false);
                    await client.DisconnectAsync(true).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw new SendEmailException(ex.GetExceptionTechnicalInfo());
            }
        }
    }
}
