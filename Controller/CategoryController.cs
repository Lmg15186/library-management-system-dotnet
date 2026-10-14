using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    // CONTROLLER LAYER for categories. The screen talks only to this class, which
    // forwards the calls to CategoryLogic. Same pattern as UserController and
    // BookController.
    //
    // The controller is meant to be thin: it is the single door the presentation layer
    // knocks on, and it holds no rules of its own. Deciding WHAT is valid belongs in
    // CategoryLogic, and knowing HOW to reach the database belongs in CategoryDAO. The
    // full chain is:
    //
    //     FormCategory -> CategoryController -> CategoryLogic -> CategoryDAO
    //                  -> TabCategoryTableAdapter -> database
    //
    // A class this thin can look pointless at first - every method is one line. But it
    // is what keeps FormCategory from ever seeing the DAO or the DataSet. If the way
    // categories are stored ever changes, the form does not change at all, because it
    // only ever knew about this class. That is exactly what happened with the login in
    // Assessment 2: the whole method underneath was replaced, and FormLogin never
    // noticed.
    public class CategoryController
    {
        // The controller depends on the logic layer, not on the DAO.
        private CategoryLogic categoryLogic = new CategoryLogic();

        // Returns every category. Used to fill the maintenance grid, and later the
        // category drop-down on the book maintenance form.
        public List<Category> BrowseCategories()
        {
            return categoryLogic.BrowseCategories();
        }

        // Adds a new category. Returns the number of rows inserted.
        public int AddCategory(string categoryName)
        {
            return categoryLogic.AddCategory(categoryName);
        }

        // Updates an existing category. Returns the number of rows updated.
        public int UpdateCategory(int cid, string categoryName)
        {
            return categoryLogic.UpdateCategory(cid, categoryName);
        }

        // Deletes a category. Returns the number of rows deleted.
        public int DeleteCategory(int cid)
        {
            return categoryLogic.DeleteCategory(cid);
        }
    }
}