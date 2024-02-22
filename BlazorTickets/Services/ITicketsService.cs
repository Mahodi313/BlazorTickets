using static BlazorTickets.Pages.TicketCenter;

namespace BlazorTickets.Services
{
	public interface ITicketsService
	{
		public interface ITicketService
		{
			public HttpClient Client { get; set; }

			Task<List<TicketModel>> GetTickets();
			Task PostTicket(TicketModel ticketModel);
			Task UpdateTicket(int ticketId, TicketModel ticketModel);
			Task DeleteTicket(int ticketId);
		}
	}
}
