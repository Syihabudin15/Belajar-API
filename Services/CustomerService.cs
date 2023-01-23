using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BelajarRestApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IPersistence _persistence;
        public CustomerService(IRepository<Customer> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public async Task<CustomerResponse> CreateNewCustomer(Customer payload)
        {
            var customer = await _repository.Save(payload);
            if (customer == null) throw new Exception("Data Customer is invalid");
            await _persistence.SaveChangesAsync();
            CustomerResponse response = new()
            {
                Id = customer.CustomerId.ToString(),
                Name = customer.CustomerName,
                Phone = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
            return response;
        }

        public async Task DeleteById(string id)
        {
            var customer = await _repository.Find(c => c.CustomerId.Equals(Guid.Parse(id)));
            if (customer == null) throw new Exception("Customer not found");
            _repository.Delete(customer);
            await _persistence.SaveChangesAsync();
        }

        public async Task<PageResponse<CustomerResponse>> GetAll(string name, int page, int size)
        {
            var listCustomer = await _repository.FindAll((c) => EF.Functions.Like(c.CustomerName, $"%{name}%"), page, size);

            var customers = listCustomer.Select(c => new CustomerResponse
            {
                Id = c.CustomerId.ToString(),
                Name = c.CustomerName,
                Phone = c.PhoneNumber,
                Email = c.Email,
                Address = c.Address
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _repository.Count() / (decimal)size);
            PageResponse<CustomerResponse> pageResponse = new()
            {
                Content = customers,
                TotalPage = totalPages,
                TotalElement = listCustomer.Count()
            };
            return pageResponse;
        }

        public async Task<PageResponse<CustomerResponse>> GetAll(int page, int size)
        {
            var listCustomer = await _repository.FindAll(page, size);

            var customers = listCustomer.Select(c => new CustomerResponse
            {
                Id = c.CustomerId.ToString(),
                Name = c.CustomerName,
                Phone = c.PhoneNumber,
                Email = c.Email,
                Address = c.Address
            }).ToList();
            

            var totalPages = (int)Math.Ceiling(await _repository.Count() / (decimal)size);
            PageResponse<CustomerResponse> pageResponse = new()
            {
                Content = customers,
                TotalPage = totalPages,
                TotalElement = listCustomer.Count()
            };
            return pageResponse;
        }

        public async Task<CustomerResponse> GetById(string id)
        {
            var customer = await _repository.FindById(Guid.Parse(id));
            if (customer is null) throw new Exception("Not Found");

            CustomerResponse response = new()
            {
                Id = customer.CustomerId.ToString(),
                Name = customer.CustomerName,
                Phone = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
            return response;
        }

        public async Task<CustomerResponse> Update(Customer payload)
        {
            var customer = _repository.Update(payload);
            if (customer == null) throw new Exception("Update failed coz Customer id not Found");
            await _persistence.SaveChangesAsync();

            CustomerResponse response = new()
            {
                Id = customer.CustomerId.ToString(),
                Name = customer.CustomerName,
                Phone = customer.PhoneNumber,
                Email = customer.Email,
                Address = customer.Address
            };
            return response;
        }
    }
}
