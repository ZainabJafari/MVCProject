using System.Diagnostics;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MVCProject.Data.Entities;
using MVCProject.Data.Interfaces;

namespace MVCProject.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ProjectEntity> CreateAsync(ProjectEntity entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            try
            {
                await _context.Projects.AddAsync(entity);
                Debug.WriteLine("Entity added to DbContext.");
                await _context.SaveChangesAsync();
                Debug.WriteLine("Changes saved to the database.");
                return entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error creating project entity: " + ex.Message);
                throw; 
            }
        }



        public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<ProjectEntity> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
        {
            if (expression == null)
                return null!;

            return await _context.Projects.FirstOrDefaultAsync(expression) ?? null!;
        }

        public async Task<ProjectEntity> UpdateAsync(ProjectEntity updateEntity)
        {
            if (updateEntity == null)
                return null!;

            try
            {
                var existingEntity = await GetAsync(x => x.Id == updateEntity.Id);
                if (existingEntity == null)
                    return null!;

                updateEntity.Id = existingEntity.Id;
                _context.Entry(existingEntity).CurrentValues.SetValues(updateEntity);
                await _context.SaveChangesAsync();
                return existingEntity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error updating project entity: " + ex.Message);
                return null!;
            }
        }

        public async Task<bool> DeleteAsync(Expression<Func<ProjectEntity, bool>> expression)
        {
            if (expression == null)
                return false;

            try
            {
                var entity = await GetAsync(expression);
                if (entity == null)
                    return false;

                _context.Projects.Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting project entity: " + ex.Message);
                return false;
            }
        }

    }
}
