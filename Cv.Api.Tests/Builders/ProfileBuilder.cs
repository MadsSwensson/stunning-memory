using Cv.Api.Domain;

namespace Cv.Api.Tests.Builders;

public class ProfileBuilder
{
    private readonly string _id;
    private string _name = "Name";

    private ProfileBuilder(string id)
    {
        _id = id;
    }

    public static ProfileBuilder Create(string id)
    {
        return new ProfileBuilder(id);
    }

    public ProfileBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public Profile Build()
    {
        return new Profile(_id, _name);
    }
}