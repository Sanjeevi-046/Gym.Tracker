using System.ComponentModel;

namespace Gym.Tracker.Common.Enum
{
    public enum UserType
    {
        Admin = 1,
        Trainer = 2,
        Staff = 3,
        Member = 4,
        Guest = 5
    }
    public enum RoleType
    {
        Admin = 1,
        Trainer = 2,
        Staff = 3,
        Member = 4,
        Guest = 5
    }
    public enum ApplicationPermission
    {
        allowallaction =1,
        createadmin = 2,
        createstaff = 3,
        createmember = 4,
        createguest = 5,
        createtrainer = 6,
        viewdashboard = 7,
        viewstaffuser = 8,
        viewmemberuser = 9,
        viewguest = 10,
        viewmemberslot = 11,
        viewattendance = 12,
        editattendance = 13,
        editslot = 14
    }
    public enum Gender
    {
        Male = 1,
        Female = 2,
        [Description("Not Selected")]
        NotSelected = 3
    }
    public enum FileExportType
    {
        Excel = 1,
        PDF = 2
    }
    
}
