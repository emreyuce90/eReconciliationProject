using Domain.Concrete.Dtos;

namespace Business.Abstract
{
    public interface IMailSendService
    {
        void SendMailAsync(MailSendDto mailSendDto);

    }
}
