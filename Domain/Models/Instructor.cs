﻿namespace Domain.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? CardNumber { get; set; }
        public string Bio { get; set; }
        public float Rate { get; set; }
    }
}
