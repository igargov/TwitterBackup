using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Models.Identity;

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

            builder.Entity<User>(e => e.ToTable("Users"));
            builder.Entity<IdentityRole>(e => e.ToTable("Roles"));
            builder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
            builder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
            builder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
            builder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
            builder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"));
        }
    }
}