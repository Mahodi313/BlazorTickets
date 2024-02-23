using Microsoft.AspNetCore.Mvc;
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

		public TicketsController(TicketsRepository<TicketModel> ticketRepo)
		{
			_ticketRepo = ticketRepo;
		}

		[HttpGet]
		public async Task<ActionResult<List<TicketModel>>> GetAllTicketsWithResponses()
		{

			var tickets = await _ticketRepo.GetAllInclude("Responses");
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
			await _ticketRepo.Add(newTicket);

			await _ticketRepo.Complete();

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

		[HttpPut("{id}")]
		public async Task<ActionResult<TicketModel>> UpdateTicket(int id, TicketModel updatedTicket)
		{
			var dbTicket = await _ticketRepo.Get(id);


			if (dbTicket == null)
			{
				return NotFound();
			}

			dbTicket.Title = updatedTicket.Title;
			dbTicket.Description = updatedTicket.Description;
			dbTicket.SubmittedBy = updatedTicket.SubmittedBy;
			dbTicket.IsResolved = updatedTicket.IsResolved;

			await _ticketRepo.Complete();

			return Ok(dbTicket);
		}


	}
}

