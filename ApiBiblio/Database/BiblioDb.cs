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
        public DbSet<EmpruntLivre> EmpruntLivres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Permet de créer les tables d'associations
            base.OnModelCreating(modelBuilder);

            // Configuration de la table de liaison EmpruntLivre
            modelBuilder.Entity<EmpruntLivre>()
                .HasKey(el => new { el.IdEmprunt, el.IdLivre });

            modelBuilder.Entity<EmpruntLivre>()
                .HasOne(el => el.Emprunt)
                .WithMany(e => e.EmpruntLivres)
                .HasForeignKey(el => el.IdEmprunt);

            modelBuilder.Entity<EmpruntLivre>()
                .HasOne(el => el.Livre)
                .WithMany(l => l.EmpruntLivres)
                .HasForeignKey(el => el.IdLivre);

            // Index unique pour l'ISBN
            modelBuilder.Entity<Livre>()
                .HasIndex(l => l.ISBN)
                .IsUnique();

            // Index unique pour l'email de l'employé
            modelBuilder.Entity<Employe>()
                .HasIndex(e => e.LoginEmploye)
                .IsUnique();

            // Index unique pour l'email du membre
            modelBuilder.Entity<Membre>()
                .HasIndex(m => m.AdresseMail)
                .IsUnique();

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

            // Seed data pour les rôles
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, NomRole = "Administrateur" },
                new Role { Id = 2, NomRole = "Bibliothécaire" });
        }
        public DbSet<ApiBiblio.DTOs.CategorieDTO> CategorieDTO { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Employe_Emprunt> Employe_Emprunt { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Livre_Auteur> Livre_Auteur { get; set; } = default!;
        public DbSet<ApiBiblio.Models.Livre_Genre> Livre_Genre { get; set; } = default!;
        public DbSet<ApiBiblio.Models.AssignerRole> AssignerRole { get; set; } = default!;
    }
}
