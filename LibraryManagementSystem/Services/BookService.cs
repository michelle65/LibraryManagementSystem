using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
             
        }
        public Book GetById(int id)
        {
            return _bookRepository.GetById(id);
        }
        public void AddBook(string name,string isbn,decimal price)
        {
            int lastId = _bookRepository.GetAllBooks().Max(b => b.Id);

            _bookRepository.CreateNewBook(new Book { Id = lastId++, Nume = name, ISBN = isbn, Price = price });

        }

        public void UpdateBook(Book book)
        {
            _bookRepository.UpdateBook(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.RemoveBook(id);
        }

        public int CountBooksAvailableCopies(string bookName)
        {
           return _bookRepository.CountBooksAvailableCopies(bookName);
        }
    }
}
