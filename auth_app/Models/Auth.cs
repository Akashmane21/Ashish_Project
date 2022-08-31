using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace auth_app.Models
{
    public class Auth
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string location { get; set; }
        public string language { get; set; }
        public string Role { get; set; }
        public string userpin { get; set; }
    }

    [Keyless]
    public class Login
    {
        
        public string UserName { get; set; }
    
        public string Password { get; set; }
    }
}
