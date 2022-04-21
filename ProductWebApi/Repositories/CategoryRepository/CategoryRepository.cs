using Microsoft.EntityFrameworkCore;
using ProductWebApi.DBOperations;
using ProductWebApi.Entities;
using ProductWebApi.ProductOperations.SearchProduct;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ProductStoreDbContext _context;
        public CategoryRepository(ProductStoreDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Category> Exist(string name)
        {
            // Name e ulaşabilmek için entityleri kalıtımla aktarmak gerekiyor
            return await _context.Categories.SingleOrDefaultAsync(existCategory => existCategory.Name == name);
        }

        public async Task<IEnumerable<Category>> Search(SearchQuery obj)
        {
            IQueryable<Category> query = _context.Categories;

            if (!string.IsNullOrEmpty(obj.Name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(obj.Name.ToLower()));
            }
            if (query.Count() == 0)
            {
                return Enumerable.Empty<Category>();
            }

            return await query.ToListAsync();


        }

    }
}
