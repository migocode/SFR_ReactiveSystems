using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Serilog;
using SFR_ReactiveSystems.TransactionService.Models;

namespace SFR_ReactiveSystems.TransactionService.GraphQL
{
    public class GraphQLPaymentClient
    {
        private readonly GraphQLHttpClient client;
        private readonly ILogger logger;

        public GraphQLPaymentClient(ILogger logger)
        {
            GraphQLHttpClientOptions options = new()
            {
                EndPoint = new Uri("ws://graphql-engine:8080/v1/graphql")
            };

            client = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
            this.logger = logger;
        }

        public IObservable<GraphQLResponse<PaymentSubscriptionResult>> SetUpSubscription()
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

            return client.CreateSubscriptionStream<PaymentSubscriptionResult>(request, StreamExceptionHandler);
        }

        private void StreamExceptionHandler(Exception e)
        {
            logger.Error(e, "Subscription Error");
        }
    }
}