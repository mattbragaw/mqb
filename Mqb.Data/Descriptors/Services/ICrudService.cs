using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mqb.Descriptors.Services
{
    public interface ICrudService<T>
    {
        Task<bool> CreateAsync(T model);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IPaged<T>> GetPagedAllAsync(int pageIndex, int pageSize);
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetIfAsync(Predicate<T> predicate);
        Task<IPaged<T>> GetPagedIfAsync(Predicate<T> predicate, int pageIndex, int pageSize);
        Task<bool> UpdateAsync(T model);
        Task<bool> DeleteAllAsync();
        Task<bool> DeleteByIdAsync(string id);
        Task<bool> DeleteIfAsync(Predicate<T> predicate);
    }
}
