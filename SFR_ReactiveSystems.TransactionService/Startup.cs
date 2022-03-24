using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace SFR_ReactiveSystems.TransactionService
{
    public class Startup : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<Startup> logger;

        public Startup(IServiceProvider serviceProvider,
            ILogger<Startup> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            IStrawberryShakeClient? client = serviceProvider.GetService<IStrawberryShakeClient>();

            if (client is null)
            {
                throw new NullReferenceException("GrapQL client was null");
            }

            client.OnNewPayment.Watch().Subscribe(result =>
            {
                IOnNewPayment_Payments? payment = result.Data?.Payments[0];

                if (payment is null)
                {
                    return;
                }

                logger.LogInformation("Message count: {@Message}", payment);

                Transactions_insert_input input = new()
                {
                    Amount = payment.Amount,
                    CreatedAt = payment.CreatedAt,
                    CreditorIBAN = payment.CreditorIBAN,
                    DebitorIBAN = payment.DebitorIBAN,
                    Id = payment.Id,
                };

                client.PersistTransaction
                    .ExecuteAsync(input, cancellationToken)
                    .Wait();
            },
            result =>
            {
                logger.LogError("Error: {Message}", result.Message);
            },
            () =>
            {
                logger.LogInformation("Completed!");
            }, cancellationToken);

            await cancellationToken;
        }
    }
}
