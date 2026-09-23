using System.ComponentModel.DataAnnotations;
using PatientAliases.Web.DTOs;

namespace PatientAliases.Web.Validation;

/// <summary>
/// Runs DataAnnotation validation across an alias collection and returns aggregated results.
/// </summary>
public static class AliasValidator
{
    public static IReadOnlyList<ValidationResult> Validate(IEnumerable<AliasDto> aliases)
    {
        var results = new List<ValidationResult>();
        var index = 0;

        foreach (var alias in aliases)
        {
            var context = new ValidationContext(alias);
            var itemResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(alias, context, itemResults, validateAllProperties: true))
            {
                foreach (var r in itemResults)
                {
                    results.Add(new ValidationResult(
                        $"Row {index + 1}: {r.ErrorMessage}",
                        r.MemberNames));
                }
            }
            index++;
        }
        return results;
    }
}
