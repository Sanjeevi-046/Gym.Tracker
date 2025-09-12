using Gym.Tracker.Common.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Tracker.Core.ServiceModel
{
    [ExcludeFromCodeCoverage]
    public class UserResponseModel : EntityServiceModel
    {
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public long RoleId { get; set; } 
        public long? CountryId { get; set; }
        public long? UserTypeId { get; set; }
        public long? BillingTermsId { get; set; }
        public long? PaymentMethodId { get; set; }
        public long? Nationality { get; set; }

        // Security
        public string Password { get; set; } = null!;

        public bool? IsTerminated { get; set; }
        public bool? IsUserLocked { get; set; }
        public bool? IsForcePwdChange { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? ContactInformation { get; set; }
        public string? BillingAddress { get; set; }
        public string? MailingAddress { get; set; }
        public bool? IsMailNotificationAllowed { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class UserRequestModel
    {
        public long Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessages.FirstNameRequired)]
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        [Required(ErrorMessage =RequiredErrorMessages.LastNameRequired)]
        public string? LastName { get; set; }
        
        [Required(ErrorMessage = RequiredErrorMessages.EmailRequired)]
        [RegularExpression(RegularExpressionConstants.Email,ErrorMessage = ValidationMessageKeys.InvalidEmail)]
        public string Email { get; set; } = null!;
        [RegularExpression(RegularExpressionConstants.Password, ErrorMessage = ValidationMessageKeys.InvalidPassword)]
        public string Password { get; set; } = null!;

        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(20, ErrorMessage = "Phone number cannot be longer than 20 characters")]
        public string? PhoneNumber { get; set; }

        [StringLength(20, ErrorMessage = "Gender cannot be longer than 20 characters")]
        public string? Gender { get; set; }
        public long? CountryId { get; set; }
        public long? UserTypeId { get; set; }
        public long? BillingTermsId { get; set; }
        public long? PaymentMethodId { get; set; }
        public long? Nationality { get; set; }
        public long RoleId { get; set; } 
        public bool? IsTerminated { get; set; }
        public bool? IsUserLocked { get; set; }
        public bool? IsForcePwdChange { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? ContactInformation { get; set; }
        public bool IsActive { get; set; } = true;
        public bool? IsMailNotificationAllowed { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class UserVerificationResult
    {
        public bool IsNewUser { get; set; } = false;
        public bool IsExistingUser { get; set; } = false;
        public bool IsPasswordInvalid { get; set; } = false;
    }
}
