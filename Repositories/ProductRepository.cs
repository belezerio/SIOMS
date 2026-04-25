
using SIOMS.Models;
using SIOMS.Data;
using Microsoft.EntityFrameworkCore;
using SIOMS.Helpers;
namespace SIOMS.Repositories;
class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Product>> GetAllAsync(ProductQueryParams query)
    {
        var products = _context.Products.AsQueryable();

        if (query.MinPrice.HasValue)
        {
            products = products.Where(p => p.Price >= query.MinPrice.Value);
        }
        if (query.MaxPrice.HasValue)
        {
            products = products.Where(p=> p.Price <= query.MaxPrice.Value);
        }

         if (!string.IsNullOrEmpty(query.SortBy))
        {
            if (query.SortBy.ToLower() == "price")
            {
                products = query.Order == "desc"
                     ? products.OrderByDescending(p => p.Price)
                     : products.OrderBy(p => p.Price);
            }
            
        }
        products = products.Skip((query.Page - 1)* query.PageSize).Take(query.PageSize);
        
        return await products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if(product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}