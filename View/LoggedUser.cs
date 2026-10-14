using System;

namespace View
{
    // Carries who is logged in between the screens.
    //
    // It replaces the Model.User the login used to pass to FormMenu, because the
    // interface project cannot reference the Model any more. The password is not kept:
    // once the service has confirmed the login, nothing else needs it.
    public class LoggedUser
    {
        private int uid;
        private string userName;
        private int userLevel;

        // 1 = Student, 2 = Staff, 3 = Administrator.
        public int UID { get => uid; set => uid = value; }
        public string UserName { get => userName; set => userName = value; }
        public int UserLevel { get => userLevel; set => userLevel = value; }
    }
}