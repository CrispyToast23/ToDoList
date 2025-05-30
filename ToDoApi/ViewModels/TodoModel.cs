﻿namespace ToDoApi.ViewModels
{
    public class TodoModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Completed { get; set; }
        public string CategoryName { get; set; }
    }
}
