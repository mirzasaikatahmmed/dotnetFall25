using System.ComponentModel.DataAnnotations;

namespace StudentManagement.DAL
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string StudentName { get; set; }
        
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string StudentEmail { get; set; }
    }
}
