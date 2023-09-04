using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Repositories
{
    public class BookRepository : IBookRepository
    {
        private List<Book> books = new List<Book>();
        private int nextId = 0;

        public List<Book> GetAllBooks()
        {
            return books.ToList();
        }
        public Book CreateNewBook(Book book)
        {
            book.Id = nextId++;

            books.Add(book);

            return book;
        }

        public int CountBooksAvailableCopies(string bookName)
        {
            Console.Write("Enter the title of the book: ");
            string title = Console.ReadLine();
            int copiesAvailable = books.Count(b => b.Nume == bookName);
            Console.WriteLine($"Copies available for {title}: {copiesAvailable}");
            return copiesAvailable;
        }

        public Book UpdateBook(Book book)
        {
            var existingBook = GetById(book.Id);
            if (existingBook != null)
            {
                existingBook.Nume = book.Nume;
                existingBook.ISBN = book.ISBN;
                existingBook.Price = book.Price;
            }
            return existingBook;
        }

        public Book GetById(int id)
        {
            return books.FirstOrDefault(book => book.Id == id);
        }
        public void RemoveBook(int id)
        {
            var existingBook = GetById(id);
            if (existingBook != null)
            {
                books.Remove(existingBook);
            }
        }

        public void Dispose()
        {
            books.Clear();
        }
    }
}
