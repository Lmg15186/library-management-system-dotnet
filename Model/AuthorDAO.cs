using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataSetLibrarySystemTableAdapters; // I need this to "see" the TabAuthorTableAdapter

namespace Model
{
    // DATA ACCESS LAYER for authors.
    // The only class that talks to the TabAuthorTableAdapter. It returns the raw
    // table; turning rows into Author objects is the job of AuthorLogic.
    // TabAuthor is a real table, so this DAO both reads and writes.
    public class AuthorDAO
    {
        private TabAuthorTableAdapter tabAuthorTableAdapter = new TabAuthorTableAdapter();

        // -------- READ --------
        // Returns every author. Used by the grid and later the author drop-down.
        public DataSetLibrarySystem.TabAuthorDataTable BrowseAuthors()
        {
            return tabAuthorTableAdapter.GetData();
        }

        // -------- WRITE --------
        // Each method returns the number of rows affected, so the logic layer can
        // tell whether the command did anything.

        // Inserts a new author. AID is an identity column, assigned by SQL Server:
        //     INSERT INTO [TabAuthor] ([AuthorName]) VALUES (@AuthorName)
        public int Insert(string authorName)
        {
            return tabAuthorTableAdapter.Insert(authorName);
        }

        // Updates an author by its AID:
        //     UPDATE [TabAuthor] SET [AuthorName] = @AuthorName
        //     WHERE ([AID] = @Original_AID)
        public int Update(string authorName, int aid)
        {
            return tabAuthorTableAdapter.Update(authorName, aid);
        }

        // Deletes an author by its AID:
        //     DELETE FROM [TabAuthor] WHERE ([AID] = @Original_AID)
        public int Delete(int aid)
        {
            return tabAuthorTableAdapter.Delete(aid);
        }
    }
}