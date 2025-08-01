# 📚 ApiBiblio

ApiBiblio est une application web ASP.NET Core MVC pour la gestion d'une bibliothèque, permettant la création, la consultation, la modification et la suppression de données telles que les livres, auteurs, genres, catégories et employés.

## 🛠 Fonctionnalités principales

- Gestion des livres, auteurs, catégories, genres et employés
- Interface CRUD via Razor Pages
- Architecture MVC avec séparation claire des responsabilités
- Utilisation de DTOs pour le transfert de données
- Documentation Swagger intégrée
- Sécurité de base avec rôles pour les employés
- Connexion à une base de données configurée dans `appsettings.json`

## 📁 Structure du projet

```bsh
ApiBiblio/
│
├── Controllers/ # Contrôleurs pour les routes API
├── DTOs/ # Objets de transfert de données
├── Database/ # Contexte EF Core
├── Models/ # Modèles métiers
├── Views/ # Fichiers Razor (UI)
├── Swagger/ # Configuration Swagger
├── wwwroot/ # Contenu statique
├── appsettings.json # Configuration de l'application
├── Program.cs # Point d'entrée de l'application
└── ApiBiblio.csproj # Fichier de projet
```

## ▶️ Lancer le projet

1. Cloner le dépôt :

```bsh
   git clone https://github.com/abdellah59/ApiBiblio.git
   cd ApiBiblio/ApiBiblio
```

2. Restaurer les dépendances :
       dotnet restore

3. Appliquer les migrations (si nécessaire) :
       dotnet ef database update

4. Lancer le serveur :
       dotnet run

5. Accéder à l’application :
       Ouvrez http://localhost:5000 dans votre navigateur.

## 🔒 RGPD & Sécurité

- L'application respecte les principes du RGPD, notamment en matière de gestion des comptes utilisateurs.
- Les mots de passe ne sont jamais affichés en clair dans l'interface.
- Les rôles des employés sont utilisés pour restreindre l'accès à certaines fonctionnalités.

## 📄 Documentation API

Une interface Swagger est disponible à l'adresse `/swagger` pour tester les points d'entrée API et consulter la documentation technique.

## 🧑‍💻 Crédits

- Développé par Abdellah, Nicolas et Hazel
- Sous la supervision d'Alexandre Liné et Benjamin Quinet

## 📜 Licence

Ce projet est open source — vous pouvez le modifier ou l’utiliser librement selon les termes de la licence incluse.