using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TwitterBackup.Data.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public List<UserTwitterAccount> FavouriteUsers { get; set; }
    }
}
