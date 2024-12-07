using Microsoft.AspNetCore.Identity;

namespace InforceProject.Server.Models.UserModel
{
    public class User : IdentityUser
    {
        public string Role { get; set; }
    }
}
