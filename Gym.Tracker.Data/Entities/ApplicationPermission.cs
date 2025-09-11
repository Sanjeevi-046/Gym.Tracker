using System;
using System.Collections.Generic;

namespace Gym.Tracker.Data.Entities;

public partial class ApplicationPermission
{
    public int Id { get; set; }

    public int PermissionCode { get; set; }

    public string PermissionName { get; set; } = null!;

    public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}
