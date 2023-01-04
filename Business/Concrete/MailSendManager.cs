using Business.Abstract;
using Domain.Concrete.Dtos;
using System.Net;
using System.Net.Mail;

namespace Business.Concrete
{
    public class MailSendManager : IMailSendService
    {
        public void SendMailAsync(MailSendDto mailSendDto)
        {
            using var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailSendDto.MailParameter.EMail);
            mailMessage.To.Add(mailSendDto.ToEmail);
            mailMessage.Subject= mailSendDto.Subject;
            mailMessage.Body= mailSendDto.Body;
            mailMessage.IsBodyHtml = true;

            using var smtpClient = new SmtpClient(mailSendDto.MailParameter.SMTP);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(mailSendDto.MailParameter.EMail, mailSendDto.MailParameter.Password);
            smtpClient.EnableSsl = mailSendDto.MailParameter.SSL;
            smtpClient.Send(mailMessage);
        }
    }
}
