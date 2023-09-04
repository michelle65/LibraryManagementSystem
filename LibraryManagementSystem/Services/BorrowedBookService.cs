using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Services
{
    public class BorrowedBookService : IBorrowedBookService
    {
        private const decimal PenaltyPerDay = 0.01M;

        private readonly IBorrowedBookRepository _borrowedBookRepository;
        private readonly IBookRepository _bookRepository;

        public BorrowedBookService(IBorrowedBookRepository borrowedBookRepository, IBookRepository bookRepository)
        {
            _borrowedBookRepository = borrowedBookRepository;
            _bookRepository = bookRepository;
        }
        public List<BorrowedBook> GetBorrowedBooks()
        {
            return _borrowedBookRepository.GetBorrowedBooks();
        }
        public void BorrowingBook(int bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book is null)
            {
                throw new ArgumentException(nameof(bookId));
            }

            var borrowed = _borrowedBookRepository.GetById(book.Id);
            if (borrowed != null)
            {
                Console.WriteLine("Book is already borrowed!");
                throw new ArgumentException(nameof(bookId));
            }

            var newBorrowedBook = new BorrowedBook
            {
                BookId = bookId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14),
                IsReturned = false,
            };

            _borrowedBookRepository.BorrowBook(newBorrowedBook);
        }
        public void BorrowBookAdded(BorrowedBook borrowedBookParam)
        {
            _borrowedBookRepository.BorrowBook(borrowedBookParam);
        }

        public decimal ReturnBook(int bookId)
        {
            var book = _bookRepository.GetById(bookId);
            if (book is null)
            {
                Console.WriteLine($"Book {bookId} does not exitst!");
                throw new ArgumentException(nameof(bookId));
            }
            var borrowedBook = _borrowedBookRepository.GetById(bookId);
            if (borrowedBook is null)
            {
                Console.WriteLine($"Book {bookId} was not borrowed!");
                throw new ArgumentException(nameof(bookId));
            }

            decimal penalty = 0;

            if (borrowedBook.DueDate < DateTime.Now)
            {
                var dueDays = (DateTime.Now - borrowedBook.DueDate).Days;

                penalty = book.Price * PenaltyPerDay * dueDays;

                Console.WriteLine($"Book {bookId}: Returned with a penalty of {penalty}");
            }
            else
            {
                Console.WriteLine($"Book {bookId} - {book.Name} : Returned on time.");
            }

            _borrowedBookRepository.ReturnBook(bookId);

            return penalty;
        }

        public BorrowedBook GetById(int id)
        {
            return _borrowedBookRepository.GetById(id);
        }
    }
}
