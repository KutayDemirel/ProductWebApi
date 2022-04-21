using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.DBOperations;
using ProductWebApi.Entities;
using ProductWebApi.ProductOperations.SearchProduct;
using ProductWebApi.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ProductStoreDbContext _context;
        //private DbSet<T> Table;

        public GenericRepository(ProductStoreDbContext context)
        {
            this._context = context;
            //this.Table = _context.Set<T>();
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task Insert(T obj)
        {
            _context.Set<T>().Add(obj);
            await _context.SaveChangesAsync();

        }

        //public async Task<IEnumerable<T>> Search(SearchQuery obj)
        //{
        //    IQueryable tr = _context.Set<T>().Where(e => e.Name.ToLower().Contains(obj.Name.ToLower()));
        //    return await _context.Set<T>().ToListAsync();
        //}

        public async Task Update(T obj)
        {
            _context.Set<T>().Update(obj);
            await _context.SaveChangesAsync();
            
        }
        public async Task Delete(T obj)
        {
            _context.Set<T>().Remove(obj);
            await _context.SaveChangesAsync();
        }

        //public async Task<Product> Exist(string name)
        //{
        //    // Name e ulaşabilmek için entityleri kalıtımla aktarmak gerekiyor
        //    return await _context.Products.SingleOrDefaultAsync(existProduct => existProduct.Name == name);
        //}


    }
}
