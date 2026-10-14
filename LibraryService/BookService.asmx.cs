using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using Controller;
using Model;

namespace LibraryService
{
    // WEB SERVICE for books: browse, the three search criteria, and the maintenance
    // (add, update, delete).
    //
    // Same idea as UserService: basic types go in, a DataTable comes back, and the
    // work is still done by BookController -> BookLogic -> BookDAO.
    [WebService(Namespace = "http://ait.edu.au/library/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class BookService : System.Web.Services.WebService
    {
        private BookController bookController = new BookController();

        // The table carries both the readable names (for the search grid) and the
        // foreign-key IDs (for the maintenance drop-downs), because Book carries both.
        private DataTable CreateBookTable()
        {
            DataTable table = new DataTable("Books");
            table.Columns.Add("ISBN", typeof(string));
            table.Columns.Add("BookName", typeof(string));
            table.Columns.Add("Publisher", typeof(string));
            table.Columns.Add("PublishYear", typeof(int));
            table.Columns.Add("Pages", typeof(int));
            table.Columns.Add("AuthorName", typeof(string));
            table.Columns.Add("CategoryName", typeof(string));
            table.Columns.Add("LanguageName", typeof(string));
            table.Columns.Add("AuthorID", typeof(int));
            table.Columns.Add("CategoryID", typeof(int));
            table.Columns.Add("LanguageID", typeof(int));
            return table;
        }

        // Converts the list the controller returns into the table the client receives.
        // Every read method ends here, so the shape of the answer is always the same.
        private DataTable ToBookTable(List<Book> books)
        {
            DataTable table = CreateBookTable();

            if (books == null)
            {
                return table;
            }

            foreach (Book book in books)
            {
                DataRow row = table.NewRow();
                row["ISBN"] = book.ISBN;
                row["BookName"] = book.BookName;
                row["Publisher"] = book.Publisher;
                row["PublishYear"] = book.PublishYear;
                row["Pages"] = book.Pages;
                row["AuthorName"] = book.AuthorName;
                row["CategoryName"] = book.CategoryName;
                row["LanguageName"] = book.LanguageName;
                row["AuthorID"] = book.AuthorID;
                row["CategoryID"] = book.CategoryID;
                row["LanguageID"] = book.LanguageID;
                table.Rows.Add(row);
            }

            return table;
        }

        // ---------- SELECT ----------
        [WebMethod(Description = "Returns every book as a table.")]
        public DataTable GetBooks()
        {
            return ToBookTable(bookController.BrowseBooks());
        }

        [WebMethod(Description = "Search criteria 1: by book name (partial match).")]
        public DataTable SearchByBookName(string bookName)
        {
            return ToBookTable(bookController.SearchByBookName(bookName));
        }

        [WebMethod(Description = "Search criteria 2: by author (partial match).")]
        public DataTable SearchByAuthor(string authorName)
        {
            return ToBookTable(bookController.SearchByAuthor(authorName));
        }

        [WebMethod(Description = "Search criteria 3: by category (partial match).")]
        public DataTable SearchByCategory(string categoryName)
        {
            return ToBookTable(bookController.SearchByCategory(categoryName));
        }

        // ---------- INSERT ----------
        // The ISBN is typed by the librarian, so unlike the other tables it is sent in.
        [WebMethod(Description = "Adds a new book.")]
        public int InsertBook(string isbn, string bookName, int authorID, int categoryID,
                              int languageID, int publishYear, int pages, string publisher)
        {
            Book book = BuildBook(isbn, bookName, authorID, categoryID, languageID,
                                  publishYear, pages, publisher);

            return bookController.AddBook(book);
        }

        // ---------- UPDATE ----------
        // originalIsbn is separate because the ISBN itself can be edited, and the old
        // value is what finds the row.
        [WebMethod(Description = "Updates a book, found by its original ISBN.")]
        public int UpdateBook(string isbn, string bookName, int authorID, int categoryID,
                              int languageID, int publishYear, int pages, string publisher,
                              string originalIsbn)
        {
            Book book = BuildBook(isbn, bookName, authorID, categoryID, languageID,
                                  publishYear, pages, publisher);

            return bookController.UpdateBook(book, originalIsbn);
        }

        // ---------- DELETE ----------
        [WebMethod(Description = "Deletes a book by ISBN.")]
        public int DeleteBook(string isbn)
        {
            return bookController.DeleteBook(isbn);
        }

        // Builds the Book object from the basic values. Insert and Update both need
        // it, so it is written once. The object stays inside the service.
        private Book BuildBook(string isbn, string bookName, int authorID, int categoryID,
                               int languageID, int publishYear, int pages, string publisher)
        {
            return new Book
            {
                ISBN = isbn,
                BookName = bookName,
                AuthorID = authorID,
                CategoryID = categoryID,
                LanguageID = languageID,
                PublishYear = publishYear,
                Pages = pages,
                Publisher = publisher
            };
        }
    }
}