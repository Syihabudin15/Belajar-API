using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Repositories;
using BelajarRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.AccessControl;

namespace BelajarRestApi.Controllers
{
    [ApiController]
    [Route("customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewCustomer(Customer payload)
        {
            var customer = await _customerService.CreateNewCustomer(payload);
            CommonResponse<CustomerResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Created",
                Data = customer
            };
            return Created("/customers",response);
        }

        [HttpGet("name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var listCustomer = await _customerService.GetAll(name.ToLower(), page, size);
            CommonResponse<PageResponse<CustomerResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = listCustomer
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomer([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var listCustomer = await _customerService.GetAll(page, size);
            CommonResponse<PageResponse<CustomerResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = listCustomer
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(string id)
        {
            var customer = await _customerService.GetById(id);
            CommonResponse<CustomerResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = customer
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            await _customerService.DeleteById(id);
            CommonResponse<CustomerResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = null
            };
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(Customer payload)
        {
            var customer = await _customerService.Update(payload);
            CommonResponse<CustomerResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = customer
            };
            return Ok(response);
        }
    }
}
