using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Interfaces
{
    public interface IBorrowedBookRepository : IDisposable
    {
        BorrowedBook GetById(int id);
        List<BorrowedBook> GetBorrowedBooks();

        /// <summary>
        /// Borrows book with custom values
        /// </summary>
        /// <param name="borrowedBook"></param>
        /// <returns></returns>
        BorrowedBook BorrowBook(BorrowedBook borrowedBook);
        void ReturnBook(int borrowedBookId);
    }
}
