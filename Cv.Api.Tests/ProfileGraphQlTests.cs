using Cv.Api.Domain;
using Cv.Api.Tests.Builders;
using Cv.Api.Tests.Infrastructure;

namespace Cv.Api.Tests;

public sealed class ProfileGraphQlTests : GraphQlTestBase
{
    [Fact]
    public async Task Overview_returns_profile_and_non_empty_collections()
    {
        var profile = ProfileBuilder.Create("profile-overview")
            .WithName("Overview Profile")
            .Build();
        var company = CompanyBuilder.Create("company-overview")
            .WithName("Overview Company")
            .Build();
        var project = ProjectBuilder.Create("project-overview")
            .WithSummary("Overview project summary")
            .Build();
        var education = EducationBuilder.Create("education-overview")
            .WithProgram("Overview program")
            .Build();
        var skill = SkillBuilder.Create("skill-overview")
            .WithLevel("Expert")
            .Build();
        Profiles.Add(profile);
        CompaniesByProfileId[profile.Id] = [company];
        ProjectsByProfileId[profile.Id] = [project];
        EducationByProfileId[profile.Id] = [education];
        SkillsByProfileId[profile.Id] = [skill];

        const string query =
            """
            query ProfileOverview {
              profile { id name }
              overview: profile {
                companies { id name role period description }
                projects { id name summary description }
                education { id institution program period description }
                skills { id name level description }
              }
            }
            """;

        var data = await SendAsync<ProfileOverviewResponse>(query);

        Assert.Equal(profile, data.Profile);
        Assert.NotEmpty(data.Overview.Companies);
        Assert.NotEmpty(data.Overview.Projects);
        Assert.NotEmpty(data.Overview.Education);
        Assert.NotEmpty(data.Overview.Skills);
        Assert.Contains(
            data.Overview.Companies,
            item => item.Id == company.Id && item.Name == company.Name);
        Assert.Contains(
            data.Overview.Projects,
            item => item.Id == project.Id && item.Summary == project.Summary);
        Assert.Contains(
            data.Overview.Education,
            item => item.Id == education.Id && item.Program == education.Program);
        Assert.Contains(
            data.Overview.Skills,
            item => item.Id == skill.Id && item.Level == skill.Level);
    }

    private sealed record ProfileOverviewResponse(
        Profile Profile,
        ProfileCollections Overview);

    private sealed record ProfileCollections(
        IReadOnlyList<Company> Companies,
        IReadOnlyList<Project> Projects,
        IReadOnlyList<Education> Education,
        IReadOnlyList<Skill> Skills);
}
