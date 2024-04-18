using BlogAPI.Const;
using BlogAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI
{
    public class Context : IdentityDbContext<AppUser>
    {
        //DbSet<AppUser> appUsers {  get; set; }
        public DbSet<Models.Article> Articles { get; set; }
        public DbSet<Models.Comment> Comments { get; set; }
        public DbSet<Models.Theme> Themes { get; set; }
        public DbSet<Models.User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Theme themeInformatique = new Theme {Id=1,  Name = "Informatique" };
            Theme themeActualite = new Theme { Id = 2, Name = "Actualité" };
            Theme themeSport = new Theme { Id = 3, Name = "Sport" };
            Theme themeLoisirs = new Theme { Id = 4, Name = "Loisirs" };
            modelBuilder.Entity<Theme>().HasData(new List<Theme> { themeActualite, themeSport, themeLoisirs, themeInformatique });

            base.OnModelCreating(modelBuilder);


        }

        public Context(DbContextOptions optionsBuilder) : base(optionsBuilder) { }
    }
}
