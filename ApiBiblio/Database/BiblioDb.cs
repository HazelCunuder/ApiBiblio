using ApiBiblio.Models;
using Microsoft.EntityFrameworkCore;
using ApiBiblio.DTOs;

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

            // AssignerRole composite key
            modelBuilder.Entity<AssignerRole>()
                .HasKey(ca => new { ca.RoleId, ca.EmployeId });

            // Employe_Emprunt composite key
            modelBuilder.Entity<Employe_Emprunt>()
                .HasKey(ca => new { ca.EmpruntId, ca.EmployeId });

            // Livre_Auteur composite key and relationships
            modelBuilder.Entity<Livre_Auteur>()
                .HasKey(ca => new { ca.IdLivre, ca.IdAuteur });

            modelBuilder.Entity<Livre_Auteur>()
                .HasOne(ca => ca.Livre)
                .WithMany(l => l.Livre_Auteurs)
                .HasForeignKey(ca => ca.IdLivre);

            modelBuilder.Entity<Livre_Auteur>()
                .HasOne(ca => ca.Auteur)
                .WithMany(a => a.Livre_Auteurs)
                .HasForeignKey(ca => ca.IdAuteur);

            // Livre_Genre composite key and relationships
            modelBuilder.Entity<Livre_Genre>()
                .HasKey(ca => new { ca.IdLivre, ca.IdGenre });

            modelBuilder.Entity<Livre_Genre>()
                .HasOne(ca => ca.Livre)
                .WithMany(l => l.Livre_Genres)
                .HasForeignKey(ca => ca.IdLivre);

            modelBuilder.Entity<Livre_Genre>()
                .HasOne(ca => ca.Genre)
                .WithMany(g => g.Livre_Genres)
                .HasForeignKey(ca => ca.IdGenre);
        }

        public DbSet<ApiBiblio.DTOs.CategorieDTO> CategorieDTO { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Employe_Emprunt> Employe_Emprunt { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Livre_Auteur> Livre_Auteur { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Livre_Genre> Livre_Genre { get; set; } = default!;
        public DbSet<ApiBiblio.Models.AssignerRole> AssignerRole { get; set; } = default!;
    }
}
