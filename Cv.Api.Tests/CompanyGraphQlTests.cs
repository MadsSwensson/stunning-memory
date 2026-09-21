using Cv.Api.Domain;
using Cv.Api.Tests.Builders;
using Cv.Api.Tests.Infrastructure;

namespace Cv.Api.Tests;

public sealed class CompanyGraphQlTests : GraphQlTestBase
{
    private const string ProfilesQuery =
        """
        query Profiles {
          items: profiles { id name }
        }
        """;

    private const string DetailsQuery =
        """
        query Company($id: ID!) {
          item: company(id: $id) { id name role period description }
        }
        """;

    [Fact]
    public async Task Profiles_returns_all_profiles_as_list_items()
    {
        var firstProfile = ProfileBuilder.Create("profile-1")
            .WithName("First Profile")
            .Build();
        var secondProfile = ProfileBuilder.Create("profile-2")
            .WithName("Second Profile")
            .Build();
        Profiles.AddRange([firstProfile, secondProfile]);

        var data = await SendAsync<ListResponse<Profile>>(ProfilesQuery);

        Assert.NotNull(data.Items);
        Assert.Equal(2, data.Items.Length);
        Assert.Collection(
            data.Items,
            item => Assert.Equal(
                new Profile("profile-1", "First Profile"),
                item),
            item => Assert.Equal(
                new Profile("profile-2", "Second Profile"),
                item));
    }

    [Fact]
    public async Task Detail_returns_complete_company()
    {
        var profile = ProfileBuilder.Create("profile-company-detail").Build();
        var company = CompanyBuilder.Create("company-detail")
            .WithName("Detail Company")
            .WithRole("Principal Engineer")
            .WithPeriod("2024-present")
            .WithDescription("Builds reliable services.")
            .Build();
        Profiles.Add(profile);
        CompaniesByProfileId[profile.Id] = [company];

        var data = await SendAsync<DetailResponse<Company>>(
            DetailsQuery,
            new { id = "COMPANY-DETAIL" });

        Assert.Equal(company, data.Item);
    }

    [Fact]
    public async Task Detail_returns_null_without_errors_for_unknown_id()
    {
        var profile = ProfileBuilder.Create("profile-company-unknown").Build();
        var company = CompanyBuilder.Create("known-company").Build();
        Profiles.Add(profile);
        CompaniesByProfileId[profile.Id] = [company];

        var data = await SendAsync<DetailResponse<Company>>(
            DetailsQuery,
            new { id = "unknown-company" });

        Assert.Null(data.Item);
    }
}
