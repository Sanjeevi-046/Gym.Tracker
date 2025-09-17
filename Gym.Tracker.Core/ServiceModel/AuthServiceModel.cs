using Gym.Tracker.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Core.ServiceModel
{
    public class AuthResponse
    {
        public bool IsSuccess { get; set; } = false;
        public string SigninName { get; set; } = string.Empty;
        public List<string> Permissions { get; set; } = new List<string>();
        public long UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public long RoleId { get; set; }
    }
    public class LoginRequest
    {
        [Required(ErrorMessage = RequiredErrorMessages.EmailRequired)]
        public string? Email { get; set; }
        [Required(ErrorMessage = RequiredErrorMessages.PasswordRequired)]
        public string? Password { get; set; }
    }
}
