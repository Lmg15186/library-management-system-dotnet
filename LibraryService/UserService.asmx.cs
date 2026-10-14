using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using Controller;
using Model;

namespace LibraryService
{
    // WEB SERVICE for users.
    //
    // This is the layer that replaces the direct reference the Windows application
    // used to have to the Controller. The form now calls a method over HTTP instead
    // of calling a class in another project.
    //
    // A web service can only send back basic types (int, string, decimal) or a
    // DataTable - it cannot send a User object, because the client has no way to know
    // what a User is. So every method here takes basic types in, calls the same
    // UserController the form used to call, and converts the List<User> that comes
    // back into a DataTable before returning it.
    //
    // The chain is now:
    //     Form -> Web Service -> Controller -> Logic -> DAO -> database
    [WebService(Namespace = "http://ait.edu.au/library/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class UserService : System.Web.Services.WebService
    {
        private UserController userController = new UserController();

        // Builds the table that travels back to the client. The column names are the
        // same as in TabUser, so the grid on the form looks exactly as it did before.
        private DataTable CreateUserTable()
        {
            DataTable table = new DataTable("Users");
            table.Columns.Add("UID", typeof(int));
            table.Columns.Add("UserName", typeof(string));
            table.Columns.Add("Password", typeof(string));
            table.Columns.Add("UserLevel", typeof(int));
            return table;
        }

        // Turns one User object into one row of that table.
        private void AddUserRow(DataTable table, User user)
        {
            DataRow row = table.NewRow();
            row["UID"] = user.UId;
            row["UserName"] = user.UserName;
            // The hash stays on the server; the client never needs it.
            row["Password"] = string.Empty;
            row["UserLevel"] = user.UserLevel;
            table.Rows.Add(row);
        }

        // ---------- SELECT ----------
        [WebMethod(Description = "Returns every user as a table.")]
        public DataTable GetUsers()
        {
            DataTable table = CreateUserTable();
            List<User> users = userController.BrowseUsers();

            // BrowseUsers returns null when there are no users. An empty table is a
            // better answer for the client than a null, because the grid can bind to
            // it without an extra check.
            if (users != null)
            {
                foreach (User user in users)
                {
                    AddUserRow(table, user);
                }
            }

            return table;
        }

        // Authenticates a user. Returns a table with one row when the credentials are
        // valid, and an empty table when they are not, so the login form can just look
        // at the row count.
        [WebMethod(Description = "Checks a username and password.")]
        public DataTable Login(string username, string password)
        {
            DataTable table = CreateUserTable();
            User user = userController.Login(username, password);

            if (user != null)
            {
                AddUserRow(table, user);
            }

            return table;
        }

        // ---------- INSERT ----------
        // No UID is received because it is an identity column in the database.
        [WebMethod(Description = "Adds a new user.")]
        public int InsertUser(string userName, string password, int userLevel)
        {
            // The User object is built here, inside the service. The object itself
            // never crosses the network - only the three basic values did.
            User user = new User
            {
                UserName = userName,
                Password = password,
                UserLevel = userLevel
            };

            return userController.AddUser(user);
        }

        // ---------- UPDATE ----------
        [WebMethod(Description = "Updates an existing user, found by UID.")]
        public int UpdateUser(int uid, string userName, string password, int userLevel)
        {
            User updatedUser = new User
            {
                UId = uid,
                UserName = userName,
                Password = password,
                UserLevel = userLevel
            };

            // UserLogic needs the record that was selected as well, and what it uses
            // from it is the UID that identifies the row.
            User originalUser = new User { UId = uid };

            return userController.UpdateUser(updatedUser, originalUser);
        }

        // ---------- DELETE ----------
        [WebMethod(Description = "Deletes a user by UID.")]
        public int DeleteUser(int uid)
        {
            User originalUser = new User { UId = uid };

            return userController.DeleteUser(originalUser);
        }
    }
}