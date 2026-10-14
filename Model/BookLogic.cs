using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // BUSINESS LOGIC LAYER for Book.
    // Translates the DAO's raw ViewBook table into a List<Book>, and holds the rules
    // for adding, updating and deleting a book.
    public class BookLogic
    {
        private BookDAO bookDAO = new BookDAO();

        // ================= READ =================

        public List<Book> BrowseBooks()
        {
            return ConvertToBookList(bookDAO.BrowseBooks());
        }

        // Title search - required by the brief.
        public List<Book> SearchByBookName(string bookName)
        {
            return ConvertToBookList(bookDAO.SearchByBookName(bookName));
        }

        public List<Book> SearchByAuthor(string authorName)
        {
            return ConvertToBookList(bookDAO.SearchByAuthor(authorName));
        }

        public List<Book> SearchByCategory(string categoryName)
        {
            return ConvertToBookList(bookDAO.SearchByCategory(categoryName));
        }

        // ================= WRITE =================

        // Adds a new book. Returns the number of rows inserted.
        // Validation lives here so every screen that ever creates a book follows the
        // same rules.
        public int AddBook(Book book)
        {
            ValidateBook(book, true);

            return bookDAO.Insert(book.ISBN.Trim(), book.BookName.Trim(), book.AuthorID,
                                  book.CategoryID, book.LanguageID, book.PublishYear,
                                  book.Pages, book.Publisher.Trim());
        }

        // Updates an existing book, found by its original ISBN. The ISBN can itself be
        // edited, so the original is passed separately to locate the row.
        public int UpdateBook(Book book, string originalIsbn)
        {
            ValidateBook(book, false);

            if (string.IsNullOrWhiteSpace(originalIsbn))
            {
                throw new ArgumentException("Please select a book from the list first.");
            }

            return bookDAO.Update(book.ISBN.Trim(), book.BookName.Trim(), book.AuthorID,
                                  book.CategoryID, book.LanguageID, book.PublishYear,
                                  book.Pages, book.Publisher.Trim(), originalIsbn);
        }

        // Deletes a book by its ISBN. Like the other maintenance screens, it lets the
        // database enforce the foreign keys: if a borrow or reserve row still points at
        // this book, the delete is refused and the form explains it.
        public int DeleteBook(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                throw new ArgumentException("Please select a book from the list first.");
            }

            return bookDAO.Delete(isbn.Trim());
        }

        // -------- Private helper: shared validation --------
        // isNew is true for Add and false for Update, because the two differ only in
        // whether a drop-down selection is being made for the first time; the field
        // rules themselves are identical, so they live in one place.
        private void ValidateBook(Book book, bool isNew)
        {
            if (book == null)
            {
                throw new ArgumentException("No book details were provided.");
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                throw new ArgumentException("ISBN is required.");
            }

            // TabBook.ISBN is varchar(13), matching a real 13-digit ISBN.
            if (book.ISBN.Trim().Length > 13)
            {
                throw new ArgumentException("ISBN cannot be longer than 13 characters.");
            }

            if (string.IsNullOrWhiteSpace(book.BookName))
            {
                throw new ArgumentException("Book name is required.");
            }

            // The three foreign keys come from drop-downs. A value of zero or less means
            // nothing was picked, so the book would have no author/category/language.
            if (book.AuthorID <= 0)
            {
                throw new ArgumentException("Please choose an author.");
            }

            if (book.CategoryID <= 0)
            {
                throw new ArgumentException("Please choose a category.");
            }

            if (book.LanguageID <= 0)
            {
                throw new ArgumentException("Please choose a language.");
            }

            // Publish year and pages are numbers on the form, but I still guard against
            // values that make no sense for a real book.
            if (book.PublishYear <= 0)
            {
                throw new ArgumentException("Please enter a valid publish year.");
            }

            if (book.Pages <= 0)
            {
                throw new ArgumentException("Please enter a valid number of pages.");
            }

            if (string.IsNullOrWhiteSpace(book.Publisher))
            {
                throw new ArgumentException("Publisher is required.");
            }
        }

        // -------- Helper: ViewBook table -> List<Book> --------
        private List<Book> ConvertToBookList(DataSetLibrarySystem.ViewBookDataTable table)
        {
            if (table == null || table.Count == 0)
            {
                return null;
            }

            List<Book> books = new List<Book>();

            foreach (DataSetLibrarySystem.ViewBookRow row in table.Rows)
            {
                Book book = new Book
                {
                    ISBN = row.ISBN,
                    BookName = row.BookName,
                    Publisher = row.Publisher,
                    PublishYear = row.PublishYear,
                    Pages = row.Pages,
                    AuthorName = row.AuthorName,
                    CategoryName = row.CategoryName,
                    LanguageName = row.LanguageName,
                    AuthorID = row.Author,
                    CategoryID = row.Category,
                    LanguageID = row.Language
                };

                books.Add(book);
            }

            return books;
        }
    }
}