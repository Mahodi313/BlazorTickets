using Newtonsoft.Json;
using Shared.Models;
using System.Net.Http.Json;

namespace BlazorTickets.Services
{
    public class TagService : ITagService
    {
        public HttpClient Client { get; set; } = new()
        {
            BaseAddress = new Uri("https://localhost:7193/api/")
        };

        public async Task<List<TagModel>> GetAll()
        {
            var response = await Client.GetAsync("tags");

            if (response.IsSuccessStatusCode)
            {
                string tagsJson = await response.Content.ReadAsStringAsync();

                List<TagModel>? tags = JsonConvert.DeserializeObject<List<TagModel>>(tagsJson);

                if (tags != null)
                {
                    return tags;
                }
            }

            throw new HttpRequestException();
        }

        public async Task<TagModel> GetById(int id)
        {
            var response = await Client.GetAsync($"tags/{id}");

            if (response.IsSuccessStatusCode)
            {
                string tagJson = await response.Content.ReadAsStringAsync();

                TagModel? tag = JsonConvert.DeserializeObject<TagModel>(tagJson);

                if (tag != null)
                {
                    return tag;
                }
            }

            throw new HttpRequestException();
        }

        public async Task PostTag(TagModel tag)
        {
            await Client.PostAsJsonAsync("Tags", tag);
        }
    }
}
