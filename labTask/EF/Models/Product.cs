using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace labTask.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    public int CId { get; set; }

    [ForeignKey("CId")]
    public Category? Category { get; set; }
}
