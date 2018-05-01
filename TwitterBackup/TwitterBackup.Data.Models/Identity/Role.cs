using Microsoft.AspNetCore.Identity;

namespace TwitterBackup.Data.Models.Identity
{
    public class Role : IdentityRole<int>
    {
        public Role() { }

        public Role(string roleName) : base(roleName)
        {
        }
    }
}