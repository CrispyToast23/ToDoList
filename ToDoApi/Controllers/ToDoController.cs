using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.ViewModels;
using ToDoList.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoListContext _todoListContext;

        public TodoController(TodoListContext todoListContext)
        {
            _todoListContext = todoListContext;
        }

        [HttpGet("GetAllTodos")]
        public List<TodoModel> GetAllTodos()
        {
            List<TodoModel> todos = new();
            try
            {
                todos = _todoListContext.Todos.Join(
                _todoListContext.Categories,
                todo => todo.CategoryId,
                category => category.Id,
                (todo, category) => new TodoModel
                {
                    Id = todo.Id,
                    Title = todo.Title,
                    Description = todo.Description,
                    ExpirationDate = todo.ExpirationDate,
                    Completed = todo.Completed,
                    CategoryName = category.Name,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return todos;
        }

        [HttpGet("GetTodoById")]
        public TodoModel GetTodoById(int id)
        {
            TodoModel todo = new();
            try
            {
                todo = _todoListContext.Todos
                    .Where(todo => todo.Id == id)
                    .Join(_todoListContext.Categories,
                    todo => todo.CategoryId,
                    category => category.Id,
                    (todo, category) => new TodoModel
                    {
                        Id = todo.Id,
                        Title = todo.Title,
                        Description = todo.Description,
                        ExpirationDate = todo.ExpirationDate,
                        Completed = todo.Completed,
                        CategoryName = category.Name,
                    }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return todo;
        }

        [HttpGet("CreateTodo")]
        public string CreateTodo(string title, string? description, DateTime expirationDate, string categoryName)
        {
            try
            {
                Category category = _todoListContext.Categories.Where(e => e.Name == categoryName).FirstOrDefault();
                Todo todo = new(title, description, expirationDate, category.Id);
                _todoListContext.Todos.Add(todo);
                _todoListContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Success!";
        }

        [HttpGet("EditTodo")]
        public string EditTodo(int id, int? categoryId, string? title, string? description, DateTime? expirationDate, bool? completed)
        {
            try
            {
                Todo currentTodo = _todoListContext.Todos.Where(e => e.Id == id).FirstOrDefault();

                currentTodo.Title = title;
                currentTodo.Description = description;
                currentTodo.ExpirationDate = (DateTime)expirationDate;
                currentTodo.Completed = (bool)completed;
                currentTodo.CategoryId = (int)categoryId;

                _todoListContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "Success!";
        }
    }
}
