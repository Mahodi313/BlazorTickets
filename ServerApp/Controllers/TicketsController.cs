using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerApp.Database;
using Shared.Models;

namespace ServerApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TicketsController : ControllerBase
	{

        //Inject TicketsRepository to access Methods 

        private readonly TicketsRepository<TicketModel> _ticketRepo;
		private readonly TicketsDbContext _DbContext;

		public TicketsController(TicketsRepository<TicketModel> ticketRepo, TicketsDbContext DbContext)
		{
			_ticketRepo = ticketRepo;
			_DbContext = DbContext;
		}

		[HttpGet]
		public async Task<ActionResult<List<TicketModel>>> GetAllTicketsWithResponses()
		{

			var tickets = await _ticketRepo.GetAllInclude("Responses", "Tags");
			return Ok(tickets);
		}

		[HttpGet("{id}")]

		public async Task<ActionResult<TicketModel>> GetTicketById(int id)
		{
			var result = await _ticketRepo.Get(id);


			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

        [HttpPost]
        public async Task<ActionResult<TicketModel>> AddTicket(TicketModel newTicket)
        {
            if (newTicket.Tags != null)
            {
                var processedTags = new List<TagModel>();
                foreach (var tag in newTicket.Tags)
                {
                    var existingTag = await _DbContext.Tags.FirstOrDefaultAsync(t => t.Id == tag.Id);
                    if (existingTag != null)
                    {
                        processedTags.Add(existingTag);
                    }
                    else
                    {
                        processedTags.Add(tag); 
                    }
                }
                newTicket.Tags = processedTags; 
            }

            _DbContext.Ticket.Add(newTicket); 
            await _DbContext.SaveChangesAsync();

            return Ok(newTicket);
        }


        [HttpDelete("{id}")]
		public async Task<ActionResult<TicketModel>> DeleteTicket(int id)
		{
			var result = await _ticketRepo.Delete(id);

			if (result == null)
			{
				return NotFound();
			}

			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<TicketModel>> UpdateTicket(TicketModel updatedTicket)
		{
            var dbTicket = await _ticketRepo.Get(updatedTicket.Id);


            if (dbTicket == null)
			{
				return NotFound();
			}

			dbTicket.Title = updatedTicket.Title;
			dbTicket.Description = updatedTicket.Description;
			dbTicket.SubmittedBy = updatedTicket.SubmittedBy;
			dbTicket.IsResolved = updatedTicket.IsResolved;

            _DbContext.Entry(dbTicket).State = EntityState.Modified;
            await _ticketRepo.Complete();
            
            return Ok(dbTicket);
		}
    }
}

