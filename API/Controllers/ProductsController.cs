using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenarecRepository<Product> _productRepo;

        private readonly IGenarecRepository<ProductBrand> _prodeuctBrandRepo;

        IGenarecRepository<ProductType> _productTypeRepo;

        public ProductsController(IGenarecRepository<Product> productRepo, 
            IGenarecRepository<ProductBrand> prodeuctBrandRepo,
            IGenarecRepository<ProductType> productTypeRepo)
        {
            _productRepo = productRepo;
            _prodeuctBrandRepo = prodeuctBrandRepo;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var spec = new ProductWithTypeAndBrandSpecification();

            var products = await _productRepo.ListAsync(spec);
            
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            return await _productRepo.GertByIdAsync(id);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            return Ok(await _prodeuctBrandRepo.ListAllAysnc());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetTypes()
        {
            return Ok(await _productTypeRepo.ListAllAysnc());
        }
    }
}
