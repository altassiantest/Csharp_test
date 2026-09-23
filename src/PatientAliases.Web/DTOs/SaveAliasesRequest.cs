namespace PatientAliases.Web.DTOs;

/// <summary>Payload for persisting the full aliases collection for a patient.</summary>
public sealed class SaveAliasesRequest
{
    public Guid PatientId { get; set; }
    public IReadOnlyList<AliasDto> Aliases { get; set; } = Array.Empty<AliasDto>();
}
