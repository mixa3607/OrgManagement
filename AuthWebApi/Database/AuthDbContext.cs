using Microsoft.EntityFrameworkCore;
using Npgsql;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Database
{
    public sealed class AuthDbContext : DbContext
    {
        public DbSet<DbUser> Users { get; set; }
        public DbSet<DbRefreshToken> RefreshTokens { get; set; }

        public AuthDbContext(DbContextOptions<AuthDbContext> builder) : base(builder)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var mapper = NpgsqlConnection.GlobalTypeMapper;
            mapper.UseJsonNet();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var users = modelBuilder.Entity<DbUser>();
            var refreshTokens = modelBuilder.Entity<DbRefreshToken>();

            users.HasKey(u => u.Id);
            users.HasIndex(u => u.UserName).IsUnique();
            users.Property(u => u.Id).ValueGeneratedOnAdd().IsRequired();

            refreshTokens.HasKey(rt => rt.Token);
            refreshTokens.Property(rt => rt.Token).IsRequired();
            refreshTokens.HasOne(rt => rt.NavUser)
                .WithMany(u => u.NavRefreshTokens)
                .HasForeignKey(rt => rt.UserId);
        }
    }
}