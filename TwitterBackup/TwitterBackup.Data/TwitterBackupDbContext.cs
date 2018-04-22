using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Models.Identity;

namespace TwitterBackup.Data
{
    public class TwitterBackupDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public TwitterBackupDbContext(DbContextOptions<TwitterBackupDbContext> options)
            : base(options)
        {
        }

        public DbSet<TwAccount> TwitterAccounts { get; set; }
        public DbSet<TwAccountImage> TwitterAccountImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTwAccount>()
                .HasKey(k => new { k.UserId, k.TwAccountId });

            builder.Entity<UserTwAccount>()
                .HasOne(u => u.User)
                .WithMany(twu => twu.FavouriteUsers)
                .HasForeignKey(fk => fk.UserId);

            builder.Entity<UserTwAccount>()
                .HasOne(twu => twu.TwAccount)
                .WithMany(twu => twu.Users)
                .HasForeignKey(fk => fk.TwAccountId);

            builder.Entity<TwAccount>()
                .HasOne(twui => twui.TwAccountImage)
                .WithOne(twu => twu.TwAccount);

            builder.Entity<User>(e => e.ToTable("Users"));
            builder.Entity<Role>(e => e.ToTable("Roles"));
            builder.Entity<UserRole>(e => e.ToTable("UserRoles"));
            builder.Entity<UserClaim>(e => e.ToTable("UserClaims"));
            builder.Entity<UserLogin>(e => e.ToTable("UserLogins"));
            builder.Entity<UserToken>(e => e.ToTable("UserTokens"));
            builder.Entity<RoleClaim>(e => e.ToTable("RoleClaims"));
        }
    }
}