using PatientAliases.Web.DTOs;
using Microsoft.Extensions.Logging;

namespace PatientAliases.Web.Services;

/// <summary>
/// In-memory demo implementation seeded with sample data from the UI spec
/// (Patient: Steinberg, Fred — Alias: Steinberg / Freddy / J / JR).
/// Replace with a real repository (EF Core, HTTP, etc.) in production.
/// </summary>
public sealed class InMemoryPatientAliasesService : IPatientAliasesService
{
    private readonly ILogger<InMemoryPatientAliasesService> _logger;

    // Deterministic demo patient id so the sample page can bind consistently.
    public static readonly Guid DemoPatientId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    private static PatientDto _patient = new()
    {
        Id = DemoPatientId,
        LastName = "Steinberg",
        FirstName = "Fred"
    };

    private static readonly List<AliasDto> _aliases = new()
    {
        new AliasDto { Last = "Steinberg", First = "Freddy", Middle = "J", Suffix = "JR" }
    };

    public InMemoryPatientAliasesService(ILogger<InMemoryPatientAliasesService> logger)
        => _logger = logger;

    public Task<PatientDto?> GetPatientAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching patient {PatientId}", patientId);
        return Task.FromResult<PatientDto?>(patientId == _patient.Id ? _patient : null);
    }

    public Task<IReadOnlyList<AliasDto>> GetAliasesAsync(Guid patientId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Fetching aliases for {PatientId}", patientId);
        IReadOnlyList<AliasDto> snapshot = _aliases
            .Select(a => new AliasDto
            {
                Id = a.Id,
                Last = a.Last,
                First = a.First,
                Middle = a.Middle,
                Suffix = a.Suffix
            }).ToList();
        return Task.FromResult(snapshot);
    }

    public Task SaveAliasesAsync(SaveAliasesRequest request, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);
        _logger.LogInformation("Saving {Count} aliases for {PatientId}",
            request.Aliases.Count, request.PatientId);

        _aliases.Clear();
        _aliases.AddRange(request.Aliases.Select(a => new AliasDto
        {
            Id = a.Id == Guid.Empty ? Guid.NewGuid() : a.Id,
            Last = a.Last.Trim(),
            First = a.First.Trim(),
            Middle = a.Middle?.Trim(),
            Suffix = a.Suffix?.Trim()
        }));
        return Task.CompletedTask;
    }

    public Task<PatientDto> SwapAliasWithPrimaryAsync(Guid patientId, Guid aliasId, CancellationToken cancellationToken = default)
    {
        var alias = _aliases.FirstOrDefault(a => a.Id == aliasId)
                    ?? throw new InvalidOperationException("Alias not found.");

        _logger.LogInformation("Swapping alias {AliasId} with primary name for {PatientId}", aliasId, patientId);

        // Swap primary <-> alias values
        (alias.Last, _patient.LastName) = (_patient.LastName, alias.Last);
        (alias.First, _patient.FirstName) = (_patient.FirstName, alias.First);
        (alias.Middle, _patient.MiddleName) = (_patient.MiddleName, alias.Middle);
        (alias.Suffix, _patient.Suffix) = (_patient.Suffix, alias.Suffix);

        return Task.FromResult(_patient);
    }
}
