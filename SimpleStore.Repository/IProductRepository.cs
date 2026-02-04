using SimpleStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStore.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductAsync();
        Task<Product> GetProductAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task UpdateProductAsync(Product product);
        Task<int> ProductCount();

    }
}
