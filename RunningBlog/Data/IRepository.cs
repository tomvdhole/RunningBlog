using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RunningBlog.Data
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> Get(T entity);
        Task<T> Get(int id);
        Task AddAsync(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}