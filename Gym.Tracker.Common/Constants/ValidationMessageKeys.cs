using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Common.Constants
{
    public class ValidationMessageKeys
    {
        public const string InvalidEmail = "Invalid Email";
        public const string InvalidPhoneNumber = "Invalid PhoneNumber";
        public const string InvalidPassword = "Invalid Password";
    }
    public class RequiredErrorMessages
    {
        public const string FirstNameRequired = "First name is required";
        public const string LastNameRequired = "Last name is required";
        public const string PhoneNumberRequired = "Phone number is required";
        public const string EmailRequired = "Email is required";
        public const string PasswordRequired = "Password is required";
    }
}
