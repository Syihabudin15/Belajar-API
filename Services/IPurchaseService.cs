using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;

namespace BelajarRestApi.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseResponse> CreateNewPurchase(Purchase payload);
        Task<PageResponse<PurchaseResponse>> GetAllPurchaseList(int page, int size);
        Task<PageResponse> GetAllWithDetail();
    }
}
