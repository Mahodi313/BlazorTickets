using Microsoft.AspNetCore.Mvc;
using ServerApp.Database;
using Shared.Models;

namespace ServerApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ResponsesController : ControllerBase
	{
		private readonly TicketsRepository<ResponseModel> _responseRepo;

		public ResponsesController(TicketsRepository<ResponseModel> responseRepo)
		{
			_responseRepo = responseRepo;
		}

		[HttpGet]
		public async Task<ActionResult<List<ResponseModel>>> GetAllResponses()
		{
			var responses = await _responseRepo.GetAll();
			return Ok(responses);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ResponseModel>> GetResponseById(int id)
		{
			var response = await _responseRepo.Get(id);
			if (response == null)
			{
				return NotFound();
			}
			return Ok(response);
		}

		[HttpPost]
		public async Task<ActionResult<ResponseModel>> AddResponse(ResponseModel newResponse)
		{
			await _responseRepo.Add(newResponse);
			await _responseRepo.Complete();
			return Ok(newResponse);
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult<ResponseModel>> DeleteResponse(int id)
		{
			var response = await _responseRepo.Delete(id);
			if (response == null)
			{
				return NotFound();
			}
			return Ok(response);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult<ResponseModel>> UpdateResponse(int id, ResponseModel updatedResponse)
		{
			var dbResponse = await _responseRepo.Get(id);
			if (dbResponse == null)
			{
				return NotFound();
			}
			dbResponse.Response = updatedResponse.Response;
			dbResponse.SubmittedBy = updatedResponse.SubmittedBy;

			await _responseRepo.Complete();
			return Ok(dbResponse);
		}
	}
}