using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web_API_Demo.Models;

namespace Web_API_Demo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>
        {
            new ToDoItem { Id = 1, Title = "Task 1", Completed = false},
            new ToDoItem { Id = 2, Title = "Task 2", Completed = true},
            new ToDoItem { Id = 3, Title = "Task 3", Completed = false}
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            if(_toDoItems.Count() == 0)
            {
                return NotFound();
            }
            return Ok(_toDoItems);
        }
    }
}
