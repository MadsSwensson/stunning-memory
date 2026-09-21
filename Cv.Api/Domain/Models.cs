namespace Cv.Api.Domain;

public sealed record Profile(string Id, string Name);

public sealed record Company(
    string Id,
    string Name,
    string Role,
    string Period,
    string Description);

public sealed record Project(
    string Id,
    string Name,
    string Summary,
    string Description);

public sealed record Education(
    string Id,
    string Institution,
    string Program,
    string Period,
    string Description);

public sealed record Skill(
    string Id,
    string Name,
    string Level,
    string Description);
