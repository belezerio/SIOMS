using AutoMapper;
using SIOMS.DTOs;
using SIOMS.Models;
using SIOMS.Repositories;

namespace SIOMS.Services;

public class ProductService
{
    private readonly IProductRepository _repo;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllProducts()
    {
        var products = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto> GetProductById(int id)
    {
        var product = await _repo.GetByIdAsync(id);
        return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task CreateProduct(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _repo.AddAsync(product);
    }

    public async Task UpdateProduct(int id, CreateProductDto dto)
    {
        var existing = await _repo.GetByIdAsync(id);

        if (existing == null)
            throw new Exception("Product not found");

        _mapper.Map(dto, existing);
        await _repo.UpdateAsync(existing);
    }

    public async Task DeleteProduct(int id)
    {
        await _repo.DeleteAsync(id);
    }
}