using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebApi.DBOperations;
using ProductWebApi.Entities;
using ProductWebApi.Models;
using ProductWebApi.Models.CategoryModels;
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
    [Route("api/v1/Categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ProductStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public CategoryController(ICategoryRepository repo, ProductStoreDbContext context, IMapper mapper)
        {
            _repository = repo;
            _context = context;
            _mapper = mapper;
        }

        //GET: api/v1/Products/
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriesViewModel>>> GetAll()
        {
            var productList = await _repository.GetAll();
            List<CategoriesViewModel> vm = _mapper.Map<List<CategoriesViewModel>>(productList);
            return vm;

        }

        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<CategoriesViewModel>>> Search([FromQuery] SearchQuery search)
        {
            var result = await _repository.Search(search);
            if (!result.Any())
            {
                return NotFound();
            }
            List<CategoriesViewModel> vm = _mapper.Map<List<CategoriesViewModel>>(result);
            return vm;
        }

        // GET: api/v1/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDetailViewModel>> GetById([Range(1, int.MaxValue)] int id)
        {
            var product = await _repository.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            CategoryDetailViewModel result = _mapper.Map<CategoryDetailViewModel>(product);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([Range(1, int.MaxValue)] int id, [FromBody] UpdateCategoryDto updatedCategory)
        {

            if (ModelState.IsValid)
            {
                var category = await _repository.GetById(id);

                if (category == null)
                {
                    return NotFound();
                }

                try
                {
                    _mapper.Map(updatedCategory, category);
                    await _repository.Update(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(id))
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
        public async Task<ActionResult<CreateProductDto>> Insert([FromBody] CreateCategoryDto productDto)
        {

            if (ModelState.IsValid)
            {
                var product = await _repository.Exist(productDto.Name);

                if (product != null)
                {
                    return BadRequest();
                }

                product = _mapper.Map<Category>(productDto);
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

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }

    }
}
