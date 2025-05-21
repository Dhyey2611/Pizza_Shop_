namespace Pizza_Shop_.Models
{
    public class EmailSettings
    {
        public int Id { get; set; }
        public required string  Smtpserver { get; set; }
        public int Smtpport { get; set; }
        public required string  SenderEmail { get; set; }
        public required string  SenderPassword { get; set; }
    }
}
