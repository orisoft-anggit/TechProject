using System.ComponentModel.DataAnnotations;

namespace TectProject.API.Models
{
	public class MsStorageLocation
	{   
		[Key]
		public string LocationId { get; set; }
		public string LocationName { get; set; }

		public ICollection<TrBpkb> TrBpkbs { get; set; }
	}
}