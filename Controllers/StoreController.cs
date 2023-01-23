using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Repositories;
using BelajarRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Net;



namespace BelajarRestApi.Controllers
{
    [ApiController]
    [Route("stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewStore([FromBody] Store payload)
        {
            var store = await _storeService.CreateNewStore(payload);
            CommonResponse<StoreResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Created",
                Data = store
            };
            return Created("/stores", response);
        }

        [HttpGet("name")]
        public async Task<IActionResult> SearchByName([FromQuery] string name, [FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var listStore = await _storeService.GetAll(name, page, size);
            CommonResponse<PageResponse<StoreResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = listStore
            };
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStore([FromQuery] int page = 1, [FromQuery] int size = 5)
        {
            var listStore = await _storeService.GetAll(page, size);
            CommonResponse<PageResponse<StoreResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = listStore
            };
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(string id)
        {
            var store = await _storeService.GetById(id);
            CommonResponse<StoreResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = store
            };
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStore([FromBody] Store payload)
        {
            var store = await _storeService.Update(payload);
            CommonResponse<StoreResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = store
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(string id)
        {
            await _storeService.DeleteById(id);
            CommonResponse<StoreResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = null
            };
            return Ok(response);
        }


    }
}
