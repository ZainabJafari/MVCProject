namespace MVCProject.Factories;
using MVCProject.Models;
using MVCProject.Dtos;
using MVCProject.Data.Entities;


public class ProjectFactory
{
    public static ProjectEntity Create() => new();

    public static ProjectEntity Create(ProjectRegisteration projectRegisteration) => new()
    {
        Id = Guid.NewGuid().ToString(),  // Se till att ett unikt ID skapas
        ProjectName = projectRegisteration.ProjectName,
        ClientName = projectRegisteration.ClientName,
        Description = projectRegisteration.Description,
        Budget = projectRegisteration.Budget,
        StartDate = projectRegisteration.StartDate,
        EndDate = projectRegisteration.EndDate,
        Status = projectRegisteration.Status,
        ImageUrl = projectRegisteration.ImageUrl
    };

    public static ProjectEntity Create(ProjectUpdateForm projectUpdateForm)
    {
        return new ProjectEntity
        {
            Id = projectUpdateForm.Id,// Nytt GUID för nya projekt
            ProjectName = projectUpdateForm.ProjectName,
            ClientName = projectUpdateForm.ClientName,
            Description = projectUpdateForm.Description,
            Budget = projectUpdateForm.Budget,
            StartDate = projectUpdateForm.StartDate,
            EndDate = projectUpdateForm.EndDate,
            Status = projectUpdateForm.Status,
            ImageUrl = projectUpdateForm.ImageUrl
        };
    }

    public static Project Create(ProjectEntity projectEntity)
    {
        return new Project
        {
            Id = projectEntity.Id,
            ProjectName = projectEntity.ProjectName,
            ClientName = projectEntity.ClientName,
            Description = projectEntity.Description,
            Budget = projectEntity.Budget,
            StartDate = projectEntity.StartDate,
            EndDate = projectEntity.EndDate,
            Status = projectEntity.Status,
            ImageUrl = projectEntity.ImageUrl
        };
    }
}
