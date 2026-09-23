using System.ComponentModel.DataAnnotations;

namespace PatientAliases.Web.DTOs;

/// <summary>Primary patient record referenced by the aliases screen.</summary>
public sealed class PatientDto
{
    public Guid Id { get; set; }

    [Required, StringLength(60)]
    public string LastName { get; set; } = string.Empty;

    [Required, StringLength(60)]
    public string FirstName { get; set; } = string.Empty;

    [StringLength(30)]
    public string? MiddleName { get; set; }

    [StringLength(10)]
    public string? Suffix { get; set; }

    /// <summary>Formatted display name "Last, First".</summary>
    public string DisplayName => string.IsNullOrWhiteSpace(LastName)
        ? FirstName
        : $"{LastName}, {FirstName}".Trim();
}
