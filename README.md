#  M4Webapp - Plateforme d'Apprentissage

## Description

**M4Webapp** est une application web ASP.NET Core moderne dédiée à l'**apprentissage des technologies** : C#, ASP.NET Core, Azure, SQL Server et bien d'autres.

L'application centralise **29+ ressources d'apprentissage** de Microsoft Learn avec recherche, filtrage et gestion de favoris.

## 🚀 Fonctionnalités Principales

### 📚 Ressources d'Apprentissage
- **29 ressources** provenant de **Microsoft Learn**
- Organisées en **8 catégories** Microsoft
- Filtrage par :
  - Catégorie
  - Niveau (Débutant, Intermédiaire, Avancé)
  - Type (Gratuit/Premium)
  - Tri (Titre, Niveau, Catégorie)
- Recherche en temps réel
- Système de favoris avec localStorage

### 📧 Gestion des Messages
- Formulaire de contact avec validation
- Enregistrement en base de données (SQLite)
- Envoi email (SMTP ou simulation)
- Admin panel pour gérer les messages

### 🎨 Interface Moderne
- **Mode Sombre / Clair** (toggle)
- Design responsive Bootstrap 5
- Gradients modernes
- Animations au survol
- Support mobile complet

## Architecture

### Stack Technique
- **Framework** : ASP.NET Core 9.0 MVC
- **Language** : C# 13
- **Base de Données** : SQLite (`./data/m4webapp.db`)
- **Frontend** : Bootstrap 5, Alpine.js, Chart.js
- **Logging** : Microsoft.Extensions.Logging

### Dossiers Principaux
```
M4Webapp/
... Controllers/          # Logique applicative
... Data/                 # Contexte de base de données et seeding
... Models/               # Modèles métier
... Repositories/        # Accès données (Messages)
... Services/            # Logique métier (Ressources, Recherche)
... Views/               # Templates Razor
... wwwroot/            # Assets (CSS, JS, images)
```

## 🛠️ Démarrage Rapide

### Prérequis
- .NET 9.0 SDK
- Git

### Installation

```bash
# 1. Cloner le repository
git clone https://github.com/votre-compte/M4Webapp.git
cd M4Webapp

# 2. Créer le dossier pour la base de données
mkdir -p data

# 3. Restaurer les packages
dotnet restore M4Webapp.csproj

# 4. Compiler
dotnet build M4Webapp.csproj

# 5. Exécuter
dotnet run --project M4Webapp.csproj
```

L'application démarre par défaut sur : **http://localhost:5000** (ou https://localhost:5001 si configuré)

## 📖 Utilisation

### Pages Principales
- **Accueil** : `/` - Hero section + aperçu
- **Ressources** : `/Home/Resources` - Catalogue complet avec filtres
- **Contact** : `/Home/Contact` - Formulaire de contact

### Fonctionnalités
1. **Rechercher** : Tapez dans la barre de recherche sur la page Ressources.
2. **Filtrer** : Utilisez les filtres (Catégorie, Gratuit) pour affiner les résultats.
3. **Favoriser** : Cliquez sur l'étoile pour ajouter aux favoris (stockage local).
4. **Basculer Thème** : Utilisez le bouton 🌙/☀️ dans la barre de navigation.

## ⚙️ Configuration

### appsettings.json
La base de données est configurée pour utiliser SQLite par défaut :
```json
{
  "ConnectionStrings": {
    "SqliteConnection": "Data Source=./data/m4webapp.db"
  }
}
```

## 🤝 Contribution

Les contributions sont bienvenues !

1. Fork le repository
2. Créez une branche (`git checkout -b feature/AmazingFeature`)
3. Commit vos changes (`git commit -m 'Add AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrez une Pull Request

## License

Ce projet est licencié sous la [MIT License](LICENSE).

---

**Dernière mise à jour** : 2026-04-23
**Version** : 1.0
**Maintenu par** : Équipe M4Webapp
