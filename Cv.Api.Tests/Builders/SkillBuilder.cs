using Cv.Api.Domain;

namespace Cv.Api.Tests.Builders;

public sealed class SkillBuilder
{
    private readonly string _id;
    private string _name = "Name";
    private string _level = "Level";
    private string _description = "Description";

    private SkillBuilder(string id)
    {
        _id = id;
    }

    public static SkillBuilder Create(string id)
    {
        return new SkillBuilder(id);
    }

    public SkillBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public SkillBuilder WithLevel(string level)
    {
        _level = level;
        return this;
    }

    public SkillBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Skill Build()
    {
        return new Skill(_id, _name, _level, _description);
    }
}
