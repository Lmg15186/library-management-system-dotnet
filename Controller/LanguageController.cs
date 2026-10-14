using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    // CONTROLLER LAYER for languages. The screen talks only to this class, which
    // forwards the calls to LanguageLogic. Same thin pattern as CategoryController:
    //
    //     FormLanguage -> LanguageController -> LanguageLogic -> LanguageDAO
    //                  -> TabLanguageTableAdapter -> database
    //
    // It holds no rules of its own - it just keeps the form from ever seeing the DAO
    // or the DataSet.
    public class LanguageController
    {
        private LanguageLogic languageLogic = new LanguageLogic();

        // Returns every language. Fills the maintenance grid, and later the language
        // drop-down on the book maintenance form.
        public List<Language> BrowseLanguages()
        {
            return languageLogic.BrowseLanguages();
        }

        // Adds a new language. Returns the number of rows inserted.
        public int AddLanguage(string languageName)
        {
            return languageLogic.AddLanguage(languageName);
        }

        // Updates an existing language. Returns the number of rows updated.
        public int UpdateLanguage(int lid, string languageName)
        {
            return languageLogic.UpdateLanguage(lid, languageName);
        }

        // Deletes a language. Returns the number of rows deleted.
        public int DeleteLanguage(int lid)
        {
            return languageLogic.DeleteLanguage(lid);
        }
    }
}