using System.ComponentModel.DataAnnotations;

namespace TectProject.API.DTO.Auth.Request
{
    public class CreateLoginRequest
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "you must fill the user name")]
        public string UserName {get; set;}

        [Required(ErrorMessage = "you must fill the password")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}