using Model.DataSetLibrarySystemTableAdapters;
using System;
using System.Collections.Generic;
using System.Data;  // needed for DataRow
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    // BUSINESS LOGIC LAYER for users.
    // It takes the raw table from the DAO and turns it into User objects, and it holds
    // the rules: validation, duplicated usernames, and what an empty result means.
    // The chain is Form -> Controller -> Logic -> DAO -> database.
    public class UserLogic
    {
        private UserDAO userDAO = new UserDAO();

        // Limits copied from the TabUser columns: UserName is varchar(8) and Password
        // is varchar(30). Keeping them as constants means the message the user sees
        // matches what the database actually accepts.
        private const int UserNameMaxLength = 8;
        private const int PasswordMaxLength = 30;
        private const int LowestUserLevel = 1;   // Student
        private const int HighestUserLevel = 3;  // Administrator

        // Returns all users as objects, or null when the table is empty.
        public List<User> BrowseUsers()
        {
            DataSetLibrarySystem.TabUserDataTable tabUserDataTable = userDAO.BrowseUsers();

            // "No data" is a business rule, so it is decided here and not in the form.
            if (tabUserDataTable.Count == 0)
            {
                return null;
            }

            List<User> users = new List<User>();

            // Each row becomes one User. The cast is needed because row["..."] comes
            // back as object.
            foreach (DataRow row in tabUserDataTable.Rows)
            {
                User user = new User
                {
                    UId = (int)row["UId"],
                    UserName = row["UserName"].ToString(),
                    Password = row["Password"].ToString(),
                    UserLevel = (int)row["UserLevel"]
                };
                users.Add(user);
            }

            return users;
        }

        // Returns the user with these credentials, or null when they are wrong.
        //
        // This is where the Assessment 1 feedback lands. The old login loaded every
        // user and searched the list with a foreach. Now the DAO runs a query with a
        // WHERE clause and at most one row comes back, so there is no loop here.
        public User Authenticate(string username, string password)
        {
            // The database only holds hashes, so the typed password is hashed first and
            // the WHERE clause compares hash with hash.
            DataSetLibrarySystem.TabUserDataTable tabUserDataTable =
                userDAO.FindByCredentials(username, PasswordHasher.Hash(password ?? ""));

            // To the database zero rows is just an empty answer. To the system it means
            // the credentials are invalid, and deciding that is a business rule.
            if (tabUserDataTable.Count == 0)
            {
                return null;
            }

            // Row zero is safe because UserName is unique, so the WHERE can only match
            // one record.
            DataRow row = tabUserDataTable.Rows[0];

            User user = new User
            {
                UId = (int)row["UId"],
                UserName = row["UserName"].ToString(),
                Password = row["Password"].ToString(),
                UserLevel = (int)row["UserLevel"]
            };

            return user;
        }

        // ================= MAINTENANCE =================

        // Checks the values before anything reaches the database. It throws instead of
        // returning false so the form can just show the message.
        // passwordRequired is false on Update, where a blank password means "keep the
        // current one".
        private void Validate(User user, bool passwordRequired = true)
        {
            if (user == null)
            {
                throw new ArgumentException("No user data was supplied.");
            }

            if (string.IsNullOrWhiteSpace(user.UserName))
            {
                throw new ArgumentException("The username is required.");
            }

            if (user.UserName.Length > UserNameMaxLength)
            {
                throw new ArgumentException(
                    "The username cannot be longer than " + UserNameMaxLength + " characters.");
            }

            if (passwordRequired && string.IsNullOrWhiteSpace(user.Password))
            {
                throw new ArgumentException("The password is required.");
            }

            if (!string.IsNullOrEmpty(user.Password) && user.Password.Length > PasswordMaxLength)
            {
                throw new ArgumentException(
                    "The password cannot be longer than " + PasswordMaxLength + " characters.");
            }

            if (user.UserLevel < LowestUserLevel || user.UserLevel > HighestUserLevel)
            {
                throw new ArgumentException(
                    "Please select a user level (1 = Student, 2 = Staff, 3 = Administrator).");
            }
        }

        public int AddUser(User user)
        {
            Validate(user);

            // Two users cannot share a username, because that is what they log in with.
            if (UserNameAlreadyExists(user.UserName, 0))
            {
                throw new ArgumentException("That username is already taken.");
            }

            // Only the hash is saved, never the password itself.
            return userDAO.Insert(user.UserName, PasswordHasher.Hash(user.Password), user.UserLevel);
        }

        // Takes the typed values and the row that was selected in the grid. The
        // second one is only needed for its UID, which says which record to change.
        public int UpdateUser(User updatedUser, User originalUser)
        {
            Validate(updatedUser, false);

            if (originalUser == null)
            {
                throw new ArgumentException("Please select a user from the list first.");
            }

            // Ignoring the UID being edited, otherwise a user clashes with themselves.
            if (UserNameAlreadyExists(updatedUser.UserName, originalUser.UId))
            {
                throw new ArgumentException("That username is already taken.");
            }

            // A blank password keeps the current hash; a new one is hashed before saving.
            string passwordHash = string.IsNullOrWhiteSpace(updatedUser.Password)
                ? GetCurrentPasswordHash(originalUser.UId)
                : PasswordHasher.Hash(updatedUser.Password);

            int rows = userDAO.Update(updatedUser.UserName, passwordHash,
                                      updatedUser.UserLevel, originalUser.UId);

            // 0 rows means no record with that UID was found, so it was already deleted.
            if (rows == 0)
            {
                throw new Exception(
                    "This user no longer exists. Please refresh the list and try again.");
            }

            return rows;
        }

        public int DeleteUser(User originalUser)
        {
            if (originalUser == null)
            {
                throw new ArgumentException("Please select a user from the list first.");
            }

            int rows = userDAO.Delete(originalUser.UId);

            if (rows == 0)
            {
                throw new Exception(
                    "This user no longer exists. Please refresh the list and try again.");
            }

            return rows;
        }

        // Returns the hash already stored for this user, so an update without a new
        // password does not change it.
        private string GetCurrentPasswordHash(int uid)
        {
            List<User> users = BrowseUsers();

            if (users != null)
            {
                foreach (User existing in users)
                {
                    if (existing.UId == uid)
                    {
                        return existing.Password;
                    }
                }
            }

            throw new Exception("This user no longer exists. Please refresh the list and try again.");
        }

        // Looks for the same username in the existing users. ignoreUid skips the record
        // being edited; Add passes 0, because no user has UID 0.
        private bool UserNameAlreadyExists(string userName, int ignoreUid)
        {
            List<User> users = BrowseUsers();

            if (users == null)
            {
                return false;
            }

            foreach (User existing in users)
            {
                if (existing.UId != ignoreUid
                    && string.Equals(existing.UserName, userName,
                                     StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}