using Cv.Api.Domain;
using Cv.Api.Tests.Builders;
using Cv.Api.Tests.Infrastructure;

namespace Cv.Api.Tests;

public sealed class EducationGraphQlTests : GraphQlTestBase
{
    private const string Query =
        """
        query Education($id: ID!) {
          item: education(id: $id) { id institution program period description }
        }
        """;

    [Fact]
    public async Task Detail_returns_complete_education()
    {
        var profile = ProfileBuilder.Create("profile-education-detail").Build();
        var education = EducationBuilder.Create("education-detail")
            .WithInstitution("Test University")
            .WithProgram("MSc, Software Engineering")
            .WithPeriod("2020-2022")
            .WithDescription("Studied distributed systems.")
            .Build();
        Profiles.Add(profile);
        EducationByProfileId[profile.Id] = [education];

        var data = await SendAsync<DetailResponse<Education>>(
            Query,
            new { id = "EDUCATION-DETAIL" });

        Assert.Equal(education, data.Item);
    }

    [Fact]
    public async Task Detail_returns_null_without_errors_for_unknown_id()
    {
        var profile = ProfileBuilder.Create("profile-education-unknown").Build();
        var education = EducationBuilder.Create("known-education").Build();
        Profiles.Add(profile);
        EducationByProfileId[profile.Id] = [education];

        var data = await SendAsync<DetailResponse<Education>>(
            Query,
            new { id = "unknown-education" });

        Assert.Null(data.Item);
    }
}
