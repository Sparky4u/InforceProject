using System.ComponentModel.DataAnnotations;

namespace InforceProject.Server.Models.LoginModel
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }   
    }
}
