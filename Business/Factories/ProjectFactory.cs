namespace Business.Factories;
using Business.Dtos;
using Data.Entities;
using MVCProject.Models;

public class ProjectFactory
{
    public static ProjectEntity Create() => new ();

    public static ProjectEntity Create(ProjectRegisteration projectRegisteration) => new() 
        {
        Id = Guid.NewGuid().ToString(),
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
