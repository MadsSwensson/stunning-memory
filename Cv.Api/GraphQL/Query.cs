using Cv.Api.Application;
using GraphQL;
using GraphQL.Types;

namespace Cv.Api.GraphQL;

public sealed class Query : ObjectGraphType
{
    public Query()
    {
        Name = "Query";

        Field<ProfileGraphType>("profile")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async context => await GetService(context).GetProfileByIdAsync(
                context.GetArgument<string>("id"),
                context.CancellationToken));

        Field<NonNullGraphType<ListGraphType<NonNullGraphType<ProfileGraphType>>>>("profiles")
            .ResolveAsync(async context => await GetService(context).GetProfilesAsync(
                context.CancellationToken));

        Field<CompanyGraphType>("company")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async context => await GetService(context).GetCompanyByIdAsync(
                context.GetArgument<string>("id"),
                context.CancellationToken));

        Field<ProjectGraphType>("project")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async context => await GetService(context).GetProjectByIdAsync(
                context.GetArgument<string>("id"),
                context.CancellationToken));

        Field<EducationGraphType>("education")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async context => await GetService(context).GetEducationByIdAsync(
                context.GetArgument<string>("id"),
                context.CancellationToken));

        Field<SkillGraphType>("skill")
            .Argument<NonNullGraphType<IdGraphType>>("id")
            .ResolveAsync(async context => await GetService(context).GetSkillByIdAsync(
                context.GetArgument<string>("id"),
                context.CancellationToken));
    }

    private static IProfileQueryService GetService(IResolveFieldContext context) =>
        (context.RequestServices
            ?? throw new InvalidOperationException("Request services are unavailable."))
        .GetRequiredService<IProfileQueryService>();
}
