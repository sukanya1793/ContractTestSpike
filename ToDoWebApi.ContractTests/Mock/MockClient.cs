using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using ToDoWebapi.Models;

namespace ToDoWebApi.ContractTests.Mock
{
    public class MockClient
    {
        private readonly HttpClient _client;

        public MockClient(string baseUrl = null)
        {
            _client = new HttpClient {BaseAddress = new Uri(baseUrl ?? "http://localhost:5001")};
        }

        public TodoItem GetToDoList(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/ToDoItems/" + id);
            request.Headers.Add("Accept", "application/json");

            var response = _client.SendAsync(request);
            var responseContent = response.Result.Content.ReadAsStringAsync().Result;
            var responseCode = response.Result.StatusCode;
            var reasonPhrase = response.Result.ReasonPhrase;

            request.Dispose();
            response.Dispose();

            if (responseCode == HttpStatusCode.OK)
            {
                return !String.IsNullOrEmpty(responseContent)
                    ? JsonConvert.DeserializeObject<TodoItem>(responseContent)
                    : null;
            }

            throw new Exception(reasonPhrase);
        }
    }
}