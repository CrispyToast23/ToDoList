using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.ViewModels;
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
        public List<TodoModel> GetAllTodos()
        {
            List<TodoModel> todos = _toDoListContext.Todos.Join(
                _toDoListContext.Categories,
                e => e.CategoryId,
                x => x.Id,
                (e, x) => new TodoModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description= e.Description,
                    ExpirationDate = e.ExpirationDate,
                    Completed= e.Completed,
                    CategoryName = x.Name,
                }).ToList();

            return todos;
        }

        [HttpGet("CreateTodo")]
        public string CreateTodo(string title, string? description, DateTime expirationDate, string categoryName)
        {
            try
            {
                Category category = _toDoListContext.Categories.Where(e => e.Name == categoryName).First();
                Todo todo = new(title, description, expirationDate, category.Id);
                _toDoListContext.Todos.Add(todo);
                _toDoListContext.SaveChanges();
            } catch (Exception ex) 
            {
                return ex.Message;
            }
            return "Success!";
        }
    }
}
