using LibraryManagementSystem.Repositories;
using LibraryManagementSystem.Services;
using System;
using System.Linq;

namespace LibraryManagementSystem
{
    public class Program
    {
        static void Main(string[] args)
        {

            var borrowBookRepository = new BorrowedBookRepository();
            var bookRepository = new BookRepository();
            var bookService = new BookService(bookRepository);
            var borrowedBookService = new BorrowedBookService(borrowBookRepository, bookRepository);

            SeedData.SeedDb(bookRepository, borrowBookRepository);
            while (true)
            {
                Console.WriteLine("Library Management System Menu:");
                Console.WriteLine("1) Add a new book");
                Console.WriteLine("2) Get the list with all books");
                Console.WriteLine("3) Get the number of copies available for one book");
                Console.WriteLine("4) Borrow a book");
                Console.WriteLine("5) Return a book");
                Console.WriteLine("6) Exit");
                Console.Write("Enter your choice: ");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:

                            Console.Write("Enter the name of the book: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter the isbn of the book: ");
                            string isbn = Console.ReadLine();
                            Console.Write("Enter the price of the book: ");
                            if (decimal.TryParse(Console.ReadLine(), out decimal price))
                            {
                                bookService.AddBook(name, isbn, price);
                                Console.WriteLine("Book added successfully!");
                            }
                            else
                            {
                                Console.WriteLine("Invalid rental price format.");
                            }
                            break;

                        case 2:
                            PrintAllBooks(bookService);

                            break;

                        case 3:

                            Console.Write("Enter the name of the book: ");
                            string nameOfTheBook = Console.ReadLine();
                            GetBooksAvailableBooks(bookService, borrowedBookService, nameOfTheBook);
                            break;

                        case 4:
                            Console.Write("Enter the id of the book you want to borrow: ");
                            if (int.TryParse(Console.ReadLine(), out int bookId))
                            {
                                borrowedBookService.BorrowingBook(bookId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid bookId value format.");
                            }
                            break;
                        case 5:
                            Console.Write("Enter the id of the book you want to return: ");
                            if (int.TryParse(Console.ReadLine(), out int bookToReturnId))
                            {
                                borrowedBookService.ReturnBook(bookToReturnId);
                            }
                            else
                            {
                                Console.WriteLine("Invalid bookToReturnId value format.");
                            }
                            break;

                        case 7:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select a valid option.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                }
            }


        }

        private static void PrintAllBooks(BookService bookService)
        {
            var bookGroups = bookService.GetAllBooks().GroupBy(g => g.ISBN);

            foreach (var bookGroup in bookGroups)
            {
                var firstBook = bookGroup.First();
                Console.WriteLine($"Book id:{firstBook.Id}, Title:{firstBook.Name}, ISBN:{firstBook.ISBN}, Price:{firstBook.Price}, Copies:{bookGroup.Count()}");
            }
        }
        private static void GetBooksAvailableBooks(BookService bookService, BorrowedBookService borrowedBookService, string bookName)
        {
            var bookCollection = bookService.GetAllBooks();
            var borrowedBooks = borrowedBookService.GetBorrowedBooks();
            var matchingBooks = bookCollection.FirstOrDefault(book => book.Name == bookName);

            if (matchingBooks != null)
            {

                int bookId = matchingBooks.Id;
                int allBooks = bookCollection.Count(borrowedBook => borrowedBook.Name == bookName);
                int borrowedCount = borrowedBooks.Count(borrowedBook => borrowedBook.BookId == bookId && borrowedBook.IsReturned == false);

                int availableCount = allBooks - borrowedCount;
                Console.WriteLine($"----------------------------------");

                Console.WriteLine($"Book Name: {bookName}");
                Console.WriteLine($"Total Copies: {allBooks}");
                Console.WriteLine($"Borrowed Copies: {borrowedCount}");
                Console.WriteLine($"Available Copies: {availableCount}");
                Console.WriteLine($"----------------------------------");

            }
            else
            {
                Console.WriteLine($"No book found with the name: {bookName}");
            }
        }
    }
}