using Cv.Api.Domain;

namespace Cv.Api.Infrastructure;

internal static class SampleData
{
    private const string Profile1 = "profile-1";
    private const string Profile2 = "profile-2";
    private const string Profile3 = "profile-3";

    public static IReadOnlyList<Profile> Profiles { get; } =
    [
        new(Profile1, "Sample Profile"),
        new(Profile2, "Frontend Specialist"),
        new(Profile3, "Platform Engineer")
    ];

    public static IReadOnlyDictionary<string, IReadOnlyList<Company>> CompaniesByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Company>>
        {
            [Profile1] =
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
            ],
            [Profile2] =
            [
                new(
                    "company-3",
                    "Fabrikam Studio",
                    "Senior Frontend Engineer",
                    "2021-present",
                    "Builds accessible design systems and customer-facing web applications.")
            ],
            [Profile3] =
            [
                new(
                    "company-4",
                    "Adventure Works",
                    "Platform Engineer",
                    "2020-present",
                    "Operates cloud infrastructure and internal developer platforms.")
            ]
        };

    public static IReadOnlyDictionary<string, IReadOnlyList<Project>> ProjectsByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Project>>
        {
            [Profile1] =
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
            ],
            [Profile2] =
            [
                new(
                    "project-3",
                    "Component Library",
                    "A reusable and accessible UI component system.",
                    "Standardized product interfaces and reduced duplicated frontend work.")
            ],
            [Profile3] =
            [
                new(
                    "project-4",
                    "Observability Platform",
                    "Centralized metrics, logs, and distributed traces.",
                    "Improved incident response and service-level monitoring across teams.")
            ]
        };

    public static IReadOnlyDictionary<string, IReadOnlyList<Education>> EducationByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Education>>
        {
            [Profile1] =
            [
                new(
                    "education-1",
                    "Example University",
                    "BSc, Computer Science",
                    "2016-2019",
                    "Focused on software engineering, distributed systems, and databases.")
            ],
            [Profile2] =
            [
                new(
                    "education-2",
                    "City Technical University",
                    "MSc, Human-Computer Interaction",
                    "2018-2020",
                    "Focused on interaction design, accessibility, and user research.")
            ],
            [Profile3] =
            [
                new(
                    "education-3",
                    "Institute of Technology",
                    "MSc, Cloud Computing",
                    "2017-2019",
                    "Focused on distributed systems, networking, and cloud architecture.")
            ]
        };

    public static IReadOnlyDictionary<string, IReadOnlyList<Skill>> SkillsByProfileId { get; } =
        new Dictionary<string, IReadOnlyList<Skill>>
        {
            [Profile1] =
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
            ],
            [Profile2] =
            [
                new(
                    "skill-3",
                    "TypeScript and React",
                    "Advanced",
                    "Accessible component design, state management, and frontend testing.")
            ],
            [Profile3] =
            [
                new(
                    "skill-4",
                    "Kubernetes",
                    "Advanced",
                    "Container orchestration, deployment automation, and production operations.")
            ]
        };
}
