using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;

namespace BelajarRestApi.Services
{
    public interface IStoreService
    {
        Task<StoreResponse> CreateNewStore(Store payload);
        Task<StoreResponse> GetById(string id);
        Task<PageResponse<StoreResponse>> GetAll(string name, int page, int size);
        Task<PageResponse<StoreResponse>> GetAll(int page, int size);
        Task<StoreResponse> Update(Store payload);
        Task DeleteById(string id);
    }
}
