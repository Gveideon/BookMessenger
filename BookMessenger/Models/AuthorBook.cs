﻿namespace BookMessenger.Models
{
    public class AuthorBook
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public int BookId { get; set; } 
        public Book? Book { get; set; }
        public int NumberOfAuthor { get; set; }
    }
}
