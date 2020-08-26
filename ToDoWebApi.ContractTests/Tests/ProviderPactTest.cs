using System.Collections.Generic;
using PactNet;
using PactNet.Infrastructure.Outputters;
using ToDoWebApi.ContractTests.Outputter;
using Xunit;
using Xunit.Abstractions;

namespace ToDoWebApi.ContractTests.Tests
{
    public class ProviderPactTest
    {
        private readonly ITestOutputHelper _output;

        public ProviderPactTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void MyFact()
        {
            _output.WriteLine("Hello world");
        }

        [Fact]
        public void TestProvider()
        {
            var config = new PactVerifierConfig
            {
                Outputters = new List<IOutput>
                {
                    new XUnitOutput(_output)
                },
                Verbose = true
            };

            IPactVerifier pactVerifier = new PactVerifier(config);
            pactVerifier
                .ServiceProvider("ToDoList", "http://localhost:5001")
                .HonoursPactWith("Service_Consumer")
                .PactUri(
                    @"\Users\sukanyar\RiderProjects\ToDoWebapi\ToDoWebApi.ContractTests\bin\Debug\netcoreapp3.1\~\dotnet\pacts\service_consumer-todolist.json")
                .Verify();
        }
    }
}