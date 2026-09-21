using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Cv.Api.Tests;

public sealed class RootEndpointTests
{
    [Fact]
    public async Task Root_redirects_to_graphiql()
    {
        using var factory = new WebApplicationFactory<Program>();
        using var client = factory.CreateClient(
            new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

        var response = await client.GetAsync("/");

        Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);
        Assert.Equal("/ui/graphiql", response.Headers.Location?.OriginalString);
    }
}
