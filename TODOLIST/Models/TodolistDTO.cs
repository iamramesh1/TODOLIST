using System.ComponentModel.DataAnnotations;

namespace TODOLIST.Models
{
    public class TodolistDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The Title field is required.")]
        [StringLength(100, ErrorMessage = "The Title cannot be longer than 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "The Title can only contain letters, numbers, and spaces.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "The Description cannot be longer than 500 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "The Description can only contain letters, numbers, and spaces.")]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }
    }
}
