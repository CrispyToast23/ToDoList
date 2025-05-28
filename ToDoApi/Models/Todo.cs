using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToDoList.Models
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Id))]
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool? Completed { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }
}
