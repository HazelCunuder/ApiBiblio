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
            // Permet de créer les tables d'associations
            base.OnModelCreating(modelBuilder);

            // Définir une clé primaire composite sur Assigner Role
            modelBuilder.Entity<AssignerRole>()
                .HasKey(ca => new { ca.RoleId, ca.EmployeId });

            // Définir une clé primaire composite sur Employe_Emprunt
            modelBuilder.Entity<Employe_Emprunt>()
                .HasKey(ca => new { ca.EmpruntId, ca.EmployeId });

            // Définir une clé primaire composite sur Livre - Auteur
            modelBuilder.Entity<Livre_Auteur>()
                .HasKey(ca => new { ca.IdLivre, ca.IdAuteur });

            // Définir une clé primaire composite sur Livre - Genre
            modelBuilder.Entity<Livre_Genre>()
                .HasKey(ca => new { ca.IdLivre, ca.IdGenre });

        }
        public DbSet<ApiBiblio.DTOs.CategorieDTO> CategorieDTO { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Employe_Emprunt> Employe_Emprunt { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Livre_Auteur> Livre_Auteur { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Livre_Genre> Livre_Genre { get; set; } = default!;
        public DbSet<ApiBiblio.Models.AssignerRole> AssignerRole { get; set; } = default!;
    }
}
