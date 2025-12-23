using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using labTask.Data;
using labTask.Models;
using labTask.DTOs;

namespace labTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;

    public ProductsController(ApplicationDbContext context, IMapper mapper)
    {
        db = context;
        this.mapper = mapper;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var products = db.Products.Include(p => p.Category).ToList();
        var productDtos = mapper.Map<List<ProductDto>>(products);
        return Ok(productDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var product = db.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == id);

        if (product == null)
        {
            return NotFound(new { message = $"Product with id {id} not found" });
        }

        var productDto = mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpGet("bycategory/{categoryId}")]
    public IActionResult GetByCategory(int categoryId)
    {
        var category = db.Categories.Find(categoryId);
        if (category == null)
        {
            return NotFound(new { message = $"Category with id {categoryId} not found" });
        }

        var products = db.Products
            .Include(p => p.Category)
            .Where(p => p.CId == categoryId)
            .ToList();

        var productDtos = mapper.Map<List<ProductDto>>(products);
        return Ok(productDtos);
    }

    [HttpPost("create")]
    public IActionResult Create(CreateProductDto dto)
    {
        var category = db.Categories.Find(dto.CId);
        if (category == null)
        {
            return BadRequest(new { message = $"Category with id {dto.CId} not found" });
        }

        var product = mapper.Map<Product>(dto);
        db.Products.Add(product);
        db.SaveChanges();
        
        product.Category = category;
        var productDto = mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpPut("update/{id}")]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        var product = db.Products.Find(id);

        if (product == null)
        {
            return NotFound(new { message = $"Product with id {id} not found" });
        }

        var category = db.Categories.Find(dto.CId);
        if (category == null)
        {
            return BadRequest(new { message = $"Category with id {dto.CId} not found" });
        }

        mapper.Map(dto, product);
        db.SaveChanges();
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var product = db.Products.Find(id);

        if (product == null)
        {
            return NotFound(new { message = $"Product with id {id} not found" });
        }

        db.Products.Remove(product);
        db.SaveChanges();
        return Ok();
    }
}
