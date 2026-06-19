using AutoMapper;
using Asp.Versioning;
using ProductService.API.Caching;
using ProductService.API.DTOs;
using ProductService.API.Interfaces;
using ProductService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProductService.API.Controllers.V1;


[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(
        IProductService service,
        IMapper mapper,
        ICacheService cacheService,
        ILogger<ProductsController> logger)
    {
        _service = service;
        _mapper = mapper;
        _cacheService = cacheService;
        _logger = logger;
    }

    // GET: api/products
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
    {
        const string cacheKey = "products_all";

        var cachedProducts =
            await _cacheService.GetData<List<ProductDto>>(cacheKey);

        if (cachedProducts != null)
        {
            _logger.LogInformation(
                "Products fetched from Redis Cache");

            return Ok(cachedProducts);
        }

        _logger.LogInformation(
            "Products fetched from Database");

        var products =
            await _service.GetAllProducts();

        var productDtos =
            _mapper.Map<List<ProductDto>>(products);

        await _cacheService.SetData(
            cacheKey,
            productDtos,
            TimeSpan.FromMinutes(5));

        return Ok(productDtos);
    }

    // GET: api/products/1
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        string cacheKey = $"product_{id}";

        var cachedProduct =
            await _cacheService.GetData<ProductDto>(cacheKey);

        if (cachedProduct != null)
        {
            _logger.LogInformation(
                $"Product {id} fetched from Redis Cache");

            return Ok(cachedProduct);
        }

        _logger.LogInformation(
            $"Product {id} fetched from Database");

        var product =
            await _service.GetProductById(id);

        if (product == null)
        {
            return NotFound(new
            {
                Message = $"Product with ID {id} not found."
            });
        }

        var productDto =
            _mapper.Map<ProductDto>(product);

        await _cacheService.SetData(
            cacheKey,
            productDto,
            TimeSpan.FromMinutes(5));

        return Ok(productDto);
    }

    // POST: api/products
    
    [HttpPost]
    public async Task<ActionResult<ProductDto>>
        CreateProduct(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        var createdProduct =
            await _service.CreateProduct(product);

        var response =
            _mapper.Map<ProductDto>(createdProduct);

        await _cacheService.RemoveData("products_all");

        _logger.LogInformation(
            "Products cache invalidated after Create");

        return CreatedAtAction(
            nameof(GetProduct),
            new { id = response.ProductId },
            response
        );
    }

    // PUT: api/products/1
    
    [HttpPut("{id}")]
    public async Task<IActionResult>
        UpdateProduct(int id, UpdateProductDto dto)
    {
        if (id != dto.ProductId)
        {
            return BadRequest(new
            {
                Message = "Product ID mismatch."
            });
        }

        var existingProduct =
            await _service.GetProductById(id);

        if (existingProduct == null)
        {
            return NotFound(new
            {
                Message = $"Product with ID {id} not found."
            });
        }

        var product =
            _mapper.Map<Product>(dto);

        await _service.UpdateProduct(product);

        await _cacheService.RemoveData("products_all");
        await _cacheService.RemoveData($"product_{id}");

        _logger.LogInformation(
            $"Cache invalidated for Product {id}");

        return Ok(new
        {
            Message = "Product updated successfully."
        });
    }

    // DELETE: api/products/1
    
    [HttpDelete("{id}")]
    public async Task<IActionResult>
        DeleteProduct(int id)
    {
        var existingProduct =
            await _service.GetProductById(id);

        if (existingProduct == null)
        {
            return NotFound(new
            {
                Message = $"Product with ID {id} not found."
            });
        }

        await _service.DeleteProduct(id);

        await _cacheService.RemoveData("products_all");
        await _cacheService.RemoveData($"product_{id}");

        _logger.LogInformation(
            $"Cache invalidated after deleting Product {id}");

        return Ok(new
        {
            Message = "Product deleted successfully."
        });
    }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        return Ok("Products Controller Working");
    }
}