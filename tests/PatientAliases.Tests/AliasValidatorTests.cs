using PatientAliases.Web.DTOs;
using PatientAliases.Web.Validation;
using Xunit;

namespace PatientAliases.Tests;

public class AliasValidatorTests
{
    [Fact]
    public void Validate_ValidAlias_ReturnsNoErrors()
    {
        var aliases = new[] { new AliasDto { Last = "Steinberg", First = "Freddy", Middle = "J", Suffix = "JR" } };
        var results = AliasValidator.Validate(aliases);
        Assert.Empty(results);
    }

    [Fact]
    public void Validate_MissingLast_ReturnsError()
    {
        var aliases = new[] { new AliasDto { Last = "", First = "Freddy" } };
        var results = AliasValidator.Validate(aliases);
        Assert.Contains(results, r => r.ErrorMessage!.Contains("Last name is required"));
    }

    [Fact]
    public void Validate_SuffixWithInvalidChars_ReturnsError()
    {
        var aliases = new[] { new AliasDto { Last = "A", First = "B", Suffix = "1$" } };
        var results = AliasValidator.Validate(aliases);
        Assert.Contains(results, r => r.ErrorMessage!.Contains("Suffix"));
    }
}
