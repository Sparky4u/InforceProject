using InforceProject.Server.Models.AboutInfoModel;
using InforceProject.Server.Models.UrlModel;
using InforceProject.Server.Models.UserModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InforceProject.Server.Data
{
    public class UrlContext : IdentityDbContext<User>
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<AboutInfo> AboutInfos { get; set; }
        public UrlContext(DbContextOptions<UrlContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=UrlShortenerDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Url>()
                .HasIndex(u => u.ShortUrl)
                .IsUnique(); 
        }
    }
}
