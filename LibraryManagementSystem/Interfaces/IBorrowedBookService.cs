using LibraryManagementSystem.Models;
using System.Collections.Generic;

namespace LibraryManagementSystem.Interfaces
{
    internal interface IBorrowedBookService
    {
        BorrowedBook GetById(int id);
        List<BorrowedBook> GetBorrowedBooks();
        void BorrowBookAdded(BorrowedBook borrowedBook);
        decimal ReturnBook(int borrowedBookId);
        void BorrowingBook(int borrowedBookId);
    }
}
