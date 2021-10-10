using Microsoft.EntityFrameworkCore;
using RealStateManager.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealStateManager.DAL.Repositories
{
    public class RepositoryGeneric<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class 
    {
        private readonly RealStateManagerContext _realStateManagerContext;

        public RepositoryGeneric(RealStateManagerContext _realStateManagerContext)
        {
            this._realStateManagerContext = _realStateManagerContext;
        }

        public async Task Delete(TEntity entity)
        {
            try
            {
                _realStateManagerContext.Set<TEntity>().Remove(entity);
                await _realStateManagerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var entity = await GetById(id);
                _realStateManagerContext.Set<TEntity>().Remove(entity);
                await _realStateManagerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(string id)
        {
            try
            {
                var entity = await GetById(id);
                _realStateManagerContext.Set<TEntity>().Remove(entity);
                await _realStateManagerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            try
            {
                return await _realStateManagerContext.Set<TEntity>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                return await _realStateManagerContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<TEntity> GetById(string id)
        {
            try
            {
                return await _realStateManagerContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(TEntity entity)
        {
            try
            {
                await _realStateManagerContext.AddAsync(entity);
                await _realStateManagerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(List<TEntity> entity)
        {
            try
            {
                await _realStateManagerContext.AddRangeAsync(entity);
                await _realStateManagerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Update(TEntity entity)
        {
            try
            {
                _realStateManagerContext.Set<TEntity>().Update(entity);
                await _realStateManagerContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
