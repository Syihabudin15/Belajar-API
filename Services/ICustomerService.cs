using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;

namespace BelajarRestApi.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponse> CreateNewCustomer(Customer payload);
        Task<CustomerResponse> GetById(string id);
        Task<PageResponse<CustomerResponse>> GetAll(string name, int page, int size);
        Task<PageResponse<CustomerResponse>> GetAll(int page, int size);
        Task<CustomerResponse> Update(Customer payload);
        Task DeleteById(string id);
    }
}
