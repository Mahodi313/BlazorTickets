using Shared.Models;
using System.Net.Http.Json;

namespace BlazorTickets.Services
{
    public class ResponseService : IResponseService
    {
        public HttpClient Client { get; set; } = new()
        {
            BaseAddress = new Uri("https://localhost:7193/api/")
        };

        public async Task PostResponse(ResponseModel newResponse)
        {
            await Client.PostAsJsonAsync("Responses", newResponse);
        }
    }
}
