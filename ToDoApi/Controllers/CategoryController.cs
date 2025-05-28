using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.Models;

namespace ToDoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ToDoListContext _toDoListContext;

        public CategoryController(ToDoListContext toDoListContext)
        {
            _toDoListContext = toDoListContext;
        }

        [HttpGet("GetAllCategories")]
        public List<Category> GetAllCategories()
        {
            var t = _toDoListContext.Categories.ToList();

            return t;
        }

        [HttpGet("CreateCategory")]
        public string CreateCategory(string name, string color)
        {
            try
            {
                Category category = new(name, color);
                _toDoListContext.Categories.Add(category);
                _toDoListContext.SaveChanges();
            } catch (Exception ex) 
            {
                return ex.Message;
            }
            return "Success!";
        }
    }
}
