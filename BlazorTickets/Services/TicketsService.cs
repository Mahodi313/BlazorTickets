namespace BlazorTickets.Services;

using System.Net.Http.Json;
using Newtonsoft.Json;

using Shared.Models;

public class TicketsService : ITicketsService
{
	public HttpClient Client { get; set; } = new()
	{
		BaseAddress = new Uri("https://localhost:7193/api/")
	};
	public async Task<List<TicketModel>> GetTickets()
	{
		var response = await Client.GetAsync("tickets");

		if (response.IsSuccessStatusCode)
		{
			string ticketssJson = await response.Content.ReadAsStringAsync();

			List<TicketModel>? tickets = JsonConvert.DeserializeObject<List<TicketModel>>(ticketssJson);

			if (tickets != null)
			{
				return tickets;
			}
		}
		throw new HttpRequestException();
	}
	public async Task PostTicket(TicketModel ticketModel)
	{

		await Client.PostAsJsonAsync("Tickets", ticketModel);
	}
	public async Task UpdateTicket(int ticketId, TicketModel ticketModel)
	{
		var response = await Client.PutAsJsonAsync($"tickets/{ticketId}", ticketModel);
		if (!response.IsSuccessStatusCode)
		{
			throw new HttpRequestException();
		}
	}

	public async Task DeleteTicket(int ticketId)
	{
		var response = await Client.DeleteAsync($"tickets/{ticketId}");
		if (!response.IsSuccessStatusCode)
		{
			throw new HttpRequestException();
		}
	}
}



