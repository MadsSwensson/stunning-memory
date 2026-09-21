using Cv.Api.Domain;
using Cv.Api.Application;
using GraphQL;
using GraphQL.Types;

namespace Cv.Api.GraphQL;

public sealed class ProfileGraphType : ObjectGraphType<Profile>
{
    public ProfileGraphType()
    {
        Name = "Profile";
        Field<NonNullGraphType<IdGraphType>>("id").Resolve(context => context.Source.Id);
        Field(profile => profile.Name);
        Field<NonNullGraphType<ListGraphType<NonNullGraphType<CompanyGraphType>>>>("companies")
            .ResolveAsync(async context => await GetService(context).GetCompaniesAsync(
                context.Source.Id,
                context.CancellationToken));
        Field<NonNullGraphType<ListGraphType<NonNullGraphType<ProjectGraphType>>>>("projects")
            .ResolveAsync(async context => await GetService(context).GetProjectsAsync(
                context.Source.Id,
                context.CancellationToken));
        Field<NonNullGraphType<ListGraphType<NonNullGraphType<EducationGraphType>>>>("education")
            .ResolveAsync(async context => await GetService(context).GetEducationAsync(
                context.Source.Id,
                context.CancellationToken));
        Field<NonNullGraphType<ListGraphType<NonNullGraphType<SkillGraphType>>>>("skills")
            .ResolveAsync(async context => await GetService(context).GetSkillsAsync(
                context.Source.Id,
                context.CancellationToken));
    }

    private static IProfileQueryService GetService(IResolveFieldContext context) =>
        (context.RequestServices
            ?? throw new InvalidOperationException("Request services are unavailable."))
        .GetRequiredService<IProfileQueryService>();
}

public sealed class CompanyGraphType : ObjectGraphType<Company>
{
    public CompanyGraphType()
    {
        Name = "Company";
        Field<NonNullGraphType<IdGraphType>>("id").Resolve(context => context.Source.Id);
        Field(company => company.Name);
        Field(company => company.Role);
        Field(company => company.Period);
        Field(company => company.Description);
    }
}

public sealed class ProjectGraphType : ObjectGraphType<Project>
{
    public ProjectGraphType()
    {
        Name = "Project";
        Field<NonNullGraphType<IdGraphType>>("id").Resolve(context => context.Source.Id);
        Field(project => project.Name);
        Field(project => project.Summary);
        Field(project => project.Description);
    }
}

public sealed class EducationGraphType : ObjectGraphType<Education>
{
    public EducationGraphType()
    {
        Name = "Education";
        Field<NonNullGraphType<IdGraphType>>("id").Resolve(context => context.Source.Id);
        Field(education => education.Institution);
        Field(education => education.Program);
        Field(education => education.Period);
        Field(education => education.Description);
    }
}

public sealed class SkillGraphType : ObjectGraphType<Skill>
{
    public SkillGraphType()
    {
        Name = "Skill";
        Field<NonNullGraphType<IdGraphType>>("id").Resolve(context => context.Source.Id);
        Field(skill => skill.Name);
        Field(skill => skill.Level);
        Field(skill => skill.Description);
    }
}
