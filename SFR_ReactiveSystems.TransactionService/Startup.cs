using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StrawberryShake;

namespace SFR_ReactiveSystems.TransactionService
{
    public class Startup : IHostedService
    {
        private readonly IStrawberryShakeClient client;
        private readonly ILogger<Startup> logger;

        public Startup(IStrawberryShakeClient client,
            ILogger<Startup> logger)
        {
            this.client = client;
            this.logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            PaymentMonitor paymentMonitor = new(cancellationToken);

            await Task.Factory.StartNew(ConsumeSubscription,
                cancellationToken,
                cancellationToken,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stopping");
            return Task.CompletedTask;
        }

        public async Task QueryPayments(CancellationToken cancellationToken)
        {
            IOperationResult<IGetAllPaymentsResult>? result = null;
            try
            {
                result = await client.GetAllPayments.ExecuteAsync(cancellationToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error retrieving data");
            }

            if (result is null)
            {
                logger.LogError("Restul is empty");
                return;
            }

            if (result.Errors.Count > 0)
            {
                logger.LogError("Data empty.");
                foreach (var item in result.Errors)
                {
                    logger.LogError("Error: {Error}", item.Message);
                }
            }

            if (result.Data is null)
            {
                logger.LogWarning("Data is empty");
                return;
            }

            foreach (var item in result.Data.Payments.ToArray())
            {
                logger.LogInformation("Id: {@Result}", item);
            }
        }

        public async Task ConsumeSubscription(object? state)
        {
            ArgumentNullException.ThrowIfNull(state);
            CancellationToken cancellationToken = (CancellationToken)state;

            IDisposable session = client.OnNewPayment.Watch().Subscribe(result =>
            {
                logger.LogInformation("Message count: {Count}", result.Data?.Payments.Count.ToString());
            },
            result =>
            {
                logger.LogError("Error: {Message}", result.Message);
            },
            () =>
            {
                logger.LogInformation("Completed!");
            });

            await new TaskCompletionSource<object>().Task;

            session.Dispose();
        }
    }
}
