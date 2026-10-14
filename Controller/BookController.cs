using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    // CONTROLLER LAYER for Book. The screen talks only to this class, which forwards
    // calls to BookLogic. Thin by design - it holds no rules of its own.
    //
    //     Form -> Controller -> Logic -> DAO -> TableAdapter -> database
    public class BookController
    {
        private BookLogic bookLogic = new BookLogic();

        // -------- Browse / search --------
        public List<Book> BrowseBooks()
        {
            return bookLogic.BrowseBooks();
        }

        // Title search - required by the brief.
        public List<Book> SearchByBookName(string bookName)
        {
            return bookLogic.SearchByBookName(bookName);
        }

        public List<Book> SearchByAuthor(string authorName)
        {
            return bookLogic.SearchByAuthor(authorName);
        }

        public List<Book> SearchByCategory(string categoryName)
        {
            return bookLogic.SearchByCategory(categoryName);
        }

        // -------- Maintenance --------
        public int AddBook(Book book)
        {
            return bookLogic.AddBook(book);
        }

        public int UpdateBook(Book book, string originalIsbn)
        {
            return bookLogic.UpdateBook(book, originalIsbn);
        }

        public int DeleteBook(string isbn)
        {
            return bookLogic.DeleteBook(isbn);
        }
    }
}