using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Repositories;
using BelajarRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using System.Net;

namespace BelajarRestApi.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewProduct([FromBody] Product request)
        {
            var product = await _productService.CreateNewProduct(request);
            CommonResponse<ProductResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Successfully Created product",
                Data = product
            };

            return Created("/products", response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productService.GetById(id);
            CommonResponse<ProductResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Successfully Get Product",
                Data = product
            };
            return Ok(response);
        }

        [HttpGet("name")]
        public async Task<IActionResult> SearchByName(string name, int page, int size)
        {
            var products = await _productService.GetAll(name, page, size);
            CommonResponse<PageResponse<ProductResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Successfully Get All Product",
                Data = products
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProduct([FromQuery] int page,[FromQuery] int size)
        {
            var products = await _productService.GetAll(page, size);
            CommonResponse<PageResponse<ProductResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Successfully Get All Product",
                Data = products
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _productService.DeleteById(id);
            CommonResponse<string?> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Successfully Delete",
                Data = null
            };
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product payload)
        {
            var product = await _productService.Update(payload);
            CommonResponse<ProductResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = product
            };
            return Ok(response);
        }

    }
}
