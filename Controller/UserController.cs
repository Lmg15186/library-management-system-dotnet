using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    // CONTROLLER LAYER for users.
    // The forms are only allowed to call this class, and it passes the work down to
    // UserLogic. It is deliberately thin: no rules and no SQL live here.
    //
    //     Form -> Controller -> Logic -> DAO -> TableAdapter -> database
    //
    // It used to hold a UserDAO and convert the table itself, which skipped the
    // business layer completely. Now UserLogic is back in the chain.
    public class UserController
    {
        private UserLogic userLogic = new UserLogic();

        // Used by the user maintenance screen, which needs the whole list to show it.
        public List<User> BrowseUsers()
        {
            return userLogic.BrowseUsers();
        }

        // ---------- LOGIN ----------
        // Returns the matching user, or null when the credentials are wrong.
        // The signature did not change when the login was rewritten for the Assessment
        // 1 feedback, so FormLogin still calls it the same way.
        public User Login(string username, string password)
        {
            return userLogic.Authenticate(username, password);
        }

        // ---------- MAINTENANCE ----------
        // The user maintenance screen goes through these. They return the number of
        // affected rows so the form can confirm the action worked.

        public int AddUser(User user)
        {
            return userLogic.AddUser(user);
        }

        // Two parameters: the new values, and the row as the screen read it.
        public int UpdateUser(User updatedUser, User originalUser)
        {
            return userLogic.UpdateUser(updatedUser, originalUser);
        }

        public int DeleteUser(User originalUser)
        {
            return userLogic.DeleteUser(originalUser);
        }
    }
}