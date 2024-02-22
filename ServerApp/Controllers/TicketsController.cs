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

		//injectar TicketsDbContext så att databasen updateras 
		//TODO: använda repository för databas metoder och kalla på dem i Controller

		private readonly TicketsDbContext _context;

		public TicketsController(TicketsDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<List<TicketModel>>> GetAllTickets()
		{
			return await _context.Ticket.ToListAsync();
		}

		[HttpGet("{id}")]

		public async Task<ActionResult<TicketModel>> GetTicketById(int id)
		{
			var result = await _context.Ticket.FindAsync(id);

			if (result == null)
			{
				return NotFound();
			}
			return Ok(result);
		}

		[HttpPost]
		public async Task<ActionResult<TicketModel>> AddTicket(TicketModel newTicket)
		{
			_context.Add(newTicket);
			await _context.SaveChangesAsync();

			return Ok(newTicket);
		}


		[HttpDelete("{id}")]

		public async Task<ActionResult<TicketModel>> DeleteTicket(int id)
		{
			var result = await _context.Ticket.FindAsync(id);

			if (result == null)
			{
				return NotFound();
			}
			_context.Remove(result);
			await _context.SaveChangesAsync();

			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<TicketModel>> UpdateTicket(int id, TicketModel updatedTicket)
		{
			var dbTicket = await _context.Ticket.FindAsync(id);

			if (dbTicket == null)
			{
				return NotFound();
			}
			//Uppdatera id?
			dbTicket.Title = updatedTicket.Title;
			dbTicket.Description = updatedTicket.Description;
			dbTicket.SubmittedBy = updatedTicket.SubmittedBy;
			dbTicket.IsResolved = updatedTicket.IsResolved;

			await _context.SaveChangesAsync();

			return Ok(dbTicket);
		}


	}
}
