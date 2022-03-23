using GraphQL;
using GraphQL.Client.Abstractions.Websocket;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL.Client.Serializer.SystemTextJson;
using SFR_ReactiveSystems.Frontend.Client.Models;
using GraphQL.Client.Abstractions;

namespace SFR_ReactiveSystems.Frontend.Client.GraphQL
{
    public class GraphQLClient
    {
        private readonly GraphQLHttpClient client;
        private readonly ILogger<GraphQLClient> logger;

        public GraphQLClient(ILogger<GraphQLClient> logger)
        {
            GraphQLHttpClientOptions options = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri("http://localhost:8080/v1/graphql")
            };

            client = new GraphQLHttpClient(options, new NewtonsoftJsonSerializer());
            this.logger = logger;
        }

        public async Task<int> SendAsync(Payment payment)
        {
            string query = @"mutation ($payment: Payments_insert_input!) { 
                              insert_Payments_one(object: $payment) 
                              { id } }";

            logger.LogInformation(query);

            logger.LogInformation($"Inserting {payment}");

            GraphQLRequest request = new GraphQLRequest
            {
                Query = query,
                Variables = new { payment }
            };

            int paymentId = -1;
            try
            {
                GraphQLResponse<ResponseType> response = await client.SendMutationAsync<ResponseType>(request);
                logger.LogInformation($"Payment submitted. Response {response.Data}");

                paymentId = response.Data.Insert_Payments_One.Id;

                foreach (GraphQLError error in response.Errors ?? Array.Empty<GraphQLError>())
                {
                    logger.LogError(error.Message);
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Could not send");
            }

            return paymentId;
        }
    }
}
