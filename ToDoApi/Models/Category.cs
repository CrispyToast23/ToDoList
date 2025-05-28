using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
    public class Category
    {
        public Category(string name, string color) 
        { 
            Name = name; 
            Color = color;
        }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Length(7,7)]
        public string Color { get; set; }
    }
}
