namespace labTask.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CId { get; set; }
    public string? CategoryName { get; set; }
}

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CId { get; set; }
}

public class UpdateProductDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int CId { get; set; }
}
