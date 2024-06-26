﻿namespace BookMessenger.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<Book> Books { get; set; } = new();
        public override string ToString() => $"{Name}";

    }
}
