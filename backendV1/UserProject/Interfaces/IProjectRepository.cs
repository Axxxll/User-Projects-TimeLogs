public interface IProjectRepository
{
    public ICollection<ProjectUserHoursDto> GetTopTenProjects();
}