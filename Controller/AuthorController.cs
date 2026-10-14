using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    // CONTROLLER LAYER for authors. The screen talks only to this class, which
    // forwards the calls to AuthorLogic. Same thin pattern as the others:
    //
    //     FormAuthor -> AuthorController -> AuthorLogic -> AuthorDAO
    //                -> TabAuthorTableAdapter -> database
    public class AuthorController
    {
        private AuthorLogic authorLogic = new AuthorLogic();

        // Returns every author. Fills the maintenance grid, and later the author
        // drop-down on the book maintenance form.
        public List<Author> BrowseAuthors()
        {
            return authorLogic.BrowseAuthors();
        }

        // Adds a new author. Returns the number of rows inserted.
        public int AddAuthor(string authorName)
        {
            return authorLogic.AddAuthor(authorName);
        }

        // Updates an existing author. Returns the number of rows updated.
        public int UpdateAuthor(int aid, string authorName)
        {
            return authorLogic.UpdateAuthor(aid, authorName);
        }

        // Deletes an author. Returns the number of rows deleted.
        public int DeleteAuthor(int aid)
        {
            return authorLogic.DeleteAuthor(aid);
        }
    }
}