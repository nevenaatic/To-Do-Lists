namespace ToDoApi.Options
{
    public class SendGridOptions
    {

        public const string SendGrid = "SendGrid";

        public string From { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public int Interval { get; set; }
    }
}
