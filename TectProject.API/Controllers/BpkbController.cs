using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TectProject.API.DTO;
using TectProject.API.DTO.Bpkb.Request;
using TectProject.API.DTO.StorageLocation.Response;
using TectProject.API.Interface;

namespace TectProject.API.Controllers
{
	[ApiController]
	[Route("")]
	public class BpkbController : ControllerBase
	{
		private readonly IBpkbService bpkbService;

		public BpkbController(IBpkbService bpkbService)
		{
			this.bpkbService = bpkbService;
		}
		
		[HttpPost("bpbk")]
		public async Task<ActionResult<BaseResponse<string>>> InsertBpkb(CreateBpkbRequest request)
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			var response = await bpkbService.InsertBpkb(request, userId);

			if (!response.Success)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpGet("bpkb/storage-locations")]
		public async Task<ActionResult<List<StorageLocationResponse>>> GetStorageLocations()
		{
			var locations = await bpkbService.GetStorageLocations();
			return Ok(locations);
		}
	}
}