using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TectProject.API.DTO.Auth.Request;
using TectProject.API.DTO.Auth.Response;
using TectProject.API.Interface;

namespace TectProject.API.Controllers
{
	[Route("")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService authService;

		public AuthController(IAuthService authService)
		{
			this.authService = authService;
		}
		
		[AllowAnonymous]
		[HttpPost("register")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<CreateRegisterResponse>>Register(CreateRegisterRequest request)
		{
			var response = await authService.Register(request);
			if(!response.Success)
			{
				return StatusCode(StatusCodes.Status400BadRequest, response); 
			}
			return StatusCode(StatusCodes.Status200OK, response);
		}
		
		[AllowAnonymous]
		[HttpPost("login")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<CreateLoginResponse>>Login(CreateLoginRequest request)
		{
			var response = await authService.Login(request);
			if(!response.Success)
			{
				return StatusCode(StatusCodes.Status400BadRequest, response); 
			}
			return StatusCode(StatusCodes.Status200OK, response);
		}
	}
}