using Cv.Api.Domain;

namespace Cv.Api.Tests.Builders;

public sealed class ProjectBuilder
{
    private readonly string _id;
    private string _name  = "Name";
    private string _summary = "Summary";
    private string _description = "Description";

    private ProjectBuilder(string id)
    {
        _id = id;
    }

    public static ProjectBuilder Create(string id)
    {
        return new ProjectBuilder(id);
    }

    public ProjectBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public ProjectBuilder WithSummary(string summary)
    {
        _summary = summary;
        return this;
    }

    public ProjectBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Project Build()
    {
        return new Project(_id, _name, _summary, _description);
    }
}
