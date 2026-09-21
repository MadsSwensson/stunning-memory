using GraphQL.Types;

namespace Cv.Api.GraphQL;

public sealed class ApiSchema : Schema
{
    public ApiSchema(IServiceProvider services, Query query)
        : base(services)
    {
        Query = query;
    }
}
