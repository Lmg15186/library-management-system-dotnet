using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{

    /// <summary>
    /// A model representing a system user.
    /// Mirrors the columns in the TabUser table: UID, UserName, Password, UserLevel.
    /// </summary>
    /// 
    // User data is stored in the database (the TabUser table), but in the database it
    // is just “rows and columns.” For the rest of the program to work with it
    // in an object-oriented way, I need a TEMPLATE in C# that represents
    // a user. This class is that template: a User object = a row in TabUser.
    //
    // Note that the database columns (UID, UserName, Password, UserLevel) become
    // properties here. The idea is to keep the C# code “mirroring” the database.

    public class User
    {
        // Private fields 
        // I make the data private (encapsulation) so that no one from outside
        // can directly access the variable. Access is done through the properties below.
        // This is good OOP practice: I protect the object's internal state.
        private int uId;
        private string userName;
        private string password;
        private int userLevel; //  access level

        // PUBLIC PROPERTIES (the “gateway” to the fields):
        // get => returns the value; set => assigns the value.
        // This is where UserLogic will populate each User when reading from the database.
        public int UId { get => uId; set => uId = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }

        // TYPE DECISION: Why is UserLevel an int and not a string or interface?
        // UserLevel represents a numeric permission level:
        //   1 = student, 2 = staff/sup, 3 = admin
        // Since these are numbers that I’ll compare later in the Login (if level == 1 ...),
        // the correct type is int. (In the old code, this was defined as an “interface,”
        // which was an error: “interface” is a reserved keyword and cannot be used to store
        // a value.)
        public int UserLevel { get => userLevel; set => userLevel = value; }
    }
}