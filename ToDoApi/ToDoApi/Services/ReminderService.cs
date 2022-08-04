using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using ToDoApi.Options;
using ToDoCore;
using ToDoInfrastructure;

namespace ToDoApi.Services
{
    public class ReminderService : IHostedService, IDisposable
    {
        private readonly SendGridOptions _sendGridOptions;
        private Timer? _timer = null;
        private readonly IServiceScopeFactory scopeFactory;

        public ReminderService(IOptions<SendGridOptions> sendGridOptions, IServiceScopeFactory scopeFactory)
        {
            _sendGridOptions = sendGridOptions.Value;
            this.scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(_sendGridOptions.Interval));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private void DoWork(object? state) { 
            using var scope = scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ToDoListContext>();
            var lists = dbContext.ToDoLists.Where( l=> l.ReminderDate < DateTime.Now && !l.IsReminded).ToList();
            lists.ForEach(async l => await SendEmailAsync(l));
            lists.ForEach(l => l.IsReminded = true);
            dbContext.SaveChanges();
        }
        public void Dispose()
        {
            _timer?.Dispose();
        }

        private async Task SendEmailAsync(ToDoList list)
        {
            var client = new SendGridClient(_sendGridOptions.ApiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_sendGridOptions.From, _sendGridOptions.Name),
                Subject = _sendGridOptions.Subject,
                PlainTextContent = String.Format(_sendGridOptions.Content, list.Id)
            };
            msg.AddTo(new EmailAddress("nevena.atic99@gmail.com"));
            var response = await client.SendEmailAsync(msg);
        }
    }
}
