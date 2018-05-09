using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TwitterBackup.Data.Models.Identity
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            this.FavouriteUsers = new HashSet<UserTwitterAccount>();
            this.TwitterStatuses = new HashSet<UserTwitterStatus>();
        }

        public ICollection<UserTwitterAccount> FavouriteUsers { get; set; }

        public ICollection<UserTwitterStatus> TwitterStatuses { get; set; }
    }
}