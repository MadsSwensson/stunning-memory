using Cv.Api.Application;
using Cv.Api.Domain;

namespace Cv.Api.Infrastructure;

public sealed class InMemoryProfileQueryService : IProfileQueryService
{
    private readonly IReadOnlyList<Profile> _profiles;
    private readonly IReadOnlyDictionary<string, IReadOnlyList<Company>> _companiesByProfileId;
    private readonly IReadOnlyDictionary<string, IReadOnlyList<Project>> _projectsByProfileId;
    private readonly IReadOnlyDictionary<string, IReadOnlyList<Education>> _educationByProfileId;
    private readonly IReadOnlyDictionary<string, IReadOnlyList<Skill>> _skillsByProfileId;

    public InMemoryProfileQueryService(
        IReadOnlyList<Profile> profiles,
        IReadOnlyDictionary<string, IReadOnlyList<Company>> companiesByProfileId,
        IReadOnlyDictionary<string, IReadOnlyList<Project>> projectsByProfileId,
        IReadOnlyDictionary<string, IReadOnlyList<Education>> educationByProfileId,
        IReadOnlyDictionary<string, IReadOnlyList<Skill>> skillsByProfileId)
    {
        _profiles = profiles;
        _companiesByProfileId = companiesByProfileId;
        _projectsByProfileId = projectsByProfileId;
        _educationByProfileId = educationByProfileId;
        _skillsByProfileId = skillsByProfileId;
    }

    public Task<Profile?> GetProfileByIdAsync(
        string id,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_profiles.FirstOrDefault(profile => string.Equals(
            profile.Id,
            id,
            StringComparison.OrdinalIgnoreCase)));
    }

    public Task<IReadOnlyList<Profile>> GetProfilesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_profiles);
    }

    public Task<IReadOnlyList<Company>> GetCompaniesAsync(
        string profileId,
        CancellationToken cancellationToken) =>
        GetItemsAsync(profileId, _companiesByProfileId, cancellationToken);

    public Task<IReadOnlyList<Project>> GetProjectsAsync(
        string profileId,
        CancellationToken cancellationToken) =>
        GetItemsAsync(profileId, _projectsByProfileId, cancellationToken);

    public Task<IReadOnlyList<Education>> GetEducationAsync(
        string profileId,
        CancellationToken cancellationToken) =>
        GetItemsAsync(profileId, _educationByProfileId, cancellationToken);

    public Task<IReadOnlyList<Skill>> GetSkillsAsync(
        string profileId,
        CancellationToken cancellationToken) =>
        GetItemsAsync(profileId, _skillsByProfileId, cancellationToken);

    public Task<Company?> GetCompanyByIdAsync(
        string id,
        CancellationToken cancellationToken) =>
        FindByIdAsync(id, _companiesByProfileId, company => company.Id, cancellationToken);

    public Task<Project?> GetProjectByIdAsync(
        string id,
        CancellationToken cancellationToken) =>
        FindByIdAsync(id, _projectsByProfileId, project => project.Id, cancellationToken);

    public Task<Education?> GetEducationByIdAsync(
        string id,
        CancellationToken cancellationToken) =>
        FindByIdAsync(id, _educationByProfileId, education => education.Id, cancellationToken);

    public Task<Skill?> GetSkillByIdAsync(
        string id,
        CancellationToken cancellationToken) =>
        FindByIdAsync(id, _skillsByProfileId, skill => skill.Id, cancellationToken);

    private static Task<IReadOnlyList<T>> GetItemsAsync<T>(
        string profileId,
        IReadOnlyDictionary<string, IReadOnlyList<T>> itemsByProfileId,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(
            itemsByProfileId.GetValueOrDefault(profileId) ?? (IReadOnlyList<T>)[]);
    }

    private static Task<T?> FindByIdAsync<T>(
        string id,
        IReadOnlyDictionary<string, IReadOnlyList<T>> itemsByProfileId,
        Func<T, string> idSelector,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var item = itemsByProfileId.Values
            .SelectMany(items => items)
            .FirstOrDefault(item => string.Equals(
                idSelector(item),
                id,
                StringComparison.OrdinalIgnoreCase));

        return Task.FromResult(item);
    }
}
