namespace Business.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Business.Dtos;
    using Business.Factories;
    using Data.Entities;
    using MVCProject.Data.Interfaces;
    using MVCProject.Models;

    public class ProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        // Hämtar alla projekt
        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync(); // Hämta alla projekt från repository
            return projects.Select(p => ProjectFactory.Create(p)); // Skapa en Project DTO från varje ProjectEntity
        }

        // Hämtar projekt genom ID
        public async Task<Project> GetProjectByIdAsync(string id)
        {
            var project = await _projectRepository.GetAsync(x => x.Id == id); // Hämta ett projekt via ID
            return ProjectFactory.Create(project); // Skapa DTO från ProjectEntity
        }

        // Skapar ett nytt projekt
        public async Task<Project> CreateProjectAsync(ProjectRegisteration projectRegisteration)
        {
            var projectEntity = ProjectFactory.Create(projectRegisteration); // Omvandla från DTO till entity
            await _projectRepository.CreateAsync(projectEntity); // Anropa repository för att skapa projekt
            return ProjectFactory.Create(projectEntity); // Skapa DTO från entity och returnera
        }

        // Uppdaterar ett projekt
        public async Task UpdateProjectAsync(string id, ProjectUpdateForm projectUpdateForm)
        {
            var projectEntity = ProjectFactory.Create(projectUpdateForm); // Omvandla från DTO till entity
            await _projectRepository.UpdateAsync(projectEntity); // Anropa repository för att uppdatera projekt
        }
    }
}
