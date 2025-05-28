using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoListContext _toDoListContext;

        public ToDoController(ToDoListContext toDoListContext)
        {
            _toDoListContext = toDoListContext;
        }

        [HttpGet("GetAllTodos")]
        public List<Todo> GetAllTodos()
        {
            List<Todo> todos = _toDoListContext.Todos.ToList();

            return todos;
        }
    }
}
