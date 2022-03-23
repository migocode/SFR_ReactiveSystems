using GraphQL;
using SFR_ReactiveSystems.TransactionService.Models;
using System.Reactive.Linq;

namespace SFR_ReactiveSystems.TransactionService
{
    public class PaymentMonitor : IObserver<GraphQLResponse<PaymentSubscriptionResult>>
    {
        public CancellationToken CancellationToken { get; }

        public PaymentMonitor(CancellationToken cancellationToken)
        {
            this.CancellationToken = cancellationToken;
        }

        public void OnCompleted()
        {
            Console.WriteLine("Observer Completed!");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine("Observer Error!");
        }

        public void OnNext(GraphQLResponse<PaymentSubscriptionResult> paymentResponse)
        {
            Console.WriteLine($"{paymentResponse.Data}");
        }

        public void Subscribe(IObservable<GraphQLResponse<PaymentSubscriptionResult>> provider)
        {
            provider
                .Delay(new TimeSpan(0,0,2))
                .Subscribe(this, CancellationToken);
        }
    }
}
