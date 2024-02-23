using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
	public class TagModel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; } = null!; // Exempel: "CSharp", "JavaScript"
		public List<TicketModel> Tickets { get; set; } = new();
	}
}
