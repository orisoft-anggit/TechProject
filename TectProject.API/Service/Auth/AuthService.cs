using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TectProject.API.Data;
using TectProject.API.DTO;
using TectProject.API.DTO.Auth.Request;
using TectProject.API.DTO.Auth.Response;
using TectProject.API.Interface;
using TectProject.API.Models;

namespace TectProject.API.Service.Auth
{
	public class AuthService : IAuthService
	{
		private readonly ApplicationDbContext context;
		private readonly IConfiguration configuration;
		private readonly IMapper mapper;

		public AuthService(ApplicationDbContext context, IConfiguration configuration, IMapper mapper)
		{
			this.context = context;
			this.configuration = configuration;
			this.mapper = mapper;
		}
		
		public async Task<BaseResponse<CreateRegisterResponse>> Register(CreateRegisterRequest request)
		{
			//define response
			var response = new BaseResponse<CreateRegisterResponse>();
			
			//check is username 
			if (await IsUserExistByUserName(request.UserName))
			{
				response.Success = false;
				response.Message = "the username is already used";
				return response;
			}
			
			//define method for create password hash this is username
			CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
			
			var userEntity = mapper.Map<MsUser>(request);
			userEntity.IsActive = true;
			
			userEntity.PasswordHash = passwordHash;
			userEntity.PasswordSalt = passwordSalt;
			
			//added users 
			context.MsUsers.Add(userEntity);
			
			//save data to db
			await context.SaveChangesAsync();
			
			response.Data = mapper.Map<CreateRegisterResponse>(userEntity);
			response.Data.Password = request.Password;
			response.Success = true;
			response.Message = "user added successfully";
			
			return response;
		}

		private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

		public async Task<BaseResponse<string>> Login(CreateLoginRequest request)
		{
			BaseResponse<string> response = new BaseResponse<string>();
			
			//find user by emails
			var userEntity = await context.MsUsers
				.FirstOrDefaultAsync(x => x.UserName.Equals(request.UserName));

			//check user 
			if (userEntity.UserName == null)
			{
				response.Success = false;
				response.Message = "User Not Found";
			}
			
			//verify this is password by login
			else if (!VerifyPasswordHash(request.Password, userEntity.PasswordHash, userEntity.PasswordSalt))
			{
				response.Success = false;
				response.Message = "Wrong Password";
			}
			else
			{
				//create token and succes by login
				response.Data = CreateToken(userEntity);
				response.Success = true;
				response.Message = "Login Successful";
			}
			return response;
		}

		private string CreateToken(MsUser userEntity)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, userEntity.UserId.ToString()),
				new Claim(ClaimTypes.Name, userEntity.UserName),
			};

			SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8
				.GetBytes(configuration.GetSection("AppSetting:Token").Value));

			SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

			SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddDays(1),
				SigningCredentials = creds
			};

			JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
			SecurityToken Token = TokenHandler.CreateToken(TokenDescriptor);

			return TokenHandler.WriteToken(Token);
		}

		private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
			{
				var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
				return computeHash.SequenceEqual(passwordHash);
			}
		}

		public async Task<bool> IsUserExistByUserName(string userName)
		{
			if (await context.MsUsers.AnyAsync(x => x.UserName == userName))
			{
				return true;
			}
			return false;
		}
	}
}