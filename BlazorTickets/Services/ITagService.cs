using Shared.Models;

namespace BlazorTickets.Services
{
    public interface ITagService
    {

        public HttpClient Client { get; set; }
        Task<List<TagModel>> GetAll();
        Task<TagModel> GetById(int id);
        Task PostTag(TagModel tag);
    }
}
