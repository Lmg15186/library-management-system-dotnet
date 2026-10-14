using System;
using System.Collections.Generic;
using System.Data;  // I need this to use DataRow in the foreach loop
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // BUSINESS LOGIC LAYER for categories.
    // The DAO gives me the raw data (a DataTable). But the rest of the program does
    // not want to work with "rows and columns" - it wants Category objects. So the job
    // of this layer is to TRANSLATE: take the table and return a List<Category>.
    // Same idea as UserLogic and BookLogic.
    //
    // This layer does more than translating, though. It is also where the rules about
    // categories live: what counts as a valid name, and what it means when a command
    // changes no rows at all. The form should not have to know any of that; it just
    // asks for something to happen and gets told whether it worked.
    public class CategoryLogic
    {
        // The logic calls the DAO. Note the "chain": Logic -> DAO -> database.
        // Each level is only aware of the level immediately below it.
        private CategoryDAO categoryDAO = new CategoryDAO();

        // -------- READ --------

        // Returns every category as a list of objects.
        // Returns null when the table is empty, which is the same convention UserLogic
        // and BookLogic use, so every screen checks for "nothing found" the same way.
        public List<Category> BrowseCategories()
        {
            // STEP 1: I request the raw data from the data layer.
            DataSetLibrarySystem.TabCategoryDataTable table = categoryDAO.BrowseCategories();

            // STEP 2: decide what "no data" means before doing any work.
            if (table == null || table.Count == 0)
            {
                return null;
            }

            // STEP 3: create the empty list that I will populate.
            List<Category> categories = new List<Category>();

            // STEP 4: iterate through EVERY row and construct a Category.
            // I loop over the typed TabCategoryRow rather than a plain DataRow, so the
            // columns come back already typed (row.CID is an int) and a misspelled
            // column name is caught by the compiler instead of at run time.
            foreach (DataSetLibrarySystem.TabCategoryRow row in table.Rows)
            {
                Category category = new Category
                {
                    CID = row.CID,
                    CategoryName = row.CategoryName
                };
                categories.Add(category);
            }

            // STEP 5: return the completed list to the caller (the Controller).
            return categories;
        }

        // -------- WRITE --------

        // Adds a new category. Returns the number of rows inserted.
        //
        // The validation lives here rather than only in the form because "a category
        // must have a name" is true no matter which screen is asking. If a second
        // screen ever creates categories, it inherits the same rule automatically.
        //
        // I throw an ArgumentException instead of returning false or 0, because those
        // would only tell the form that something went wrong, not WHAT went wrong. The
        // exception carries the reason with it, so the form can show the user a message
        // that actually explains the problem.
        public int AddCategory(string categoryName)
        {
            ValidateName(categoryName);

            // Trim() removes spaces the user may have typed accidentally at the start
            // or the end. Without it, "Programming" and "Programming " would be stored
            // as two different categories, and the duplicate would be invisible on the
            // screen because the extra space cannot be seen.
            return categoryDAO.Insert(categoryName.Trim());
        }

        // Updates an existing category. Returns the number of rows updated.
        public int UpdateCategory(int cid, string categoryName)
        {
            ValidateName(categoryName);

            // A CID of zero or less means no row was selected in the grid. Checking it
            // here stops the program from sending a pointless UPDATE to the database.
            if (cid <= 0)
            {
                throw new ArgumentException("Please select a category from the list first.");
            }

            return categoryDAO.Update(categoryName.Trim(), cid);
        }

        // Deletes a category. Returns the number of rows deleted.
        //
        // Note what this method does NOT do: it does not check whether any books are
        // using this category. It does not need to. TabBook has a foreign key pointing
        // at TabCategory, so if a book still refers to this category the database
        // itself refuses the delete and throws an exception. The form catches that and
        // explains it to the librarian. Letting the database enforce the rule it
        // already knows about is better than writing a second copy of the same check
        // here, which could drift out of step with the schema.
        public int DeleteCategory(int cid)
        {
            if (cid <= 0)
            {
                throw new ArgumentException("Please select a category from the list first.");
            }

            return categoryDAO.Delete(cid);
        }

        // -------- Private helper: shared validation --------
        // Add and Update enforce identical rules about the name, so the checks live in
        // one private method. Writing them twice would guarantee the two copies drift
        // apart the first time a rule changes.
        private void ValidateName(string categoryName)
        {
            // IsNullOrWhiteSpace catches three cases at once: null, an empty string,
            // and a string that only contains spaces. A user pressing the space bar and
            // clicking Add should not create a category that looks blank in the grid.
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException("Category name is required.");
            }

            // TabCategory.CategoryName is varchar(50) in the database. If I let a
            // longer value through, SQL Server would reject it with a truncation error
            // that means nothing to a librarian. Checking the length here means the
            // user gets a message that tells them exactly what to fix.
            if (categoryName.Trim().Length > 50)
            {
                throw new ArgumentException("Category name cannot be longer than 50 characters.");
            }
        }
    }
}