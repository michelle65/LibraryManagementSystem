using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.UnitTests
{

    [TestClass]
    public class BookRepositoryTests
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
        public void When_Adding_Ok_Book_Should_Return_Ok()
        {
            // Arrange
            var book = new Book { Id = 1, Name = "Sample Book", ISBN = "1221-123132-22" };

            // Act
            var actualBook = bookRepository.CreateNewBook(book);

            // Assert
            var expectedBook = new Book { Id = 1, Name = "Sample Book", ISBN = "1221-123132-22" };

            Assert.IsNotNull(actualBook);
            Assert.AreEqual(expectedBook.Name, actualBook.Name);
            Assert.AreEqual(expectedBook.ISBN, actualBook.ISBN);
        }
        [TestMethod]
        public void When_Getting_A_Book_Not_In_DB_Should_Return_Null()
        {
            // Arrange empty repositoy
            var bookRepository = new BookRepository();

            // Act
            var actualBook = bookRepository.GetById(1);

            // Assert
            Assert.IsNull(actualBook);
        }
    }
}
