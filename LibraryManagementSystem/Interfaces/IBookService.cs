using LibraryManagementSystem.Models;
using System.Collections.Generic;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBookService
    {
        Book GetById(int id);
        IEnumerable<Book> GetAllBooks();
        void AddBook(string name,string isbn,decimal price);
        int CountBooksAvailableCopies(string bookName);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }
}
