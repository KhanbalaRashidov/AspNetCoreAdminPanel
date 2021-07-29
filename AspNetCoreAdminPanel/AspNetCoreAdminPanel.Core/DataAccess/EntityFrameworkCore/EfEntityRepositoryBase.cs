using AspNetCoreAdminPanel.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCoreAdminPanel.Core.DataAccess.EntityFrameworkCore
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity :class, IEntity,new()
        where TContext : DbContext, new()

    {
        public TEntity Add(TEntity entity)
        {
            using (var dbContext = new TContext())
            {
                var addedEntity = dbContext.Entry(entity);
                addedEntity.State = EntityState.Added;
                dbContext.SaveChanges();
                return entity;
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var dbContext = new TContext())
            {
                var addedEntity = dbContext.Entry(entity);
                addedEntity.State = EntityState.Added;
                await dbContext.SaveChangesAsync();
                return entity;
            }
        }

        public void Delete(TEntity entity)
        {
            using (var dbContext = new TContext())
            {
                var deletedEntity = dbContext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var dbContext = new TContext())
            {
                var deletedEntity = dbContext.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                await dbContext.SaveChangesAsync();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var dbContext = new TContext())
            {
                return dbContext.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var dbContext = new TContext())
            {
                return filter == null
                    ? dbContext.Set<TEntity>().ToList()
                    : dbContext.Set<TEntity>().Where(filter).ToList();
            }
        }

        public TEntity Update(TEntity entity)
        {
            using (var dbContext = new TContext())
            {
                var updatedEntity = dbContext.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                dbContext.SaveChanges();
                return entity;
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var dbContext = new TContext())
            {
                var updatedEntity = dbContext.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                await dbContext.SaveChangesAsync();
                return entity;
            }
        }
    }
}
