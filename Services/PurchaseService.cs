using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BelajarRestApi.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IPersistence _persistence;
        public PurchaseService(IRepository<Purchase> purchaseRepository, IPersistence persistence)
        {
            _purchaseRepository = purchaseRepository;
            _persistence = persistence;
        }

        public async Task<PurchaseResponse> CreateNewPurchase(Purchase payload)
        {
            var insertValue = new Purchase()
            {
                CustomerId = payload.CustomerId,
                TransDate = DateTime.Now,
                PurchaseDetails = payload.PurchaseDetails.Select(pp => new PurchaseDetail()
                {
                    ProductPriceId = pp.ProductPriceId,
                    PurchaseId = pp.PurchaseId,
                    Qty = pp.Qty,
                }).ToList()
            };
            var result = await _persistence.ExecuteTransactionAsync(async () =>
            {
                var purchase = await _purchaseRepository.Save(insertValue);
                await _persistence.SaveChangesAsync();
                return purchase;
            });

            var purchaaseDetailResponse = result.PurchaseDetails.Select(pd => new PurchaseDetailResponse()
            {
                Id = pd.Id.ToString(),
                ProductPriceId = pd.ProductPriceId.ToString(),
                PurchaseId = result.Id.ToString(),
                Qty = pd.Qty
            }).ToList();

            var purchase = new PurchaseResponse()
            {
                Id = result.Id.ToString(),
                TransDate = result.TransDate.ToString(),
                CustomerId = result.CustomerId.ToString(),
                PurchaseDetails = purchaaseDetailResponse
            };
            return purchase;
        }

        public async Task<PageResponse<PurchaseResponse>> GetAllPurchaseList(int page, int size)
        {
            var listPurchase = await _purchaseRepository.FindAll(p => true, page, size, new[] { "PurchaseDetails" });

            var purchaseResponse = listPurchase.Select(p => new PurchaseResponse()
            {
                Id = p.Id.ToString(),
                TransDate = p.TransDate.ToString(),
                CustomerId = p.CustomerId.ToString(),
                PurchaseDetails = p.PurchaseDetails.Select(pp => new PurchaseDetailResponse()
                {
                    Id = pp.Id.ToString(),
                    Qty = pp.Qty,
                    ProductPriceId = pp.ProductPriceId.ToString(),
                    PurchaseId = pp.PurchaseId.ToString(),
                }).ToList()
            }).ToList();

            var totalPage = (int)Math.Ceiling(await _purchaseRepository.Count() / (decimal)page);
            PageResponse<PurchaseResponse> response = new()
            {
                Content = purchaseResponse,
                TotalPage = totalPage,
                TotalElement = listPurchase.Count()
            };
            return response;
        }
    }
}
