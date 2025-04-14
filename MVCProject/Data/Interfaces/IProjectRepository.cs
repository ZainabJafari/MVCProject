using System.Linq.Expressions;
using MVCProject.Data.Entities;

namespace MVCProject.Data.Interfaces
{
    public interface IProjectRepository
    {
        Task<ProjectEntity> CreateAsync(ProjectEntity entity);
        Task<bool> DeleteAsync(Expression<Func<ProjectEntity, bool>> expression);
        Task<IEnumerable<ProjectEntity>> GetAllAsync();
        Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression);
        Task<ProjectEntity> UpdateAsync(ProjectEntity updateEntity);
    }
}