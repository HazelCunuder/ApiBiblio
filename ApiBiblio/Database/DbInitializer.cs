using ApiBiblio.Database;
using ApiBiblio.Models;

namespace BibliothequeApp.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(BiblioDb context)
        {
            // Vérifier si la DB contient déjà des données
            if (context.Roles.Any())
                return;

            // Ajouter les rôles
            var roles = new Role[]
            {
                new Role { NomRole = "Administrateur" },
                new Role { NomRole = "Bibliothécaire" }
            };

            context.Roles.AddRange(roles);
            await context.SaveChangesAsync();

            // Ajouter des catégories de base
            var categories = new Categorie[]
            {
                new Categorie { NomCategorie = "Roman" },
                new Categorie { NomCategorie = "BD" },
                new Categorie { NomCategorie = "Manga" },
                new Categorie { NomCategorie = "Article" }
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            // Ajouter des genres de base
            var genres = new Genre[]
            {
                new Genre { NomGenre = "Sciences" },
                new Genre { NomGenre = "Fiction" },
                new Genre { NomGenre = "Biographie" },
                new Genre { NomGenre = "Théâtre" }
            };

            context.Genres.AddRange(genres);
            await context.SaveChangesAsync();

            // Ajouter un employé administrateur par défaut
            var adminEmploye = new Employe
            {
                NomEmploye = "Admin",
                PrenomEmploye = "Super",
                LoginEmploye = "admin@bibliotheque.com",
                MdpEmploye = BCrypt.Net.BCrypt.HashPassword("admin123"),
                IdRole = 1 // Administrateur
            };

            context.Employes.Add(adminEmploye);
            await context.SaveChangesAsync();
        }
    }
}