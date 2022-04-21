using ProductWebApi.Entities;
using ProductWebApi.ProductOperations.SearchProduct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebApi.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> Exist(string name);

        Task<IEnumerable<Category>> Search(SearchQuery obj);
    }
}
