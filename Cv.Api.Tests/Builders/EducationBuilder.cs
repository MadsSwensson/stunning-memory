using Cv.Api.Domain;

namespace Cv.Api.Tests.Builders;

public sealed class EducationBuilder
{
    private readonly string _id;
    private string _institution = "Institution";
    private string _program = "Program";
    private string _period = "Period";
    private string _description = "Description";

    private EducationBuilder(string id)
    {
        _id = id;
    }

    public static EducationBuilder Create(string id)
    {
        return new EducationBuilder(id);
    }

    public EducationBuilder WithInstitution(string institution)
    {
        _institution = institution;
        return this;
    }

    public EducationBuilder WithProgram(string program)
    {
        _program = program;
        return this;
    }

    public EducationBuilder WithPeriod(string period)
    {
        _period = period;
        return this;
    }

    public EducationBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Education Build()
    {
        return new Education(_id, _institution, _program, _period, _description);
    }
}
