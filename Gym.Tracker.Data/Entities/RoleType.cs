using System;
using System.Collections.Generic;

namespace Gym.Tracker.Data.Entities;

public partial class RoleType
{
    public int Id { get; set; }

    public int RoleCode { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
