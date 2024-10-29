using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[ApiController]
[Route("api/todo")]
public class TodoController : ControllerBase
{
    // In-memory list to store Todo items
    public static List<TodoItem> todos = new List<TodoItem>();
    public static int nextId = 1;

    // GET: api/todo
    [HttpGet]
    public IActionResult GetTodos()
    {
        if (todos.Count == 0) return NotFound();
        return Ok(todos);
    }

    // POST: api/todo
    [HttpPost]
    public IActionResult AddTodo([FromBody] TodoItem item)
    {
        if (item == null)
        {
            return BadRequest("Empty TodoItem");
        }
        if (string.IsNullOrEmpty(item.Title))
        {
            return BadRequest("Invalid title");
        }
        item.Id = nextId;
        nextId++;
        todos.Add(item);
        return Ok(item);
    }

    // PUT: api/todo/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTodo(int id)
    {
        var todo = todos.FirstOrDefault(x => x.Id == id);
        if (todo != null)
        {
            todo.IsCompleted = !todo.IsCompleted;
            return Ok(todo);
        }
        else
        {
            return NoContent();
        }
    }

    // DELETE: api/todo/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTodo(int id)
    {
        var todo = todos.FirstOrDefault(x => x.Id == id);
        if (todo != null)
        {
            todos.Remove(todo);
            return NoContent();
        }
        else
        {
            return NoContent();
        }
    }
}
