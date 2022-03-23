
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.Extensions.Logging;
using SFR_ReactiveSystems.TransactionService.Models;

namespace SFR_ReactiveSystems.TransactionService.GraphQL
{
    public class GraphQLTransactionClient : IDisposable
    {
        private readonly GraphQLHttpClient client;
        private readonly ILogger<GraphQLPaymentClient> logger;

        private IDisposable? subscription;

        public GraphQLTransactionClient(ILogger<GraphQLPaymentClient> logger)
        {
            GraphQLHttpClientOptions options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("http://localhost:8080/v1/graphql")
            };

            client = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
            this.logger = logger;
        }

        public void Observe(IObserver<GraphQLResponse<PaymentSubscriptionResult>> observer)
        {
            GraphQLRequest request = new()
            {
                Query = @"subscription { Payments 
                            {
                              debitorIBAN
                              creditorIBAN
                              amount
                              createdAt
                              id
                            }}"
            };

            subscription = client.CreateSubscriptionStream<PaymentSubscriptionResult>(request).Subscribe(observer);
        }

        public void EndSubscription()
        {
            subscription?.Dispose();
        }

        public void Dispose() => EndSubscription();
    }
}