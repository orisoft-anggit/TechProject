using System.ComponentModel.DataAnnotations;

namespace TectProject.API.DTO.Auth.Request
{
    public class CreateRegisterRequest
    {
        [Required(ErrorMessage = "you must fill the username")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "username is not valid")]
        [StringLength(16, ErrorMessage = "Must be between 3 and 16 characters", MinimumLength = 3)]
        public string UserName {get; set;}       

        [Required(ErrorMessage = "you must fill the password")]
        [StringLength(20, ErrorMessage = "Must be between 5 and 20 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password  {get; set;}
    }
}