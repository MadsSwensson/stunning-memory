using Cv.Api.Application;
using Cv.Api.GraphQL;
using Cv.Api.Infrastructure;
using GraphQL;
using GraphQL.Server.Ui.GraphiQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(SampleData.Profiles);
builder.Services.AddSingleton(SampleData.CompaniesByProfileId);
builder.Services.AddSingleton(SampleData.ProjectsByProfileId);
builder.Services.AddSingleton(SampleData.EducationByProfileId);
builder.Services.AddSingleton(SampleData.SkillsByProfileId);

builder.Services.AddSingleton<IProfileQueryService, InMemoryProfileQueryService>();
builder.Services.AddSingleton<Query>();

builder.Services.AddSingleton<CompanyGraphType>();
builder.Services.AddSingleton<ProjectGraphType>();
builder.Services.AddSingleton<EducationGraphType>();
builder.Services.AddSingleton<SkillGraphType>();
builder.Services.AddSingleton<ProfileGraphType>();

builder.Services.AddGraphQL(graphQl => graphQl
    .AddSchema<ApiSchema>()
    .AddSystemTextJson());

var app = builder.Build();

app.UseGraphQL<ApiSchema>("/graphql");
app.UseGraphQLGraphiQL("/ui/graphiql", new GraphiQLOptions
{
    GraphQLEndPoint = "/graphql"
});

app.Run();

public partial class Program;
