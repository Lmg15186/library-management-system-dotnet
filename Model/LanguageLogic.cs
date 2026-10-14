using System;
using System.Collections.Generic;
using System.Data;  // I need this to use DataRow in the foreach loop
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // BUSINESS LOGIC LAYER for languages.
    // The DAO gives me the raw data (a DataTable). The rest of the program wants
    // Language objects, so this layer translates the table into a List<Language>.
    // It is also where the rules about languages live: what counts as a valid name,
    // and what it means when a command changes no rows. Same shape as CategoryLogic.
    public class LanguageLogic
    {
        // The logic calls the DAO. Note the "chain": Logic -> DAO -> database.
        private LanguageDAO languageDAO = new LanguageDAO();

        // -------- READ --------

        // Returns every language as a list of objects.
        // Returns null when the table is empty, the same convention the other logic
        // classes use, so every screen checks for "nothing found" the same way.
        public List<Language> BrowseLanguages()
        {
            // STEP 1: request the raw data from the data layer.
            DataSetLibrarySystem.TabLanguageDataTable table = languageDAO.BrowseLanguages();

            // STEP 2: decide what "no data" means before doing any work.
            if (table == null || table.Count == 0)
            {
                return null;
            }

            // STEP 3: create the empty list that I will populate.
            List<Language> languages = new List<Language>();

            // STEP 4: iterate through EVERY row and construct a Language.
            // I loop over the typed TabLanguageRow rather than a plain DataRow, so the
            // columns come back already typed (row.LID is an int) and a misspelled
            // column name is caught by the compiler instead of at run time.
            foreach (DataSetLibrarySystem.TabLanguageRow row in table.Rows)
            {
                Language language = new Language
                {
                    LID = row.LID,
                    LanguageName = row.LanguageName
                };
                languages.Add(language);
            }

            // STEP 5: return the completed list to the caller (the Controller).
            return languages;
        }

        // -------- WRITE --------

        // Adds a new language. Returns the number of rows inserted.
        // The validation lives here because "a language must have a name" is true no
        // matter which screen is asking.
        public int AddLanguage(string languageName)
        {
            ValidateName(languageName);

            // Trim() removes accidental leading/trailing spaces, so "English" and
            // "English " are not stored as two invisible duplicates.
            return languageDAO.Insert(languageName.Trim());
        }

        // Updates an existing language. Returns the number of rows updated.
        public int UpdateLanguage(int lid, string languageName)
        {
            ValidateName(languageName);

            // An LID of zero or less means no row was selected in the grid.
            if (lid <= 0)
            {
                throw new ArgumentException("Please select a language from the list first.");
            }

            return languageDAO.Update(languageName.Trim(), lid);
        }

        // Deletes a language. Returns the number of rows deleted.
        //
        // Like CategoryLogic, this does not check whether any books use the language.
        // TabBook has a foreign key pointing at TabLanguage, so if a book still refers
        // to this language the database itself refuses the delete and throws. The form
        // catches that and explains it. Letting the database enforce the rule it
        // already knows about avoids a second copy of the same check drifting apart.
        public int DeleteLanguage(int lid)
        {
            if (lid <= 0)
            {
                throw new ArgumentException("Please select a language from the list first.");
            }

            return languageDAO.Delete(lid);
        }

        // -------- Private helper: shared validation --------
        // Add and Update enforce identical rules about the name, so the checks live in
        // one private method rather than being written twice.
        private void ValidateName(string languageName)
        {
            // IsNullOrWhiteSpace catches null, empty, and spaces-only at once.
            if (string.IsNullOrWhiteSpace(languageName))
            {
                throw new ArgumentException("Language name is required.");
            }

            // TabLanguage.LanguageName is varchar(50) in the database. Checking the
            // length here gives the user a clear message instead of a truncation error.
            if (languageName.Trim().Length > 50)
            {
                throw new ArgumentException("Language name cannot be longer than 50 characters.");
            }
        }
    }
}