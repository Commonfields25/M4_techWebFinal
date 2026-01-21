using System.ComponentModel.DataAnnotations;

namespace M4Webapp.Services;

public interface IValidationService
{
    bool TryValidate(object model, out List<ValidationResult> results);
}

public class ValidationService : IValidationService
{
    public bool TryValidate(object model, out List<ValidationResult> results)
    {
        var context = new ValidationContext(model);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(model, context, results, true);
    }
}
