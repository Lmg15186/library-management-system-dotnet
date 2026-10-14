using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using Controller;
using Model;

namespace LibraryService
{
    // WEB SERVICE for the master data: Category, Language and Author.
    //
    // The three are in one service because they are the same shape - an ID and a name -
    // and the brief groups them together as the reference data a staff member
    // maintains. Each one keeps its own controller, so the layers below are untouched.
    [WebService(Namespace = "http://ait.edu.au/library/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class MasterService : System.Web.Services.WebService
    {
        private CategoryController categoryController = new CategoryController();
        private LanguageController languageController = new LanguageController();
        private AuthorController authorController = new AuthorController();

        // All three tables are built the same way, so one helper takes the two column
        // names and returns an empty table ready to be filled.
        private DataTable CreateIdNameTable(string tableName, string idColumn, string nameColumn)
        {
            DataTable table = new DataTable(tableName);
            table.Columns.Add(idColumn, typeof(int));
            table.Columns.Add(nameColumn, typeof(string));
            return table;
        }

        // ================= CATEGORY =================

        [WebMethod(Description = "Returns every category as a table.")]
        public DataTable GetCategories()
        {
            DataTable table = CreateIdNameTable("Categories", "CID", "CategoryName");
            List<Category> categories = categoryController.BrowseCategories();

            if (categories != null)
            {
                foreach (Category category in categories)
                {
                    table.Rows.Add(category.CID, category.CategoryName);
                }
            }

            return table;
        }

        [WebMethod(Description = "Adds a new category.")]
        public int InsertCategory(string categoryName)
        {
            return categoryController.AddCategory(categoryName);
        }

        [WebMethod(Description = "Updates a category, found by CID.")]
        public int UpdateCategory(int cid, string categoryName)
        {
            return categoryController.UpdateCategory(cid, categoryName);
        }

        [WebMethod(Description = "Deletes a category by CID.")]
        public int DeleteCategory(int cid)
        {
            return categoryController.DeleteCategory(cid);
        }

        // ================= LANGUAGE =================

        [WebMethod(Description = "Returns every language as a table.")]
        public DataTable GetLanguages()
        {
            DataTable table = CreateIdNameTable("Languages", "LID", "LanguageName");
            List<Language> languages = languageController.BrowseLanguages();

            if (languages != null)
            {
                foreach (Language language in languages)
                {
                    table.Rows.Add(language.LID, language.LanguageName);
                }
            }

            return table;
        }

        [WebMethod(Description = "Adds a new language.")]
        public int InsertLanguage(string languageName)
        {
            return languageController.AddLanguage(languageName);
        }

        [WebMethod(Description = "Updates a language, found by LID.")]
        public int UpdateLanguage(int lid, string languageName)
        {
            return languageController.UpdateLanguage(lid, languageName);
        }

        [WebMethod(Description = "Deletes a language by LID.")]
        public int DeleteLanguage(int lid)
        {
            return languageController.DeleteLanguage(lid);
        }

        // ================= AUTHOR =================

        [WebMethod(Description = "Returns every author as a table.")]
        public DataTable GetAuthors()
        {
            DataTable table = CreateIdNameTable("Authors", "AID", "AuthorName");
            List<Author> authors = authorController.BrowseAuthors();

            if (authors != null)
            {
                foreach (Author author in authors)
                {
                    table.Rows.Add(author.AID, author.AuthorName);
                }
            }

            return table;
        }

        [WebMethod(Description = "Adds a new author.")]
        public int InsertAuthor(string authorName)
        {
            return authorController.AddAuthor(authorName);
        }

        [WebMethod(Description = "Updates an author, found by AID.")]
        public int UpdateAuthor(int aid, string authorName)
        {
            return authorController.UpdateAuthor(aid, authorName);
        }

        [WebMethod(Description = "Deletes an author by AID.")]
        public int DeleteAuthor(int aid)
        {
            return authorController.DeleteAuthor(aid);
        }
    }
}