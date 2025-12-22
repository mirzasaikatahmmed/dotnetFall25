using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UMS.EF.Tables
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Column(TypeName = "VARCHAR")]
        public required string Name { get; set; }
        [Column(TypeName = "VARCHAR")]
        public required string Email { get; set; }
    }
}