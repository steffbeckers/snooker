namespace Snooker.BackgroundJobs
{
    public class SendEmailJobArgs
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}