using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eventStore
{
    public interface IRepository<T>
    {
        public Task<T> GetById(string id);
        public Task<T> Update(string id, T updatedData);

        public Task<T> DeleteById(string id);

        public Task<List<T>> Get();

        public Task<T> Create(T item); 
    }
}
