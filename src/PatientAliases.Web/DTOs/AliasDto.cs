using System.ComponentModel.DataAnnotations;

namespace PatientAliases.Web.DTOs;

/// <summary>
/// Represents a single patient alias row (Last, First, Middle, Suffix).
/// </summary>
public sealed class AliasDto
{
    /// <summary>Unique identifier for the alias (server-assigned).</summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(60, ErrorMessage = "Last name cannot exceed 60 characters.")]
    [Display(Name = "Last")]
    public string Last { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(60, ErrorMessage = "First name cannot exceed 60 characters.")]
    [Display(Name = "First")]
    public string First { get; set; } = string.Empty;

    [StringLength(30, ErrorMessage = "Middle name cannot exceed 30 characters.")]
    [Display(Name = "Middle")]
    public string? Middle { get; set; }

    [StringLength(10, ErrorMessage = "Suffix cannot exceed 10 characters.")]
    [RegularExpression(@"^[A-Za-z\.]*$", ErrorMessage = "Suffix contains invalid characters.")]
    [Display(Name = "Suffix")]
    public string? Suffix { get; set; }

    /// <summary>Client-side flag indicating the row is in inline edit mode.</summary>
    public bool IsEditing { get; set; }

    /// <summary>Client-side flag indicating this alias row is newly created and unsaved.</summary>
    public bool IsNew { get; set; }
}
