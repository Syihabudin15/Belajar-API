using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BelajarRestApi.Services
{
    public class StoreService : IStoreService
    {
        private readonly IRepository<Store> _repository;
        private readonly IPersistence _persistence;
        public StoreService(IRepository<Store> repository, IPersistence persistence)
        {
            _repository = repository;
            _persistence = persistence;
        }

        public async Task<StoreResponse> CreateNewStore(Store payload)
        {
            var store = await _repository.Save(payload);
            if (store == null) throw new Exception("Data Store is invalid");
            await _persistence.SaveChangesAsync();

            StoreResponse response = new()
            {
                Id = store.StoreId.ToString(),
                Name = store.StoreName,
                Phone = store.PhoneNumber,
                Email = store.Email,
                Address = store.Address,
                NoSiup = store.NoSiup
            };
            return response;
        }

        public async Task DeleteById(string id)
        {
            var store = await _repository.FindById(Guid.Parse(id));
            if (store == null) throw new Exception("Store not found");
            _repository.Delete(store);
            await _persistence.SaveChangesAsync();
        }

        public async Task<PageResponse<StoreResponse>> GetAll(string name, int page, int size)
        {
            var listStore = await _repository.FindAll(c => EF.Functions.Like(c.StoreName, $"%{name}%"), page, size);

            var stores = listStore.Select(s => new StoreResponse
            {
                Id = s.StoreId.ToString(),
                Name = s.StoreName,
                Phone = s.PhoneNumber,
                Email = s.Email,
                Address = s.Address,
                NoSiup = s.NoSiup
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _repository.Count() / (decimal)size);
            PageResponse<StoreResponse> pageResponse = new()
            {
                Content = stores,
                TotalPage = totalPages,
                TotalElement = listStore.Count()
            };
            return pageResponse;
        }
        public async Task<PageResponse<StoreResponse>> GetAll(int page, int size)
        {
            var listStore = await _repository.FindAll(page, size);

            var stores = listStore.Select(s => new StoreResponse
            {
                Id = s.StoreId.ToString(),
                Name = s.StoreName,
                Phone = s.PhoneNumber,
                Email = s.Email,
                Address = s.Address,
                NoSiup = s.NoSiup
            }).ToList();

            var totalPages = (int)Math.Ceiling(await _repository.Count() / (decimal)size);
            PageResponse<StoreResponse> pageResponse = new()
            {
                Content = stores,
                TotalPage = totalPages,
                TotalElement = listStore.Count()
            };
            return pageResponse;
        }

        public async Task<StoreResponse> GetById(string id)
        {
            var store = await _repository.FindById(Guid.Parse(id));
            if (store is null) throw new Exception("Not Found");
            StoreResponse response = new()
            {
                Id = store.StoreId.ToString(),
                Name = store.StoreName,
                Phone = store.PhoneNumber,
                Email = store.Email,
                Address = store.Address,
                NoSiup = store.NoSiup
            };
            return response;
        }

        public async Task<StoreResponse> Update(Store payload)
        {
            var store = _repository.Update(payload);
            if (store == null) throw new Exception("Update failed coz Store id not Match");
            await _persistence.SaveChangesAsync();
            StoreResponse response = new()
            {
                Id = store.StoreId.ToString(),
                Name = store.StoreName,
                Phone = store.PhoneNumber,
                Email = store.Email,
                Address = store.Address,
                NoSiup = store.NoSiup
            };
            return response;
        }
    }
}
