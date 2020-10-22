using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace TestApp.Mocking
{

    public class SendGridMessageClient : IMessageClient
    {
        private readonly ISendGridClient client;
        private readonly ILogger logger;

        public SendGridMessageClient(ISendGridClient sendGridClient, ILogger logger)
        {
            this.client = sendGridClient;
            this.logger = logger;
        }

        public async Task SendAsync(User sender, User recipient, SalesReport report)
        {
            var message = MailHelper.CreateSingleEmail(
                  new EmailAddress(sender.Email, $"{sender.FirstName} {sender.LastName}"),
                  new EmailAddress(recipient.Email, $"{recipient.FirstName} {recipient.LastName}"),
                  "Raport sprzedaży",
                  report.ToString(),
                  report.ToHtml());


            logger.Info($"Wysyłanie raportu do {recipient.FirstName} {recipient.LastName} <{recipient.Email}>...");

            var response = await client.SendEmailAsync(message);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                /// ReportSent?.Invoke(this, new ReportSentEventArgs(DateTime.Now));

                logger.Info($"Raport został wysłany.");
            }
            else
            {
                logger.Error($"Błąd podczas wysyłania raportu.");

                throw new ApplicationException("Błąd podczas wysyłania raportu.");
            }
        }
    }





}