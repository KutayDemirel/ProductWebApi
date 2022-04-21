using AutoMapper;
using ProductWebApi.Entities;
using ProductWebApi.Models;
using ProductWebApi.Models.CategoryModels;

namespace ProductWebApi.Common
{
    public class ProductMappingProfile :Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ProductDetailViewModel>().ForMember(dest => dest.Brand, opt => opt.MapFrom(src => (
                (BrandEnum)src.BrandId).ToString())).ForMember(dest => dest.Category, opt => opt.MapFrom(src => (
                (CategoryEnum)src.CategoryId).ToString()));
            CreateMap<Product, ProductsViewModel>().ForMember(dest => dest.Brand, opt => opt.MapFrom(src => (
                (BrandEnum)src.BrandId).ToString())).ForMember(dest => dest.Category, opt => opt.MapFrom(src => (
                (CategoryEnum)src.CategoryId).ToString()));
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
            CreateMap<Category, CategoryDetailViewModel>();
            CreateMap<Category, CategoriesViewModel>();
        }
    }
}
