using Microsoft.Extensions.Logging.Abstractions;
using PatientAliases.Web.DTOs;
using PatientAliases.Web.Services;
using Xunit;

namespace PatientAliases.Tests;

public class InMemoryPatientAliasesServiceTests
{
    private static InMemoryPatientAliasesService CreateSut()
        => new(NullLogger<InMemoryPatientAliasesService>.Instance);

    [Fact]
    public async Task GetPatient_ReturnsSeededPatient()
    {
        var sut = CreateSut();
        var patient = await sut.GetPatientAsync(InMemoryPatientAliasesService.DemoPatientId);
        Assert.NotNull(patient);
        Assert.Equal("Steinberg, Fred", patient!.DisplayName);
    }

    [Fact]
    public async Task GetAliases_ReturnsSeededRow()
    {
        var sut = CreateSut();
        var aliases = await sut.GetAliasesAsync(InMemoryPatientAliasesService.DemoPatientId);
        Assert.Single(aliases);
        Assert.Equal("Freddy", aliases[0].First);
    }

    [Fact]
    public async Task SaveAliases_PersistsCollection()
    {
        var sut = CreateSut();
        await sut.SaveAliasesAsync(new SaveAliasesRequest
        {
            PatientId = InMemoryPatientAliasesService.DemoPatientId,
            Aliases = new[]
            {
                new AliasDto { Last = "Doe", First = "John" },
                new AliasDto { Last = "Doe", First = "Johnny" }
            }
        });

        var saved = await sut.GetAliasesAsync(InMemoryPatientAliasesService.DemoPatientId);
        Assert.Equal(2, saved.Count);
    }

    [Fact]
    public async Task SwapAlias_ExchangesValuesWithPrimary()
    {
        var sut = CreateSut();
        var aliases = await sut.GetAliasesAsync(InMemoryPatientAliasesService.DemoPatientId);
        var target = aliases[0];

        var updated = await sut.SwapAliasWithPrimaryAsync(
            InMemoryPatientAliasesService.DemoPatientId, target.Id);

        Assert.Equal("Freddy", updated.FirstName);
    }
}
