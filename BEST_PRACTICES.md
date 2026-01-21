# ?? GUIDE DES BONNES PRATIQUES - M4Webapp

## 1?? **ARCHITECTURE & DESIGN PATTERNS**

### ? Patterns Implémentés

#### Repository Pattern
```csharp
// ? BON
public interface IMessageRepository
{
    Task SaveAsync(ContactMessage message);
    Task DeleteAsync(int id);
}

// ? MAUVAIS
public class MessageRepository
{
    // Pas d'interface, couplage fort
}
```

#### Dependency Injection
```csharp
// ? BON
public FileMessageRepository(ILogger<FileMessageRepository> logger)
{
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
}

// ? MAUVAIS
public FileMessageRepository()
{
    _logger = LoggerFactory.Create(b => b.AddConsole()).CreateLogger<FileMessageRepository>();
}
```

#### Service Layer
```csharp
// ? BON
public class ContactService
{
    private readonly IMessageRepository _repo;
    private readonly IEmailSender _emailSender;
    
    public async Task ProcessContactAsync(ContactMessage message)
    {
        await _repo.SaveAsync(message);
        await _emailSender.SendEmailAsync(message.Email, "Merci", "...");
    }
}
```

---

## 2?? **CODE QUALITY**

### Naming Conventions
```csharp
// ? Classes, Methods, Properties
public class ContactMessage { }
public void ProcessContact() { }
public string DisplayName { get; set; }

// ? Private fields, local variables, parameters
private string _storagePath;
var maxRetries = 3;
void ProcessContact(string contactName) { }

// ? Constants
public const string DefaultTheme = "light";
```

### Comments & Documentation
```csharp
// ? BON - XML Comments
/// <summary>
/// Valide un message de contact.
/// </summary>
/// <param name="model">Le modèle de contact à valider</param>
/// <returns>Tuple (validité, liste d'erreurs)</returns>
public (bool isValid, List<string> errors) Validate(ContactFormModel model)
{
}

// ? MAUVAIS - Commentaires inutiles
// Vérifie si le message est valide
public bool IsValid(ContactFormModel model)
{
}
```

### Logging
```csharp
// ? BON - Structured Logging
_logger.LogInformation("Message {Id} saved successfully", message.Id);
_logger.LogWarning("Message {Id} not found for deletion", id);
_logger.LogError(ex, "Error saving message");

// ? MAUVAIS
Console.WriteLine($"Message saved: {message.Id}");
System.Console.WriteLine("Error: " + exception.Message);
```

---

## 3?? **ERROR HANDLING**

### Custom Exceptions
```csharp
// ? BON
public class StorageException : M4WebappException
{
    public StorageException(string operation, Exception inner) 
        : base($"L'opération '{operation}' a échoué.", inner) { }
}

try
{
    // Opération...
}
catch (Exception ex)
{
    _logger.LogError(ex, "Error during storage operation");
    throw new StorageException("save", ex);
}

// ? MAUVAIS
catch (Exception ex)
{
    Console.WriteLine("Error occurred");
    throw;  // Re-throw sans contexte
}
```

### Null Safety
```csharp
// ? BON
public FileMessageRepository(ILogger<FileMessageRepository> logger)
{
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
}

// ? MAUVAIS
public FileMessageRepository(ILogger logger)
{
    _logger = logger;  // Pas de vérification
}
```

---

## 4?? **ASYNC/AWAIT**

### Best Practices
```csharp
// ? BON - Async all the way
public async Task<IEnumerable<ContactMessage>> GetAllAsync()
{
    var messages = await _repo.GetAllAsync();
    return messages;
}

// ? BON - ConfigureAwait pour les libraries
public async Task SaveAsync(ContactMessage message)
{
    await _file.WriteAllTextAsync(_path, json)
        .ConfigureAwait(false);
}

// ? MAUVAIS - Sync over async
public void GetAll()
{
    var messages = _repo.GetAllAsync().Result;  // Deadlock risk!
}

// ? MAUVAIS - Oublier ConfigureAwait
public async Task SaveAsync(ContactMessage message)
{
    await _file.WriteAllTextAsync(_path, json);  // Peut causer deadlock
}
```

---

## 5?? **VALIDATION**

### Input Validation
```csharp
// ? BON
public async Task SaveAsync(ContactMessage message)
{
    if (message == null)
        throw new ArgumentNullException(nameof(message));
    
    var (isValid, errors) = _validationService.Validate(message);
    if (!isValid)
        throw new ValidationException("Invalid contact message", errors);
    
    // Opération...
}

// ? MAUVAIS
public async Task SaveAsync(ContactMessage message)
{
    // Pas de validation
    _repo.Save(message);
}
```

---

## 6?? **CONFIGURATION**

### Constants Centralisées
```csharp
// ? BON - Utiliser Constants
public class ValidationService
{
    public void Validate(ContactFormModel model)
    {
        if (model.Name.Length > Constants.Validation.MaxNameLength)
        {
            errors.Add("Trop long");
        }
    }
}

// ? MAUVAIS - Magic strings
public void Validate(ContactFormModel model)
{
    if (model.Name.Length > 100)  // Magic number!
    {
        errors.Add("Trop long");
    }
}
```

---

## 7?? **LINQ**

### Query Best Practices
```csharp
// ? BON - Fluent API
var resources = _resourceService.GetAll()
    .Where(r => r.IsFree)
    .OrderBy(r => r.Title)
    .Take(10)
    .ToList();

// ? BON - Query syntax quand complexe
var result = (from r in resources
              where r.Category == category
              orderby r.Title
              select r).ToList();

// ? MAUVAIS - Multiple énumérations
var list = resources.Where(r => r.IsFree);
var count = list.Count();     // Énumération #1
var items = list.ToList();    // Énumération #2
```

---

## 8?? **UNIT TESTING (À Implémenter)**

### Pattern AAA
```csharp
[TestClass]
public class ValidationServiceTests
{
    [TestMethod]
    public void ValidateContact_WithValidData_ReturnTrue()
    {
        // Arrange
        var service = new ValidationService();
        var model = new ContactFormModel 
        { 
            Name = "John",
            Email = "john@example.com",
            Message = "Valid message here"
        };
        
        // Act
        var (isValid, errors) = service.ValidateContactMessage(model);
        
        // Assert
        Assert.IsTrue(isValid);
        Assert.AreEqual(0, errors.Count);
    }
}
```

---

## 9?? **PERFORMANCE**

### Caching
```csharp
// ? BON
public class ResourceService
{
    private readonly IMemoryCache _cache;
    
    public List<Resource> GetAll()
    {
        if (_cache.TryGetValue("all_resources", out List<Resource> resources))
            return resources;
        
        resources = _loadFromSource();
        _cache.Set("all_resources", resources, TimeSpan.FromHours(1));
        return resources;
    }
}

// ? MAUVAIS - Pas de cache
public List<Resource> GetAll()
{
    return _loadFromSource();  // À chaque appel!
}
```

### Lazy Loading
```csharp
// ? BON
public List<Resource> GetResourcesByCategory(string category)
{
    return _resources
        .AsEnumerable()  // Début d'énumération
        .Where(r => r.Category == category)  // Filtrage côté client
        .ToList();  // Énumération finale
}
```

---

## ?? **SECURITY**

### CSRF Protection
```csharp
// ? BON - Razor Pages
@Html.AntiForgeryToken()

// ? BON - Validation côté serveur
[ValidateAntiForgeryToken]
[HttpPost]
public async Task<IActionResult> Contact(ContactFormModel model)
{
    // ...
}
```

### SQL Injection Prevention
```csharp
// ? BON - Paramètres
SELECT * FROM Messages WHERE Email = @email

// ? MAUVAIS - Concatenation
var query = $"SELECT * FROM Messages WHERE Email = '{email}'";
```

---

## 1??1?? **RESSOURCES MICROSOFT RECOMMANDÉES**

### Apprentissage Obligatoire
1. [C# Documentation](https://learn.microsoft.com/dotnet/csharp)
2. [ASP.NET Core Best Practices](https://learn.microsoft.com/aspnet/core/fundamentals)
3. [Dependency Injection](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection)
4. [Logging](https://learn.microsoft.com/dotnet/core/extensions/logging)
5. [Configuration](https://learn.microsoft.com/dotnet/core/extensions/configuration)

### Certifications
- [AZ-900: Azure Fundamentals](https://learn.microsoft.com/certifications/azure-fundamentals)
- [AZ-204: Developing Solutions for Azure](https://learn.microsoft.com/certifications/azure-developer)

---

## ? CHECKLIST DE CODE REVIEW

- [ ] Les interfaces sont utilisées pour l'abstraction
- [ ] Dependency Injection est configurée
- [ ] Logging est présent (pas de Console.WriteLine)
- [ ] Custom exceptions sont utilisées
- [ ] Null checks et validation sont présents
- [ ] Async/await est utilisé correctement
- [ ] XML comments sur les méthodes publiques
- [ ] Constants centralisées (pas de magic strings)
- [ ] Naming conventions respectées
- [ ] Tests unitaires présents

---

**Dernière mise à jour** : 2026-01-01  
**Applicable à** : M4Webapp v1.0+
