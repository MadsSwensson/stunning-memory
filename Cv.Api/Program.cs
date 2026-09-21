using Cv.Api.Application;
using Cv.Api.GraphQL;
using Cv.Api.Infrastructure;
using GraphQL;
using GraphQL.Server.Ui.GraphiQL;

var builder = WebApplication.CreateBuilder(args);

AddInfrastructure(builder);

AddGraphQl(builder);

builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseGraphQL<ApiSchema>("/graphql");
app.UseGraphQLGraphiQL("/ui/graphiql", new GraphiQLOptions
{
    GraphQLEndPoint = "/graphql"
});

app.MapHealthChecks("/health");

app.Run();

void AddInfrastructure(WebApplicationBuilder infrastructureBuilder)
{
    infrastructureBuilder.Services.AddSingleton(SampleData.Profiles);
    infrastructureBuilder.Services.AddSingleton(SampleData.CompaniesByProfileId);
    infrastructureBuilder.Services.AddSingleton(SampleData.ProjectsByProfileId);
    infrastructureBuilder.Services.AddSingleton(SampleData.EducationByProfileId);
    infrastructureBuilder.Services.AddSingleton(SampleData.SkillsByProfileId);
    infrastructureBuilder.Services.AddSingleton<IProfileQueryService, InMemoryProfileQueryService>();
}

void AddGraphQl(WebApplicationBuilder gqlBuilder)
{
    gqlBuilder.Services.AddSingleton<Query>();
    gqlBuilder.Services.AddSingleton<CompanyGraphType>();
    gqlBuilder.Services.AddSingleton<ProjectGraphType>();
    gqlBuilder.Services.AddSingleton<EducationGraphType>();
    gqlBuilder.Services.AddSingleton<SkillGraphType>();
    gqlBuilder.Services.AddSingleton<ProfileGraphType>();
    
    builder.Services.AddGraphQL(graphQl => graphQl
        .AddSchema<ApiSchema>()
        .AddSystemTextJson());
}

public partial class Program;
