using ProductWebApi.ProductOperations.SearchProduct;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductWebApi.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> Exist(string name);
        Task<IEnumerable<Product>> Search(SearchQuery obj);
    }
}
