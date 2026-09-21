using Cv.Api.Domain;

namespace Cv.Api.Application;

public interface IProfileQueryService
{
    Task<Profile> GetProfileAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<Company>> GetCompaniesAsync(
        string profileId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Project>> GetProjectsAsync(
        string profileId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Education>> GetEducationAsync(
        string profileId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Skill>> GetSkillsAsync(
        string profileId,
        CancellationToken cancellationToken);

    Task<Company?> GetCompanyByIdAsync(string id, CancellationToken cancellationToken);

    Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken);

    Task<Education?> GetEducationByIdAsync(string id, CancellationToken cancellationToken);

    Task<Skill?> GetSkillByIdAsync(string id, CancellationToken cancellationToken);
}
