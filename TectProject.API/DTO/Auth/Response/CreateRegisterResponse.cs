namespace TectProject.API.DTO.Auth.Response
{
    public class CreateRegisterResponse
    {
        public Guid UserId {get; set;}
        
        public string UserName {get; set;}

        public string Password {get; set;}
    }
}