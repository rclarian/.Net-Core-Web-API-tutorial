using System.ComponentModel.DataAnnotations;

namespace Web_API_Demo.DTO
{
    public class ToDoItemDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="{0} is required field")]
        [StringLength(100, ErrorMessage = "{0} cannot be longer than 100 characters")]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
