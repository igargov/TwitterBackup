using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data
{
    public class TwitterBackupDbContext : IdentityDbContext<User>
    {
        public TwitterBackupDbContext(DbContextOptions<TwitterBackupDbContext> options)
            : base(options)
        {
        }

        //DBSets must go here !!!

        //SaveChanges must go here !!!

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}