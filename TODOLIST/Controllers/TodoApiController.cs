
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TODOLIST.Models;
using TODOLIST.Services;

namespace TODOLIST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TodoController : ControllerBase
    {
        private readonly TodoRepository _repository;

        public TodoController()
        {
            _repository = new TodoRepository();
        }

        [HttpGet("GetAll")]

        public ActionResult<IEnumerable<TodoItem>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var items = _repository.GetAll(pageNumber, pageSize);
            return Ok(items);
        }

        [HttpGet("GetById/{id}")]
        public ActionResult<TodoItem> GetById(int id)
        {
            var item = _repository.GetById(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost("Create")]
        public ActionResult<TodoItem> Create(TodolistDTO requestdto)
        {
            if (string.IsNullOrWhiteSpace(requestdto.Title))
                return BadRequest("Title is required.");
            var item = new TodoItem();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                item.Id = requestdto.Id;
                item.Title = requestdto.Title;
                item.Description = requestdto.Description;
                item.IsCompleted = requestdto.IsCompleted;

                var createdItem = _repository.Create(item);

                return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
            }




        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, TodolistDTO todorequest)
        {
            if (string.IsNullOrWhiteSpace(todorequest.Title))
                return BadRequest("Title is required.");
            var item = new TodoItem();
            item.Id = id;
            item.Title = todorequest.Title;
            item.Description = todorequest.Description;
            item.IsCompleted = todorequest.IsCompleted;

            if (!_repository.Update(id, item))
                return NotFound();

            return NoContent();
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (!_repository.Delete(id))
                return NotFound();

            return NoContent();
        }
    }
}
