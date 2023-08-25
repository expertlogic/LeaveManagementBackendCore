using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Contracts
{
    public interface IRepositoryBase<T> where T : class
    {
        ICollection<T> FindAll();
       ICollection<T> FindAllUsingStoreProc();
        T FindById(int id);
        bool isExists(int id);
        bool Create(T entity);
        bool CreateByProc(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Save();
    }
}
