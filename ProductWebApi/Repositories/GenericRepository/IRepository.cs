using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebApi.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        //Task<IEnumerable<T>> Search();
        Task<T> GetById(int id);
        Task Update(T obj);
        Task Insert(T obj);
        Task Delete(T obj);
        //Task<T> Exist(string name);
        //void Save();
    }
}
