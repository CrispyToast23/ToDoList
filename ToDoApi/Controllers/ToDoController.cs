using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoApi.Models;
using ToDoApi.ViewModels;

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
                int categoryId = _todoListContext.Categories.Where(e => e.Name == categoryName).FirstOrDefault().Id;
                Todo todo = new(title, description, expirationDate, categoryId);
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
        public string EditTodo(int id, Todo selectedTodo)
        {
            try
            {
                var currentTodo = _todoListContext.Todos.Find(id);

                currentTodo.Title = selectedTodo.Title;
                currentTodo.Description = selectedTodo.Description;
                currentTodo.ExpirationDate = selectedTodo.ExpirationDate;
                currentTodo.Completed = selectedTodo.Completed;
                currentTodo.CategoryId = selectedTodo.CategoryId;

                _todoListContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Success!";
        }

        [HttpGet("DeleteTodo")]
        public string DeleteTodo(int id)
        {
            try
            {
                var currentTodo = _todoListContext.Todos.Find(id);
                _todoListContext.Todos.Remove(currentTodo);

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
