using Cv.Api.Domain;
using Cv.Api.Tests.Builders;
using Cv.Api.Tests.Infrastructure;

namespace Cv.Api.Tests;

public sealed class ProjectGraphQlTests : GraphQlTestBase
{
    private const string Query =
        """
        query Project($id: ID!) {
          item: project(id: $id) { id name summary description }
        }
        """;

    [Fact]
    public async Task Detail_returns_complete_project()
    {
        var profile = ProfileBuilder.Create("profile-project-detail").Build();
        var project = ProjectBuilder.Create("project-detail")
            .WithName("Detail Project")
            .WithSummary("A concise project summary.")
            .WithDescription("A complete project description.")
            .Build();
        Profiles.Add(profile);
        ProjectsByProfileId[profile.Id] = [project];

        var data = await SendAsync<DetailResponse<Project>>(
            Query,
            new { id = "PROJECT-DETAIL" });

        Assert.Equal(project, data.Item);
    }

    [Fact]
    public async Task Detail_returns_null_without_errors_for_unknown_id()
    {
        var profile = ProfileBuilder.Create("profile-project-unknown").Build();
        var project = ProjectBuilder.Create("known-project").Build();
        Profiles.Add(profile);
        ProjectsByProfileId[profile.Id] = [project];

        var data = await SendAsync<DetailResponse<Project>>(
            Query,
            new { id = "unknown-project" });

        Assert.Null(data.Item);
    }
}
