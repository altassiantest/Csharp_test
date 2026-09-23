using PatientAliases.Web.DTOs;

namespace PatientAliases.Web.Services;

/// <summary>Contract for retrieving and persisting patient alias data.</summary>
public interface IPatientAliasesService
{
    Task<PatientDto?> GetPatientAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AliasDto>> GetAliasesAsync(Guid patientId, CancellationToken cancellationToken = default);
    Task SaveAliasesAsync(SaveAliasesRequest request, CancellationToken cancellationToken = default);
    Task<PatientDto> SwapAliasWithPrimaryAsync(Guid patientId, Guid aliasId, CancellationToken cancellationToken = default);
}
