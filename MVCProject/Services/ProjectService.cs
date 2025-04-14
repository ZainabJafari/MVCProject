
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
        var projects = await _projectRepository.GetAllAsync(); // Hämta alla projekt från repository
        return projects.Select(p => ProjectFactory.Create(p)); // Skapa en Project DTO från varje ProjectEntity
    }

    // Hämtar projekt genom ID
    public async Task<Project> GetProjectByIdAsync(string id)
    {
        if (!Guid.TryParse(id, out var projectId))
        {
            throw new ArgumentException("Ogiltigt ID-format", nameof(id));
        }

        var project = await _projectRepository.GetAsync(x => x.Id == id); // Jämför med Guid
        if (project == null)
        {
            throw new KeyNotFoundException($"Projekt med ID {id} hittades inte.");
        }

        return ProjectFactory.Create(project); // Skapa DTO från ProjectEntity
    }

    // Skapar ett nytt projekt
    public async Task<Project> CreateProjectAsync(ProjectRegisteration projectRegisteration)
    {
        var projectEntity = ProjectFactory.Create(projectRegisteration); // Omvandla från DTO till entity
        await _projectRepository.CreateAsync(projectEntity); // Anropa repository för att skapa projekt
        Console.WriteLine("data skapades");

        return ProjectFactory.Create(projectEntity); // Skapa DTO från entity och returnera
    }

    // Uppdaterar ett projekt
    public async Task UpdateProjectAsync(string id, ProjectUpdateForm projectUpdateForm)
    {
        var projectEntity = ProjectFactory.Create(projectUpdateForm); // Omvandla från DTO till entity
        await _projectRepository.UpdateAsync(projectEntity); // Anropa repository för att uppdatera projekt
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

}
