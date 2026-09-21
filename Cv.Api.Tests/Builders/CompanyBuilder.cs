using Cv.Api.Domain;

namespace Cv.Api.Tests.Builders;

public sealed class CompanyBuilder
{
    private readonly string _id;
    private string _name = "Name";
    private string _role = "Role";
    private string _period = "Period";
    private string _description = "Description";

    private CompanyBuilder(string id)
    {
        _id = id;
    }

    public static CompanyBuilder Create(string id)
    {
        return new CompanyBuilder(id);
    }

    public CompanyBuilder WithName(string name)
    {
        _name = name;
        return this;
    }

    public CompanyBuilder WithRole(string role)
    {
        _role = role;
        return this;
    }

    public CompanyBuilder WithPeriod(string period)
    {
        _period = period;
        return this;
    }

    public CompanyBuilder WithDescription(string description)
    {
        _description = description;
        return this;
    }

    public Company Build()
    {
        return new Company(_id, _name, _role, _period, _description);
    }
}
