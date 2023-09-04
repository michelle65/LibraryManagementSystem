using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LibraryManagementSystem.UnitTests
{
    [TestClass]
    public class BorrowedBooksServiceTests
    {
        IBookRepository bookRepository;
        IBorrowedBookRepository borrowedBookRepository;

        [TestInitialize]
        public void Intialize()
        {
            bookRepository = new BookRepository();
            borrowedBookRepository = new BorrowedBookRepository();
            SeedData.SeedDb(bookRepository, borrowedBookRepository);
        }
        [TestCleanup]
        public void Cleanup()
        {
            bookRepository.Dispose();
            borrowedBookRepository.Dispose();

            bookRepository = null;
            borrowedBookRepository = null;
        }

        [TestMethod]
        public void When_Returning_A_Book_After_Due_Date_Should_Return_Penalty()
        {
            var bookService = new BookService(bookRepository);
            var borrowedBookService = new BorrowedBookService(borrowedBookRepository, bookRepository);

            var book = new Book { Id = 6, Price = 105, Nume = "BookFive", ISBN = "isbn" };

            var actualBook = bookRepository.CreateNewBook(book);
            var borrowedBook = new BorrowedBook { Id = 6, BookId = actualBook.Id, BorrowDate = DateTime.Now.AddDays(-21), ReturnDate = DateTime.Now, DueDate = DateTime.Now.AddDays(-7), IsReturned = false };
            borrowedBookRepository.BorrowBook(borrowedBook);

            var actualPenalty = borrowedBookService.ReturnBook(actualBook.Id);

            var expectedPenalty = 7.35M;

            Assert.AreEqual(expectedPenalty, actualPenalty);
        }
    }
}
