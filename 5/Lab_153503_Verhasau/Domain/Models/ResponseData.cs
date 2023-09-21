﻿namespace Domain.Models
{
    public class ResponseData<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string? ErrorMessage { get; set; }
    }
}
