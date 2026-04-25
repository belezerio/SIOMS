using SIOMS.Helpers;
using SIOMS.Models;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(ProductQueryParams query);
    Task<Product> GetByIdAsync(int id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}