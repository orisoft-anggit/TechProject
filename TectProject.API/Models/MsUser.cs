using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TectProject.API.Models
{
	public class MsUser
	{
		[Key]
		public Guid UserId { get; set; }
		
		public string UserName { get; set; }
		
		public byte[] PasswordSalt { get; set; }

		[JsonIgnore]
		public byte[] PasswordHash { get; set; }
		
		public bool IsActive { get; set; }
	}
}