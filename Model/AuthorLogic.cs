using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // BUSINESS LOGIC LAYER for authors.
    // Translates the DAO's DataTable into a List<Author>, and holds the rules about
    // a valid author name. Same shape as CategoryLogic and LanguageLogic.
    public class AuthorLogic
    {
        private AuthorDAO authorDAO = new AuthorDAO();

        // -------- READ --------
        // Returns every author as a list. Returns null when the table is empty, the
        // same convention the other logic classes use.
        public List<Author> BrowseAuthors()
        {
            DataSetLibrarySystem.TabAuthorDataTable table = authorDAO.BrowseAuthors();

            if (table == null || table.Count == 0)
            {
                return null;
            }

            List<Author> authors = new List<Author>();

            // I loop the typed TabAuthorRow so the columns come back already typed and
            // a misspelled column name is caught by the compiler, not at run time.
            foreach (DataSetLibrarySystem.TabAuthorRow row in table.Rows)
            {
                Author author = new Author
                {
                    AID = row.AID,
                    AuthorName = row.AuthorName
                };
                authors.Add(author);
            }

            return authors;
        }

        // -------- WRITE --------

        // Adds a new author. Trim() removes accidental spaces so two near-identical
        // names are not stored as invisible duplicates.
        public int AddAuthor(string authorName)
        {
            ValidateName(authorName);
            return authorDAO.Insert(authorName.Trim());
        }

        // Updates an author. An AID of zero or less means no row was selected.
        public int UpdateAuthor(int aid, string authorName)
        {
            ValidateName(authorName);

            if (aid <= 0)
            {
                throw new ArgumentException("Please select an author from the list first.");
            }

            return authorDAO.Update(authorName.Trim(), aid);
        }

        // Deletes an author. Like the others, it lets the database enforce the foreign
        // key: if a book still uses this author, the delete is refused and the form
        // explains it, rather than duplicating that check here.
        public int DeleteAuthor(int aid)
        {
            if (aid <= 0)
            {
                throw new ArgumentException("Please select an author from the list first.");
            }

            return authorDAO.Delete(aid);
        }

        // -------- Private helper: shared validation --------
        private void ValidateName(string authorName)
        {
            if (string.IsNullOrWhiteSpace(authorName))
            {
                throw new ArgumentException("Author name is required.");
            }

            if (authorName.Trim().Length > 50)
            {
                throw new ArgumentException("Author name cannot be longer than 50 characters.");
            }
        }
    }
}