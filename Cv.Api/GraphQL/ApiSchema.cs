using GraphQL.Types;

namespace Cv.Api.GraphQL;

public sealed class ApiSchema : Schema
{
    public ApiSchema(IServiceProvider services)
        : base(services)
    {
        Query = services.GetRequiredService<Query>();
    }
}
