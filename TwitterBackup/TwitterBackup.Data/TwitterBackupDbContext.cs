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

        public DbSet<TwitterAccount> TwitterAccounts { get; set; }
        public DbSet<TwitterAccountImage> TwitterAccountImages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTwitterAccount>()
                .HasKey(k => new { k.UserId, k.TwitterAccountId });

            builder.Entity<UserTwitterAccount>()
                .HasOne(u => u.User)
                .WithMany(twu => twu.FavouriteUsers)
                .HasForeignKey(fk => fk.UserId);

            builder.Entity<UserTwitterAccount>()
                .HasOne(twu => twu.TwitterAccount)
                .WithMany(twu => twu.Users)
                .HasForeignKey(fk => fk.TwitterAccountId);

            builder.Entity<TwitterAccount>()
                .HasOne(twui => twui.TwitterAccountImage)
                .WithOne(twu => twu.TwitterAccount)
                .HasForeignKey<TwitterAccountImage>();

            builder.Entity<User>(e => e.ToTable("Users"));
            builder.Entity<Role>(e => e.ToTable("Roles"));
            builder.Entity<UserRole>(e => e.ToTable("UserRoles"));
            builder.Entity<UserClaim>(e => e.ToTable("UserClaims"));
            builder.Entity<UserLogin>(e => e.ToTable("UserLogins"));
            builder.Entity<UserToken>(e => e.ToTable("UserTokens"));
            builder.Entity<RoleClaim>(e => e.ToTable("RoleClaims"));
        }

        public DbSet<T> DbSet<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}