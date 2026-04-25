using Microsoft.AspNetCore.Mvc;
using SIOMS.DTOs;
using SIOMS.Services;

namespace SIOMS.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var products = await _service.GetAllProducts();
        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        await _service.CreateProduct(dto);
        return Ok(dto);
    }
}