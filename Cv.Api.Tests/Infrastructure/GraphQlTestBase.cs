using Cv.Api.Domain;
using Cv.Api.Infrastructure;
using GraphQL;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;

namespace Cv.Api.Tests.Infrastructure;

public abstract class GraphQlTestBase : IDisposable
{
    private readonly GraphQlWebApplicationFactory _factory;
    private readonly HttpClient _httpClient;
    private readonly GraphQLHttpClient _client;

    protected GraphQlTestBase()
    {
        var queryService = new InMemoryProfileQueryService(
            Profiles,
            CompaniesByProfileId,
            ProjectsByProfileId,
            EducationByProfileId,
            SkillsByProfileId);
        _factory = new GraphQlWebApplicationFactory(queryService);
        _httpClient = _factory.CreateClient();
        _client = new GraphQLHttpClient(
            new Uri(_httpClient.BaseAddress!, "/graphql"),
            new SystemTextJsonSerializer(),
            _httpClient);
    }

    protected List<Profile> Profiles { get; } = [];

    protected Dictionary<string, IReadOnlyList<Company>> CompaniesByProfileId { get; } = [];

    protected Dictionary<string, IReadOnlyList<Project>> ProjectsByProfileId { get; } = [];

    protected Dictionary<string, IReadOnlyList<Education>> EducationByProfileId { get; } = [];

    protected Dictionary<string, IReadOnlyList<Skill>> SkillsByProfileId { get; } = [];

    protected async Task<TResponse> SendAsync<TResponse>(
        string query,
        object? variables = null)
    {
        var response = await _client.SendQueryAsync<TResponse>(
            new GraphQLRequest
            {
                Query = query,
                Variables = variables
            });

        Assert.Null(response.Errors);
        Assert.NotNull(response.Data);
        return response.Data;
    }

    public void Dispose()
    {
        _client.Dispose();
        _httpClient.Dispose();
        _factory.Dispose();
        GC.SuppressFinalize(this);
    }
}

internal sealed record DetailResponse<T>(T? Item);

internal sealed record ListResponse<T>(T[]? Items);
