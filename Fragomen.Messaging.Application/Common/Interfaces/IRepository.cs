using Frogomen.Messaging.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fragomen.Messaging.Application.Common.Interfaces
{
    public interface IRepository<T> 
    {
        public Task<bool> CreateAsync(T item);
        public Task<bool> DeleteAsync(Guid id);
        public Task<bool> UpdateAsync(T item);
        public Task<IEnumerable<T?>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid id);
    }
}
