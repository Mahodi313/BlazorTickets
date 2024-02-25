using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shared.Models
{
	public class TicketModel
	{
		[Key]
		public int Id { get; set; }
		public string Title { get; set; } = null!;
		public string? Description { get; set; }
		public string SubmittedBy { get; set; } = null!;
		public bool IsResolved { get; set; }
        public List<TagModel> Tags { get; set; } = new();
        public List<ResponseModel> Responses { get; set; } = new();

	}
}
