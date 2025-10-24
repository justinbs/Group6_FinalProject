using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface ICrudService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}
