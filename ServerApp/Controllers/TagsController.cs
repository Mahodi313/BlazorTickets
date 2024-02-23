using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Database;
using Shared.Models;

namespace ServerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly TicketsRepository<TagModel> _tagRepo;

        public TagsController(TicketsRepository<TagModel> tagRepo)
        {
            _tagRepo = tagRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<TagModel>>> GetAllTags()
        {

            var tags = await _tagRepo.GetAll();
            return Ok(tags);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<TagModel>> GetTagById(int id)
        {
            var result = await _tagRepo.Get(id);


            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<TagModel>> AddTag(TagModel newTag)
        {
            await _tagRepo.Add(newTag);

            await _tagRepo.Complete();

            return Ok(newTag);
        }

    }
}
