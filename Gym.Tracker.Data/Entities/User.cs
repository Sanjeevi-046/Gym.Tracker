using System;
using System.Collections.Generic;

namespace Gym.Tracker.Data.Entities;

public partial class User
{
    public long Id { get; set; }

    public string? FullName { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Gender { get; set; }

    public long? CountryId { get; set; }

    public long? UserTypeId { get; set; }

    public long? BillingTermsId { get; set; }

    public long? PaymentMethodId { get; set; }

    public long? Nationality { get; set; }

    public byte[] Password { get; set; } = null!;

    public bool? IsTerminated { get; set; }

    public bool? IsUserLocked { get; set; }

    public bool? IsForcePwdChange { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public string? ContactInformation { get; set; }

    public string? BillingAddress { get; set; }

    public string? MailingAddress { get; set; }

    public long? CreatedBy { get; set; }

    public DateTime? CreatedAt { get; set; }

    public long? UpdatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool IsActive { get; set; }

    public long? DeletedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public long? RestoredBy { get; set; }

    public DateTime? RestoredAt { get; set; }

    public bool? IsMailNotificationAllowed { get; set; }

    public long RoleId { get; set; }
}
