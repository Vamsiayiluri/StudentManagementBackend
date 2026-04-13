using System.ComponentModel.DataAnnotations;
namespace StudentManagement.DTOs
{

    public class StudentDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Range(1, 100, ErrorMessage = "Age must be between 1 and 150")]
        public int Age { get; set; }
    }
}
