using Cv.Api.Domain;

namespace Cv.Api.Infrastructure;

internal static class SampleData
{
    private const string ProfileId = "profile-1";

    public static IReadOnlyList<Profile> Profiles { get; } =
    [
        new(ProfileId, "Sample Profile")
    ];

    public static IReadOnlyDictionary<string, IReadOnlyList<Company>> CompaniesByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Company>>(StringComparer.OrdinalIgnoreCase)
        {
            [ProfileId] =
            [
                new(
                    "company-1",
                    "Northwind Labs",
                    "Senior Software Engineer",
                    "2022-present",
                    "Builds and maintains reliable APIs and developer tooling."),
                new(
                    "company-2",
                    "Contoso Digital",
                    "Software Engineer",
                    "2019-2022",
                    "Delivered customer-facing services in a cross-functional team.")
            ]
        };

    public static IReadOnlyDictionary<string, IReadOnlyList<Project>> ProjectsByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Project>>(StringComparer.OrdinalIgnoreCase)
        {
            [ProfileId] =
            [
                new(
                    "project-1",
                    "CV GraphQL API",
                    "A small GraphQL.NET API built with .NET 10.",
                    "Demonstrates a deliberately small GraphQL API organized by responsibility."),
                new(
                    "project-2",
                    "Delivery Dashboard",
                    "A dashboard for tracking software delivery metrics.",
                    "Made deployment and lead-time trends visible to product teams.")
            ]
        };

    public static IReadOnlyDictionary<string, IReadOnlyList<Education>> EducationByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Education>>(StringComparer.OrdinalIgnoreCase)
        {
            [ProfileId] =
            [
                new(
                    "education-1",
                    "Example University",
                    "BSc, Computer Science",
                    "2016-2019",
                    "Focused on software engineering, distributed systems, and databases.")
            ]
        };

    public static IReadOnlyDictionary<string, IReadOnlyList<Skill>> SkillsByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Skill>>(StringComparer.OrdinalIgnoreCase)
        {
            [ProfileId] =
            [
                new(
                    "skill-1",
                    "C# and .NET",
                    "Advanced",
                    "Modern C#, ASP.NET Core, testing, and API design."),
                new(
                    "skill-2",
                    "GraphQL",
                    "Intermediate",
                    "Schema design, queries, and HTTP API integration.")
            ]
        };
}
