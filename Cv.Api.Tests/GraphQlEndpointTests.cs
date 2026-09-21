using Cv.Api.Domain;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Cv.Api.Tests;

public sealed class GraphQlEndpointTests :
    IClassFixture<WebApplicationFactory<Program>>,
    IDisposable
{
    private readonly HttpClient _httpClient;
    private readonly GraphQLHttpClient _client;

    public GraphQlEndpointTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
        var endpoint = new Uri(_httpClient.BaseAddress!, "/graphql");

        _client = new GraphQLHttpClient(
            endpoint,
            new SystemTextJsonSerializer(),
            _httpClient);
    }

    [Fact]
    public async Task GraphQl_endpoint_returns_profile_lists_and_typed_details()
    {
        const string query =
            """
            query ProfileAndDetails(
              $companyId: ID!
              $projectId: ID!
              $educationId: ID!
              $skillId: ID!
            ) {
              profile {
                companies { id name role period description }
                projects { id name summary description }
                education { id institution program period description }
                skills { id name level description }
              }
              company(id: $companyId) { id name role period description }
              project(id: $projectId) { id name summary description }
              education(id: $educationId) { id institution program period description }
              skill(id: $skillId) { id name level description }
            }
            """;

        var request = new GraphQLRequest
        {
            Query = query,
            Variables = new
            {
                companyId = "company-1",
                projectId = "project-1",
                educationId = "education-1",
                skillId = "skill-1"
            }
        };

        var response = await _client.SendQueryAsync<QueryResponse>(request);

        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        Assert.Contains(
            response.Data.Profile.Companies,
            company => company.Id == "company-1");
        Assert.Contains(
            response.Data.Profile.Projects,
            project => project.Id == "project-1");
        Assert.Contains(
            response.Data.Profile.Education,
            education => education.Id == "education-1");
        Assert.Contains(
            response.Data.Profile.Skills,
            skill => skill.Id == "skill-1");
        Assert.Equal("Northwind Labs", response.Data.Company.Name);
        Assert.Equal("CV GraphQL API", response.Data.Project.Name);
        Assert.Equal("Example University", response.Data.Education.Institution);
        Assert.Equal("C# and .NET", response.Data.Skill.Name);
    }

    public void Dispose() => _client.Dispose();

    private sealed record QueryResponse(
        ProfileResponse Profile,
        Company Company,
        Project Project,
        Education Education,
        Skill Skill);

    private sealed record ProfileResponse(
        IReadOnlyList<Company> Companies,
        IReadOnlyList<Project> Projects,
        IReadOnlyList<Education> Education,
        IReadOnlyList<Skill> Skills);
}
