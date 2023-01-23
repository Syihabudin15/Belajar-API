using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BelajarRestApi.Controllers
{
    [ApiController]
    [Route("purchases")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewPurchase(Purchase payload)
        {
            var purchase = await _purchaseService.CreateNewPurchase(payload);
            CommonResponse<PurchaseResponse> response = new()
            {
                StatusCode = (int)HttpStatusCode.Created,
                Message = "Created",
                Data = purchase
            };
            return Created("purchases", response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchase([FromQuery] int page, [FromQuery] int size)
        {
            var listPurchase = await _purchaseService.GetAllPurchaseList(page, size);
            CommonResponse<PageResponse<PurchaseResponse>> response = new()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Ok",
                Data = listPurchase
            };
            return Ok(response);
        }
    }
}
