using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

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