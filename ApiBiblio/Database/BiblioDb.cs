using ApiBiblio.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBiblio.Database
{
    public class BiblioDb : DbContext
    {
        public BiblioDb(DbContextOptions<BiblioDb> options) : base(options) { }

        public DbSet<Employe> Employes => Set<Employe>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Membre> Membres => Set<Membre>();
        public DbSet<Livre> Livres => Set<Livre>();
        public DbSet<Auteur> Auteurs => Set<Auteur>();
        public DbSet<Categorie> Categories => Set<Categorie>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<Emprunt> Emprunts => Set<Emprunt>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }
    }
}
