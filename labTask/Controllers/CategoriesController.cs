using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using labTask.Data;
using labTask.Models;
using labTask.DTOs;

namespace labTask.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ApplicationDbContext db;
    private readonly IMapper mapper;

    public CategoriesController(ApplicationDbContext context, IMapper mapper)
    {
        db = context;
        this.mapper = mapper;
    }

    [HttpGet("all")]
    public IActionResult GetAll()
    {
        var categories = db.Categories.ToList();
        var categoryDtos = mapper.Map<List<CategoryDto>>(categories);
        return Ok(categoryDtos);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var category = db.Categories
            .Include(c => c.Products)
            .ThenInclude(p => p.Category)
            .FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound(new { message = $"Category with id {id} not found" });
        }

        var categoryDto = mapper.Map<CategoryWithProductsDto>(category);
        return Ok(categoryDto);
    }

    [HttpPost("create")]
    public IActionResult Create(CreateCategoryDto dto)
    {
        var category = mapper.Map<Category>(dto);
        db.Categories.Add(category);
        db.SaveChanges();
        return Ok(mapper.Map<CategoryDto>(category));
    }

    [HttpPut("update/{id}")]
    public IActionResult Update(int id, UpdateCategoryDto dto)
    {
        var category = db.Categories.Find(id);
        
        if (category == null)
        {
            return NotFound(new { message = $"Category with id {id} not found" });
        }

        mapper.Map(dto, category);
        db.SaveChanges();
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public IActionResult Delete(int id)
    {
        var category = db.Categories
            .Include(c => c.Products)
            .FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound(new { message = $"Category with id {id} not found" });
        }

        if (category.Products.Any())
        {
            return BadRequest(new { message = "Cannot delete category with existing products" });
        }

        db.Categories.Remove(category);
        db.SaveChanges();
        return Ok();
    }
}
