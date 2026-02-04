using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SimpleStore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStore.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _con;

        public ProductRepository(AppDbContext con)
        {
            _con = con;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
           await _con.Products.AddAsync(product);
            return product;
        }

        public async Task DeleteProductAsync(int id)
        {
            Product product = await _con.Products.FindAsync(id);
            if(product is null)
            {
                throw new KeyNotFoundException($"Product with Id = {id} not found");
            }
            _con.Products.Remove(product);
            await _con.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductAsync()
        {

            return await _con.Products.ToListAsync();
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _con.Products.FindAsync(id);
        }

        public async Task UpdateProductAsync(Product product)
        {
            var oldProduct = await _con.Products.FirstOrDefaultAsync(p=>p.ProductId== product.ProductId);
            oldProduct.ProductName = product.ProductName;
            oldProduct.Quantity = product.Quantity;
            oldProduct.Category = product.Category;
             _con.SaveChangesAsync();
        }
        public async Task<int> ProductCount()
        {
            int count = 0;
            using(SqlConnection con = new SqlConnection("Data Source=localhost;Initial Catalog=ProductDB;Integrated Security=True;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;"))
            {
                using (SqlCommand cmd = new SqlCommand("select count(*) from Products", con)) 
                {
                    con.Open();
                    count = (int)cmd.ExecuteScalar();
                }
            }
            return count;
        }
    }
}
