
using MVCProject.Data.Interfaces;
using MVCProject.Models;
using MVCProject.Dtos;
using MVCProject.Factories;
namespace MVCProject.Services;


public class ProjectService(IProjectRepository projectRepository)
{
    private readonly IProjectRepository _projectRepository = projectRepository;

    // Hämtar alla projekt
    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        var projects = await _projectRepository.GetAllAsync(); 
        return projects.Select(p => ProjectFactory.Create(p)); 
    }

    // Hämtar projekt genom ID
    public async Task<Project> GetProjectByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out var projectId))
        {
            throw new ArgumentException("Ogiltigt ID-format", nameof(id));
        }

        var project = await _projectRepository.GetAsync(x => x.Id == id); 
        if (project == null)
        {
            throw new KeyNotFoundException($"Projekt med ID {id} hittades inte.");
        }

        return ProjectFactory.Create(project); // Skapa DTO från ProjectEntity
    }


    // Skapar ett nytt projekt
    // fick hjälp av ChatGPT för att skapa ett nytt projekt,  Omvandla från DTO till entity och anropa repository för att skapa projekt
    public async Task<Project> CreateProjectAsync(ProjectRegisteration projectRegisteration)
    {
        var projectEntity = ProjectFactory.Create(projectRegisteration); 
        await _projectRepository.CreateAsync(projectEntity); 
        Console.WriteLine("data skapades");

        return ProjectFactory.Create(projectEntity); 
    }



    // Uppdaterar ett projekt
    // fick hjälp av ChatGPT för att uppdatera ett projekt, Omvandla från DTO till entity, Anropa repository för att uppdatera projekt
    public async Task UpdateProjectAsync(string id, ProjectUpdateForm projectUpdateForm)
    {
        var projectEntity = ProjectFactory.Create(projectUpdateForm); 
        await _projectRepository.UpdateAsync(projectEntity); 
    }

    public async Task<bool> DeleteProjectAsync(string id)
    {
        if (!Guid.TryParse(id, out var projectId))
        {
            throw new ArgumentException("Ogiltigt ID-format", nameof(id));
        }

        var isDeleted = await _projectRepository.DeleteAsync(x => x.Id == id); // Anropa repository för att ta bort projektet

        if (!isDeleted)
        {
            throw new KeyNotFoundException($"Projekt med ID {id} kunde inte tas bort, eller hittades inte.");
        }

        return isDeleted;
    }

    public async Task<IEnumerable<Project>> GetProjectsByStatus(string status)
    {
        var allProjects = await _projectRepository.GetAllAsync();
        return allProjects
            .Where(p => string.Equals(p.Status, status, StringComparison.OrdinalIgnoreCase))
            .Select(ProjectFactory.Create);
    }

}
