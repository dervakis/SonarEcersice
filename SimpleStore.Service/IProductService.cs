using SimpleStore.Model;
using SimpleStore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStore.Service
{
    public interface IProductService
    {
        Task<Product> CreateProduct(Product product);
        Task<List<Product>> GetProducts();
        Task DeleteProduct(int id);
        Task UpdateProduct(Product product);
        Task<Product> GetProductById(int id);
        Task<int> GetProductCount();
    }
}
