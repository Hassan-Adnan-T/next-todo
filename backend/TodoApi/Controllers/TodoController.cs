using Microsoft.AspNetCore.Mvc;
using TodoApi.Services;
using TodoApi.Models;

namespace TodoApi.Controllers{

    [ApiController]
    [Route("api/[controller]")]

    public class TodoController : ControllerBase{
        private readonly FirebaseService _firebaseService;

        public TodoController(FirebaseService firebaseService){
            _firebaseService = firebaseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetAllTodos(){
            try{
                var todos = await _firebaseService.GetAllTodosAsync();
                return Ok(todos);
            }
            catch (Exception ex){
                return StatusCode(500, $"internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(string id){
            try{
                var todo = await _firebaseService.GetTodoByIdAsync(id);
                return Ok(todo);
            }
            catch (KeyNotFoundException){
                return NotFound($"Todo with id {id} not found");
            }
            catch (Exception ex){
                return StatusCode(500, $"internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Todo>> CreateTodo([FromBody] Todo todo){
            try{
                var createdTodo = await _firebaseService.CreateTodoAsync(todo);
                return CreatedAtAction(nameof(GetTodoById), new { id = createdTodo.Id }, createdTodo);
            }
            catch (Exception ex){
                return StatusCode(500, $"internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> UpdateTodo(string id, [FromBody] Todo todo){
            try{
                var updatedTodo = await _firebaseService.UpdateTodoAsync(id, todo);
                return Ok(updatedTodo);
            }
            catch (Exception ex){
                return StatusCode(500, $"internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTodo(string id){
            try{
                await _firebaseService.DeleteTodoAsync(id);
                return NoContent();
            }
            catch (Exception ex){
                return StatusCode(500, $"internal server error: {ex.Message}");
            }
        }
    }
}