using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.Common;
using ProductWebApi.DBOperations;
using ProductWebApi.Models;
using ProductWebApi.ProductOperations.SearchProduct;
using ProductWebApi.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class ProductController : ControllerBase
    {
        private readonly ProductStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository repo,ProductStoreDbContext context, IMapper mapper)
        {
            _repository = repo;
            _context = context;
            _mapper = mapper;
        }

        //GET: api/v1/Products/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductsViewModel>>> GetAll()
        {
            var productList = await _repository.GetAll();
            List<ProductsViewModel> vm = _mapper.Map<List<ProductsViewModel>>(productList); 
            return vm;

        }
        //GET: api/v1/Products/Search
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<ProductsViewModel>>> Search([FromQuery] SearchQuery search)
        {
            var result = await _repository.Search(search);
            if(!result.Any())
            {
                return NotFound();
            }
            List<ProductsViewModel> vm = _mapper.Map<List<ProductsViewModel>>(result);
            return vm;
        }

        // GET: api/v1/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailViewModel>> GetById([Range(1, int.MaxValue)] int id)
        {
            var product = await _repository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            ProductDetailViewModel result = _mapper.Map<ProductDetailViewModel>(product); 
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Range(1, int.MaxValue)] int id, [FromBody] UpdateProductDto updatedProduct)
        {

            if (ModelState.IsValid)
            {
                var product = await _repository.GetById(id);

                if (product == null)
                {
                    return NotFound();
                }

                try
                {
                    _mapper.Map(updatedProduct, product);
                    await _repository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok();
            }
            return BadRequest();
        }

        // POST: api/v1/Products
        [HttpPost]
        public async Task<ActionResult<CreateProductDto>> Insert([FromBody]CreateProductDto productDto)
        {
            
            if (ModelState.IsValid)
            {
                var product = await _repository.Exist(productDto.Name);
                
                if (product != null)
                {
                    return BadRequest();
                }

                product = _mapper.Map<Product>(productDto); 
                await _repository.Insert(product);
                return CreatedAtAction("Insert", product);
            }
            return BadRequest();
        }

        // DELETE: api/v1/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([Range(1, int.MaxValue)] int id)
        {
            var product = await _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            await _repository.Delete(product);
            return Ok();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}
