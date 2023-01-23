using Azure.Core;
using BelajarRestApi.Dtos;
using BelajarRestApi.Entities;
using BelajarRestApi.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BelajarRestApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IPersistence _persistence;
        public ProductService(IRepository<Product> productRepository, IPersistence persistence)
        {
            _productRepository = productRepository;
            _persistence = persistence;
        }

        public async Task<ProductResponse> CreateNewProduct(Product payload)
        {
            var product = await _productRepository.Find(product => product.ProductName.ToLower().Equals(payload.ProductName.ToLower()), new[] { "ProductPrices" });
            if (product is null)
            {
                var result = await _persistence.ExecuteTransactionAsync(async () =>
                {
                    var product = await _productRepository.Save(payload);
                    await _persistence.SaveChangesAsync();
                    return product;
                });
                var productPriceResponses = result.ProductPrices.Select(productPrice => new ProductPriceResponse
                {
                    Id = productPrice.ProductId.ToString(),
                    Price = productPrice.Price,
                    Stock = productPrice.Stock,
                    StoreId = productPrice.StoreId.ToString()
                }).ToList();

                ProductResponse response = new()
                {
                    Id = result.ProductId.ToString(),
                    ProductName = result.ProductName,
                    Description = result.Description,
                    ProductPrices = productPriceResponses
                };
                return response;
            }
            var productPriceReq = payload.ProductPrices.ToList();

            ProductPrice productPrice = new()
            {
                Price = productPriceReq[0].Price,
                Stock = productPriceReq[0].Stock,
                StoreId = productPriceReq[0].StoreId
            };
            product.ProductPrices.Add(productPrice);
            await _persistence.SaveChangesAsync();


            ProductResponse productResponse = new()
            {
                Id = product.ProductId.ToString(),
                ProductName = product.ProductName,
                Description = product.Description,
                ProductPrices = new List<ProductPriceResponse>
                {
                    new()
                    {
                        Id = productPrice.ProductId.ToString(),
                        Price = productPrice.Price,
                        Stock = productPrice.Stock,
                        StoreId = productPrice.StoreId.ToString()
                    }
                }
            };
            return productResponse;
        }

        public async Task DeleteById(string id)
        {
            var product = await _productRepository.FindById(Guid.Parse(id));
            if (product is null) throw new Exception("Product not Found");
            _productRepository.Delete(product);
            await _persistence.SaveChangesAsync();
        }

        public async Task<PageResponse<ProductResponse>> GetAll(string name, int page, int size)
        {
            var products = await _productRepository.FindAll((p) => EF.Functions.Like(p.ProductName, $"%{name}%"), page, size, new[] { "ProductPrices" });
            var productResponses = new List<ProductResponse>();
            var productPriceResponses = new List<ProductPriceResponse>();


            foreach (var product in products)
            {
                foreach (var productprice in product.ProductPrices)
                {
                    var productPriceResponse = new ProductPriceResponse()
                    {
                        Id = productprice.ProductPriceId.ToString(),
                        Price = productprice.Price,
                        Stock = productprice.Stock,
                        StoreId = productprice.StoreId.ToString()
                    };
                    productPriceResponses.Add(productPriceResponse);
                }
                var productResponse = new ProductResponse()
                {
                    Id = product.ProductId.ToString(),
                    ProductName = product.ProductName,
                    Description = product.Description,
                    ProductPrices = productPriceResponses
                };
                productResponses.Add(productResponse);
            }

            var totalPages = (int)Math.Ceiling(await _productRepository.Count() / (decimal)size);

            PageResponse<ProductResponse> pageResponse = new()
            {
                Content = productResponses,
                TotalPage = totalPages,
                TotalElement = productResponses.Count()
            };

            return pageResponse;
        }

        public async Task<PageResponse<ProductResponse>> GetAll(int page, int size)
        {
            var products = await _productRepository.FindAll((p) => true, page, size, new[] {"ProductPrices"});

            var productResponses = products.Select(p => new ProductResponse
            {
                Id = p.ProductId.ToString(),
                ProductName = p.ProductName,
                Description = p.Description,
                ProductPrices = p.ProductPrices.Select(pp => new ProductPriceResponse
                {
                    Id = pp.ProductPriceId.ToString(),
                    Price = pp.Price,
                    Stock = pp.Stock,
                    StoreId = pp.StoreId.ToString()
                }).ToList()
            }).ToList();

            var totalPage = (int)Math.Ceiling(await _productRepository.Count() / (decimal)size);
            PageResponse<ProductResponse> response = new()
            {
                Content = productResponses,
                TotalPage = totalPage,
                TotalElement = products.Count()
            };
            return response;
        }

        public async Task<ProductResponse> GetById(string id)
        {
            var product = _productRepository.Find(product => product.ProductId.Equals(Guid.Parse(id)), new[] { "ProductPrices" });
            if (product is null) throw new Exception("product not found");

            var productPriceResponse = product.Result.ProductPrices.Select(productPrice => new ProductPriceResponse
            {
                Id = productPrice.ProductId.ToString(),
                Price = productPrice.Price,
                Stock = productPrice.Stock,
                StoreId = productPrice.StoreId.ToString()
            }).ToList();

            ProductResponse response = new()
            {
                Id = product.Result.ProductId.ToString(),
                ProductName = product.Result.ProductName,
                Description = product.Result.Description,
                ProductPrices = productPriceResponse
            };
            return response;
        }

        public async Task<ProductResponse> Update(Product payload)
        {
            var product = _productRepository.Update(payload);
            var productPrice = product.ProductPrices.Select(pp => new ProductPriceResponse
            {
                Id = pp.ProductPriceId.ToString(),
                Price = pp.Price,
                Stock = pp.Stock,
                StoreId = pp.StoreId.ToString()
            }).ToList();


            ProductResponse productResponse = new()
            {
                Id = product.ProductId.ToString(),
                ProductName = product.ProductName,
                Description = product.Description,
                ProductPrices = productPrice
            };
            await _persistence.SaveChangesAsync();
            return productResponse;
        }
    }
}
