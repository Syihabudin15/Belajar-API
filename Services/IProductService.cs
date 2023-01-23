using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;

namespace BelajarRestApi.Services
{
    public interface IProductService
    {
        Task<ProductResponse> CreateNewProduct(Product payload);
        Task<ProductResponse> GetById(string id);
        Task<PageResponse<ProductResponse>> GetAll(string name, int page, int size);
        Task<PageResponse<ProductResponse>> GetAll(int page, int size);
        Task<ProductResponse> Update(Product payload);
        Task DeleteById(string id);
    }
}
