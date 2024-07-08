using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web_API_Demo.Models;
using Web_API_Demo.DTO;

namespace Web_API_Demo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private static List<ToDoItem> _toDoItems = new List<ToDoItem>
        {
            new ToDoItem { Id = 1, Title = "Task 1", IsCompleted = false},
            new ToDoItem { Id = 2, Title = "Task 2", IsCompleted = true},
            new ToDoItem { Id = 3, Title = "Task 3", IsCompleted = false}
        };

        [HttpGet]
        public IActionResult GetAll()
        {
            // if there are no to-do items, return a 404 Not Found status code
            if(_toDoItems.Count() == 0)
            {
                return NotFound();
            }

            //Map ToDoItem objects to ToDoItemDTO objects
            var toDoItemDTOS = _toDoItems.Select(item => MapToDoItemToDTO(item)).ToList();

            //If there are to-do items, return a 200 OK status code along with the list of to-do items
            return Ok(toDoItemDTOS);
        }


        [HttpGet("{id}")]
        public IActionResult GetToDoItembyId(int id)
        {
            // if there are no to-do items, return a 404 Not Found status code
            if (_toDoItems.Count() == 0)
            {
                return NotFound();
            }

            //Fetch the matching todo item
            var toDoItem = _toDoItems.FirstOrDefault(item => item.Id == id);
            if (toDoItem == null) 
            { 
                return NotFound();
            }

            var toDoItemDTO = MapToDoItemToDTO(toDoItem);
            return Ok(toDoItemDTO);

        }

        [HttpPost]
        public IActionResult CreateToDoItem([FromBody] ToDoItemDTO newItemDTO)
        {
            if (newItemDTO == null) 
            { 
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newItem = new ToDoItem
            { 
                Title = newItemDTO.Title,
                IsCompleted = newItemDTO.IsCompleted
            };

            //Generate a new ID for the to-do item
            int newId = _toDoItems.Max(item => item.Id) + 1;
            newItem.Id = newId;

            _toDoItems.Add(newItem);

            //Return the newly created to-do item along with a 201 created status code
            return CreatedAtAction(nameof(GetToDoItembyId), new { id = newItem.Id }, newItem);
        }

        private ToDoItemDTO MapToDoItemToDTO(ToDoItem item)
        {
            return new ToDoItemDTO
            {
                Id = item.Id,
                Title = item.Title,
                IsCompleted = item.IsCompleted
            };
        }
    }
}
