using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Length(7,7)]
        public string Color { get; set; }
    }
}
