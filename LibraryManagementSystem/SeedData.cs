using LibraryManagementSystem.Interfaces;
using System;

namespace LibraryManagementSystem
{
    public static class SeedData
    {
        public static void SeedDb(IBookRepository bookRepository, IBorrowedBookRepository borrowedBookRepository)
        {
            Models.Book firstBook = bookRepository.CreateNewBook(new Models.Book
            {
                ISBN = "87-23-90157-8",
                Name = "BookOne",
                Price = 150,
            });

            borrowedBookRepository.BorrowBook(new Models.BorrowedBook
            {
                BookId = firstBook.Id,
                BorrowDate = new DateTime(2023, 09, 1),
                DueDate = new DateTime(2023, 09, 1).AddDays(14),
                ReturnDate = null,
                IsReturned = false
            });
            var secondBook = bookRepository.CreateNewBook(new Models.Book
            {
                ISBN = "87-23-90157-8",
                Name = "BookOne",
                Price = 150
            });

            var thirdBook = bookRepository.CreateNewBook(new Models.Book
            {
                Id = 3,
                ISBN = "87-23-90157-8",
                Name = "BookOne",
                Price = 150
            });

            var fourthBook = bookRepository.CreateNewBook(new Models.Book
            {
                Id = 4,
                ISBN = "87-23-90157-9",
                Name = "BookTwo",
                Price = 150
            });

            var fifthBook = bookRepository.CreateNewBook(new Models.Book
            {
                Id = 5,
                ISBN = "87-23-90157-10",
                Name = "BookThree",
                Price = 150
            });

            borrowedBookRepository.BorrowBook(new Models.BorrowedBook
            {
                BookId = fifthBook.Id,
                BorrowDate = new DateTime(2023, 08, 09),
                DueDate = new DateTime(2023, 08, 09).AddDays(14),
                ReturnDate = null,
                IsReturned = false
            });
        }
    }
}
