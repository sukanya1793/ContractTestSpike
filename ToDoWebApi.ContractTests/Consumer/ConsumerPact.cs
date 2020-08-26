using System;
using PactNet;
using PactNet.Mocks.MockHttpService;

namespace ToDoWebApi.ContractTests.Consumer
{
    public class ConsumerPact : IDisposable
    {
        public IPactBuilder PactBuilder { get; private set; }
        public IMockProviderService MockProviderService { get; private set; }

        public int MockServerPort
        {
            get { return 9224; }
        }

        public string MockProviderServiceBaseUri
        {
            get { return String.Format("http://localhost:{0}", MockServerPort); }
        }

        public ConsumerPact()
        {
            var pactConfig = new PactConfig
            {
                // SpecificationVersion = "2.0.0",
                PactDir = "~/dotnet/pacts",
                LogDir = "~/dotnet/logs"
            };

            PactBuilder = new PactBuilder(pactConfig).ServiceConsumer("Service_Consumer").HasPactWith("ToDoList");

            MockProviderService = PactBuilder.MockService(MockServerPort);
        }

        public void Dispose()
        {
            PactBuilder.Build();
        }
    }
}