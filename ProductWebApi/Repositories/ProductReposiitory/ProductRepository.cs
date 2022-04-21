using Microsoft.EntityFrameworkCore;
using ProductWebApi.DBOperations;
using ProductWebApi.ProductOperations.SearchProduct;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Repositories
{


    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        private readonly ProductStoreDbContext _context;
        public ProductRepository(ProductStoreDbContext dbContext) :base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Product> Exist(string name)
        {
            // Name e ulaşabilmek için entityleri kalıtımla aktarmak gerekiyor
            return await _context.Products.SingleOrDefaultAsync(existProduct => existProduct.Name == name);
        }

        public async Task<IEnumerable<Product>> Search(SearchQuery obj)
        {
            IQueryable<Product> query = _context.Products;

            if (!string.IsNullOrEmpty(obj.Name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(obj.Name.ToLower()));
            }
            if (query.Count() == 0)
            {
                return Enumerable.Empty<Product>();
            }
            
            return await query.ToListAsync();


        }


    }
}
