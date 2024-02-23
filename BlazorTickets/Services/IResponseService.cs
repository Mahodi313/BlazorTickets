using Shared.Models;

namespace BlazorTickets.Services
{
    public interface IResponseService
    {
        public HttpClient Client { get; set; }

        Task PostResponse(ResponseModel response);
    }
}
