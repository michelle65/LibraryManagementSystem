using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookRepository : IDisposable
    {
        Book GetById(int id);
        List<Book> GetAllBooks();
        Book CreateNewBook(Book book);
        Book UpdateBook(Book book);
        int CountBooksAvailableCopies(string bookName);
        void RemoveBook(int bookId);
    }
}
