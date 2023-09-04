using System;

namespace LibraryManagementSystem.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public DateTime DueDate { get; set; }

        public bool IsReturned { get; set; }
    }
}
