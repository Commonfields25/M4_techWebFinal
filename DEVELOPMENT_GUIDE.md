# ?? GUIDE DE DÉVELOPPEMENT LOCAL

## ? Prérequis

- **IDE** : Visual Studio 2022 ou VS Code
- **.NET** : SDK 10.0 ou plus
- **Git** : Pour le version control
- **Node.js** (Optionnel) : Pour npm packages

## ?? Setup Initial

### 1. Cloner le Repository
```bash
git clone https://github.com/votre-compte/M4Webapp.git
cd M4Webapp
```

### 2. Restaurer les Packages NuGet
```bash
dotnet restore
```

### 3. Compiler le Projet
```bash
dotnet build
```

### 4. Exécuter l'Application
```bash
dotnet run
```

Application démarre sur : **https://localhost:5001**

---

## ?? Structure des Répertoires

```
M4Webapp/
??? Controllers/                  # Contrôleurs MVC
?   ??? HomeController.cs
?   ??? AdminController.cs
??? Views/                        # Templates Razor
?   ??? Home/
?   ?   ??? Index.cshtml         # Accueil
?   ?   ??? Resources.cshtml     # Ressources
?   ?   ??? Contact.cshtml       # Formulaire
?   ?   ??? Favorites.cshtml     # Favoris
?   ??? Admin/
?   ?   ??? Index.cshtml         # Dashboard
?   ?   ??? Messages.cshtml      # Messages
?   ??? Shared/
?       ??? _Layout.cshtml       # Layout principal
?       ??? _ValidationScriptsPartial.cshtml
??? Models/                       # Modèles métier
?   ??? ContactMessage.cs
?   ??? Resource.cs
?   ??? ErrorViewModel.cs
??? ViewModels/                   # Modèles de vue
?   ??? ContactFormModel.cs
?   ??? ResourceFilterViewModel.cs
?   ??? AdminDashboardViewModel.cs
??? Services/                     # Logique métier
?   ??? ResourceService.cs
?   ??? SearchService.cs
?   ??? ValidationService.cs
?   ??? ThemeService.cs
??? Repositories/                 # Accès données
?   ??? IMessageRepository.cs
?   ??? FileMessageRepository.cs
??? Notifications/                # Email, SMS
?   ??? IEmailSender.cs
?   ??? EmailSender.cs
??? Exceptions/                   # Exceptions personnalisées
?   ??? M4WebappExceptions.cs
??? Utilities/                    # Helpers & Constantes
?   ??? Constants.cs
??? wwwroot/                      # Assets publics
?   ??? css/
?   ??? js/
?   ??? images/
?   ??? lib/
??? Properties/
?   ??? launchSettings.json       # Configuration développement
?   ??? Profiles/
??? Program.cs                    # Configuration de démarrage
??? appsettings.json             # Configuration
??? appsettings.Development.json  # Config développement
??? M4Webapp.csproj              # Fichier projet

```

---

## ?? Configuration Développement

### appsettings.Development.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "M4Webapp": "Debug"
    }
  },
  "Smtp": {
    "Host": "",
    "Port": 25,
    "EnableSsl": false,
    "User": "",
    "Pass": "",
    "From": "dev@m4webapp.local"
  }
}
```

### Variables d'Environnement (.env)
```bash
ASPNETCORE_ENVIRONMENT=Development
ASPNETCORE_URLS=https://localhost:5001;http://localhost:5000
DOTNET_ENVIRONMENT=Development
```

---

## ?? Workflows Courants

### Ajouter une Ressource

**1. Modifiez `Services/ResourceService.cs`**
```csharp
new Resource
{
    Id = 30,
    Title = "Nouvelle Ressource",
    Description = "Description...",
    Category = Constants.Categories.CSharp,
    Url = Constants.MicrosoftLearnUrls.CSharpFundamentals,
    Icon = "??",
    Color = Constants.Colors.Primary,
    Topics = new[] { "Topic1", "Topic2" },
    Difficulty = Constants.Difficulty.Beginner,
    IsFree = true
}
```

**2. Testez localement**
```bash
dotnet run
# Allez sur https://localhost:5001/Home/Resources
```

**3. Commitez et pushez**
```bash
git add Services/ResourceService.cs
git commit -m "feat: Add new Microsoft Learn resource"
git push origin feature/new-resource
```

---

### Ajouter une Fonctionnalité

**1. Créez une branche**
```bash
git checkout -b feature/my-feature
```

**2. Créez l'interface**
```csharp
// Repositories/IMyService.cs
public interface IMyService
{
    Task<List<T>> GetAllAsync();
}
```

**3. Implémentez le service**
```csharp
// Services/MyService.cs
public class MyService : IMyService
{
    private readonly ILogger<MyService> _logger;
    
    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }
    
    public async Task<List<T>> GetAllAsync()
    {
        _logger.LogInformation("Getting all items");
        // Implémentation...
    }
}
```

**4. Enregistrez dans Program.cs**
```csharp
builder.Services.AddScoped<IMyService, MyService>();
```

**5. Testez**
```bash
dotnet run
# Testez la fonctionnalité
```

**6. Commitez**
```bash
git add .
git commit -m "feat: Add MyService functionality"
git push origin feature/my-feature
```

---

### Corriger un Bug

**1. Créez une branche bugfix**
```bash
git checkout -b bugfix/issue-123
```

**2. Reproduisez le bug**
```bash
dotnet run
# Naviguez et reproduisez
```

**3. Corrigez le code**
```csharp
// Changement...
```

**4. Testez la correction**
```bash
dotnet run
# Vérifiez que le bug est corrigé
```

**5. Commitez**
```bash
git add .
git commit -m "fix: Resolve issue with contact form validation #123"
git push origin bugfix/issue-123
```

---

### Refactorisation

**1. Analysez le code**
```bash
# Utilisez Resharper, Code Analysis, etc.
dotnet build /p:EnforceCodeStyleInBuild=true
```

**2. Refactorisez**
```csharp
// Avant
public void ProcessMessage(ContactMessage msg)
{
    // 100 lignes de code
}

// Après
public void ProcessMessage(ContactMessage msg)
{
    ValidateMessage(msg);
    SaveMessage(msg);
    SendConfirmationEmail(msg);
}

private void ValidateMessage(ContactMessage msg) { }
private void SaveMessage(ContactMessage msg) { }
private void SendConfirmationEmail(ContactMessage msg) { }
```

**3. Testez**
```bash
dotnet run
# Vérifiez que le comportement n'a pas changé
```

**4. Commitez**
```bash
git add .
git commit -m "refactor: Extract message processing logic into separate methods"
git push origin refactor/process-message
```

---

## ?? Testing (À Implémenter)

### Structure
```
M4Webapp.Tests/
??? Unit/
?   ??? Services/
?   ?   ??? ValidationServiceTests.cs
?   ??? Repositories/
?       ??? FileMessageRepositoryTests.cs
??? Integration/
    ??? ContactFormTests.cs
```

### Exécuter les tests
```bash
dotnet test
```

---

## ?? Debugging

### Visual Studio
```
1. Mettez un breakpoint (F9)
2. Lancez en debug (F5)
3. Naviguez à la page pour déclencher
4. Inspectez les variables
```

### VS Code
```json
// .vscode/launch.json
{
    "version": "0.2.0",
    "configurations": [
        {
            "name": ".NET Core Launch",
            "type": "coreclr",
            "request": "launch",
            "preLaunchTask": "build",
            "program": "${workspaceFolder}/bin/Debug/net10.0/M4Webapp.dll",
            "args": [],
            "cwd": "${workspaceFolder}",
            "stopAtEntry": false
        }
    ]
}
```

### Logs
```csharp
_logger.LogDebug("Debug message");
_logger.LogInformation("Info message");
_logger.LogWarning("Warning message");
_logger.LogError(exception, "Error message");
```

---

## ?? Monitoring Développement

### Application Insights (Optionnel)
```bash
# Ajouter le package
dotnet add package Microsoft.ApplicationInsights.AspNetCore
```

```csharp
// Program.cs
builder.Services.AddApplicationInsightsTelemetry();
```

---

## ?? CI/CD Local

### GitHub Actions (À Configurer)
```yaml
# .github/workflows/build.yml
name: Build & Test

on: [push, pull_request]

jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - uses: actions/setup-dotnet@v1
        with:
          dotnet-version: '10.0'
      - run: dotnet build
      - run: dotnet test
```

---

## ?? Troubleshooting

### Port 5001 Déjà Utilisé
```bash
# Trouver le process
lsof -i :5001

# Changer le port
dotnet run --urls="https://localhost:5002"
```

### Erreurs NuGet
```bash
# Nettoyer et restaurer
dotnet nuget locals all --clear
dotnet restore
```

### Hot Reload Non Fonctionnel
```bash
# Relancer avec hot reload
dotnet watch run
```

### Base de Données Corrompue
```bash
# Supprimer les fichiers JSON
rm contact_messages.json

# Relancer
dotnet run
```

---

## ?? Documentation Supplémentaire

- Voir [README.md](README.md) pour la vue d'ensemble
- Voir [BEST_PRACTICES.md](BEST_PRACTICES.md) pour les bonnes pratiques
- Voir [MICROSOFT_RESOURCES.md](MICROSOFT_RESOURCES.md) pour les ressources
- Voir [AUDIT_REPORT.md](AUDIT_REPORT.md) pour l'audit

---

**Dernière mise à jour** : 2026-01-01  
**Pour** : M4Webapp v1.0+
