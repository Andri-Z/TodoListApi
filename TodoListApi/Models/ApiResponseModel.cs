﻿namespace TodoListApi.Models
{
    public class ApiResponseModel
    {
        public List<TasksModel> Data { get; set; } = new();
        public int Page { get; set; }
        public int Limit { get; set; }
        public int Total { get; set; }
    }
}
