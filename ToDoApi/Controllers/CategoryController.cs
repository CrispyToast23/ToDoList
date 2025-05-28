using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly TodoListContext _todoListContext;

        public CategoryController(TodoListContext todoListContext)
        {
            _todoListContext = todoListContext;
        }

        [HttpGet("GetAllCategories")]
        public List<Category> GetAllCategories()
        {
            var t = _todoListContext.Categories.ToList();

            return t;
        }

        [HttpGet("CreateCategory")]
        public string CreateCategory(string name, string color)
        {
            try
            {
                Category category = new(name, color);
                _todoListContext.Categories.Add(category);
                _todoListContext.SaveChanges();
            } catch (Exception ex) 
            {
                return ex.Message;
            }
            return "Success!";
        }
    }
}
