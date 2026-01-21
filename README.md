
- Enregistrement en JSON
- Envoi email (SMTP ou simulation)
- Admin panel pour gérer les messages
- Export CSV des messages

### ?? Interface Moderne
- **Mode Sombre / Clair** (toggle ??)
- Design responsive Bootstrap 5
- Gradients modernes
- Animations au survol
- Support mobile complet

### ?? Dashboard Analytics
- **4 métriques** clés en temps réel
- **3 graphiques** Chart.js :
  - Évolution des messages (7j)
  - Distribution ressources par catégorie
  - Ressources par niveau
- Actions rapides

## ??? Architecture

### Stack Technique
- **Framework** : ASP.NET Core 10 MVC/Razor Pages
- **Language** : C# 14
- **Base de Données** : JSON File-based (extensible à SQL)
- **Frontend** : Bootstrap 5, Chart.js
- **Logging** : Microsoft.Extensions.Logging

### Dossiers Principaux
```
M4Webapp/
??? Controllers/          # Logique applicative
??? Views/               # Templates Razor
??? Models/              # Modèles métier
??? ViewModels/          # Modèles de vue
??? Services/            # Logique métier
??? Repositories/        # Accès données
??? Notifications/       # Email, SMS
??? Exceptions/          # Custom exceptions
??? Utilities/           # Constantes, helpers
??? wwwroot/            # Assets (CSS, JS, images)
```

## ?? Démarrage Rapide

### Prérequis
- .NET 10 SDK
- Visual Studio 2022 ou VS Code
- Git

### Installation

```bash
# Cloner le repository
git clone https://github.com/votre-compte/M4Webapp.git
cd M4Webapp

# Restaurer les packages
dotnet restore

# Compiler
dotnet build

# Exécuter
dotnet run
```

L'application démarre sur : **https://localhost:5001**

## ?? Utilisation

### Pages Principales
- **Accueil** : `https://localhost:5001/` - Hero section + aperçu
- **Ressources** : `https://localhost:5001/Home/Resources` - Catalogue complet avec filtres
- **Favoris** : `https://localhost:5001/Home/Favorites` - Ressources sauvegardées
- **Contact** : `https://localhost:5001/Home/Contact` - Formulaire de contact
- **Admin** : `https://localhost:5001/Admin` - Dashboard analytics

### Fonctionnalités
1. **Rechercher** : Tapez dans la barre de recherche
2. **Filtrer** : Utilisez les panneaux latéraux
3. **Favoriser** : Cliquez sur ?
4. **Basculer Thème** : Cliquez ??/??
5. **Consulter Analytics** : Allez sur `/Admin`

## ?? Sécurité

### Implémentée
- ? CSRF tokens sur formulaires
- ? Validation côté serveur
- ? Logging de toutes les opérations
- ? Gestion des exceptions personnalisées

### À Ajouter
- [ ] Authentification Admin (Identity)
- [ ] Rate Limiting
- [ ] HTTPS obligatoire en production
- [ ] Content Security Policy (CSP)

## ?? Configuration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=contacts.db"
  },
  "Smtp": {
    "Host": "",           // Laisser vide pour simulation
    "Port": 25,
    "EnableSsl": false,
    "User": "",
    "Pass": "",
    "From": ""
  }
}
```

### Variables d'Environnement
```bash
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=https://localhost:5001
```

## ??? Développement

### Ajouter une Ressource
1. Modifiez `Services/ResourceService.cs`
2. Ajoutez une nouvelle entrée dans `GetAllResources()`
3. Compilez et testez

### Personnaliser les Couleurs
- Modifiez `Utilities/Constants.cs` (Colors)
- Mettez à jour les ressources correspondantes

### Ajouter un Filtre
1. Modifiez `ViewModels/ResourceFilterViewModel.cs`
2. Mettez à jour `Services/SearchService.cs`
3. Ajustez la vue `Views/Home/Resources.cshtml`

## ?? Ressources Microsoft

### Apprentissage
- [Microsoft Learn](https://learn.microsoft.com)
- [C# Guide](https://learn.microsoft.com/dotnet/csharp)
- [ASP.NET Core](https://learn.microsoft.com/aspnet/core)
- [Entity Framework Core](https://learn.microsoft.com/ef/core)
- [Azure](https://learn.microsoft.com/azure)

### Documentation
- [Microsoft Docs](https://docs.microsoft.com)
- [GitHub Microsoft](https://github.com/microsoft)

## ?? Contribution

Les contributions sont bienvenues ! Pour contribuer :

1. Fork le repository
2. Créez une branche (`git checkout -b feature/AmazingFeature`)
3. Commit vos changes (`git commit -m 'Add AmazingFeature'`)
4. Push vers la branche (`git push origin feature/AmazingFeature`)
5. Ouvrez une Pull Request

## ?? License

Ce projet est licencié sous la [MIT License](LICENSE).

## ?? Support

Pour toute question ou problème :
- Ouvrez une [Issue](https://github.com/votre-compte/M4Webapp/issues)
- Consultez la [Documentation](AUDIT_REPORT.md)
- Envoyez un message via le formulaire de contact

## ?? Remerciements

- **Microsoft** pour les ressources Microsoft Learn
- **Bootstrap** pour le framework CSS
- **Chart.js** pour les graphiques
- Communauté .NET

## ?? Roadmap

### v1.1 (Prochainement)
- [ ] Authentification Admin avec Identity
- [ ] Système de notation des ressources
- [ ] Notifications par email
- [ ] Export PDF des analytics

### v1.2
- [ ] API REST complète
- [ ] Tests unitaires
- [ ] Internationalisation (i18n)
- [ ] Mobile App

### v2.0
- [ ] Base de données SQL complète
- [ ] Système de certification
- [ ] Microservices
- [ ] Container Docker

---

**Dernière mise à jour** : 2026-01-01  
**Version** : 1.0.0  
**Maintenu par** : Équipe M4Webapp


