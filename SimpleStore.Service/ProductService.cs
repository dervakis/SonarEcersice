using SimpleStore.Model;
using SimpleStore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStore.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<Product> CreateProduct(Product product) 
        {
            if (product.Quantity < 0)
                throw new InvalidDataException("Product Quantity Must me > 0");
            
            return await productRepository.AddProductAsync(product);
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await productRepository.GetAllProductAsync();
            if(products == null)
                return new List<Product>();
            return products;
        }
        public async Task DeleteProduct(int id)
        {
           await productRepository.DeleteProductAsync(id);
        }

        public async Task UpdateProduct(Product product)
        {
            await productRepository.UpdateProductAsync(product);
        }
        public async Task<Product> GetProductById(int id)
        {
           return await productRepository.GetProductAsync(id);
        }
        public async Task<int> GetProductCount()
        {
            return await productRepository.ProductCount();
        }

    }
}
