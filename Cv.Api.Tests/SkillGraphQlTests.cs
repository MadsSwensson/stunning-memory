using Cv.Api.Domain;
using Cv.Api.Tests.Builders;
using Cv.Api.Tests.Infrastructure;

namespace Cv.Api.Tests;

public sealed class SkillGraphQlTests : GraphQlTestBase
{
    private const string Query =
        """
        query Skill($id: ID!) {
          item: skill(id: $id) { id name level description }
        }
        """;

    [Fact]
    public async Task Detail_returns_complete_skill()
    {
        var profile = ProfileBuilder.Create("profile-skill-detail").Build();
        var skill = SkillBuilder.Create("skill-detail")
            .WithName("GraphQL")
            .WithLevel("Advanced")
            .WithDescription("Designs and tests GraphQL APIs.")
            .Build();
        Profiles.Add(profile);
        SkillsByProfileId[profile.Id] = [skill];

        var data = await SendAsync<DetailResponse<Skill>>(
            Query,
            new { id = "SKILL-DETAIL" });

        Assert.Equal(skill, data.Item);
    }

    [Fact]
    public async Task Detail_returns_null_without_errors_for_unknown_id()
    {
        var profile = ProfileBuilder.Create("profile-skill-unknown").Build();
        var skill = SkillBuilder.Create("known-skill").Build();
        Profiles.Add(profile);
        SkillsByProfileId[profile.Id] = [skill];

        var data = await SendAsync<DetailResponse<Skill>>(
            Query,
            new { id = "unknown-skill" });

        Assert.Null(data.Item);
    }
}
