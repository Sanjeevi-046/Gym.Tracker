using System;
using System.Collections.Generic;

namespace Gym.Tracker.Data.Entities;

public partial class RolePermission
{
    public int Id { get; set; }

    public int RoleTypeId { get; set; }

    public int ApplicationPermissionId { get; set; }

    public virtual ApplicationPermission ApplicationPermission { get; set; } = null!;

    public virtual RoleType RoleType { get; set; } = null!;
}
