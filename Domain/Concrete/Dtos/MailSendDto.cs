namespace Domain.Concrete.Dtos
{
    public class MailSendDto
    {
        public MailParameter MailParameter { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }


    }
}
