using LibraryManagementSystem.Interfaces;
using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagementSystem.Repositories
{
    public class BorrowedBookRepository : IBorrowedBookRepository
    {
        private List<BorrowedBook> borrowedBooks = new List<BorrowedBook>();
        private int nextId = 1;

        public List<BorrowedBook> GetBorrowedBooks()
        {
            return borrowedBooks;
        }
        public BorrowedBook BorrowBook(BorrowedBook borrowedBook)
        {
            borrowedBook.Id = nextId++;
            borrowedBooks.Add(borrowedBook);

            return borrowedBook;
        }

        public void ReturnBook(int borrowedBookId)
        {
            var borrowedBook = borrowedBooks.FirstOrDefault(b => b.Id == borrowedBookId);
            if (borrowedBook != null)
            {
                borrowedBook.ReturnDate = DateTime.Now;
                borrowedBook.IsReturned = true;
            }
        }

        public void Dispose()
        {
            borrowedBooks.Clear();
        }

        public BorrowedBook GetById(int id)
        {
            return borrowedBooks.FirstOrDefault(bb => bb.BookId == id && bb.IsReturned == false);
        }
    }
}
