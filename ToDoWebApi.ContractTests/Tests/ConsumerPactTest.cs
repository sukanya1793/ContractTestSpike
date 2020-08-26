using System.Collections.Generic;
using PactNet.Mocks.MockHttpService;
using PactNet.Mocks.MockHttpService.Models;
using ToDoWebApi.ContractTests.Consumer;
using ToDoWebApi.ContractTests.Mock;
using Xunit;

namespace ToDoWebApi.ContractTests.Tests
{
    public class ConsumerPactTest : IClassFixture<ConsumerPact>
    {
        private IMockProviderService _mockProviderService;
        private string _mockProviderServiceBaseUri;

        public ConsumerPactTest(ConsumerPact consumerPact)
        {
            _mockProviderService = consumerPact.MockProviderService;
            _mockProviderService.ClearInteractions();
            _mockProviderServiceBaseUri = consumerPact.MockProviderServiceBaseUri;
        }

        [Fact]
        public void GetTodoItems_VerifyIfiTReturns()
        {
            //Arrange
            _mockProviderService.Given("ToDoItem for ID 1").UponReceiving("a GET Request to fetch todoitem").With(
                new ProviderServiceRequest
                {
                    Method = HttpVerb.Get,
                    Path = "/api/ToDoItems/1",
                    Headers = new Dictionary<string, object>
                    {
                        {"Accept", "application/json"}
                    }
                }).WillRespondWith(new ProviderServiceResponse
            {
                Status = 200,
                Headers = new Dictionary<string, object>
                {
                    {"Content-Type", "application/json; charset=utf-8"}
                },
                Body = new
                {
                    id = 1,
                    name = "dog",
                    isComplete = true
                }
            });
            
            var consumer = new MockClient(_mockProviderServiceBaseUri);
            
            //Act
            var result = consumer.GetToDoList("1");
            
            //Assert
            Assert.Equal("dog",result.Name);
            _mockProviderService.VerifyInteractions();
        }
    }
}