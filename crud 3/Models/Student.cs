using System.ComponentModel.DataAnnotations;

namespace crud_3.Models
{
    public class Student
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        [Required]
        public int age { get; set; }
    }
}
