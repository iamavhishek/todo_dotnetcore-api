﻿namespace WebAPI.Controllers.Models
{
    public class UpdateTodoRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool isComplete { get; set; }
    }
}
