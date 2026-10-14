using System;
using System.Windows.Forms;

namespace View
{
    // Main menu shown right after login. It adapts to who logged in:
    //   1 = Student -> Books only   2 = Staff / 3 = Admin -> full access
    public partial class FormMenu : Form
    {
        // Filled by the login screen. It replaces the Model.User this form used to
        // receive, because the interface project no longer references the Model.
        private LoggedUser loggedUser;

        // The staff buttons are built in code, so they are kept in fields. Without
        // this the Load event would have no way to hide them for a student.
        private Button btnLanguages;
        private Button btnAuthors;
        private Button btnBooksMaint;
        private Button btnReports;

        public FormMenu(LoggedUser user)
        {
            InitializeComponent();
            loggedUser = user;

            // The extra admin buttons are matched to the existing menu buttons, so
            // they fit in without editing the designer. Each is stacked below the
            // previous one, and Logout is pushed to the bottom.
            btnLanguages = MakeMenuButton("Languages", btnCategories.Location.Y + btnCategories.Height + 10);
            btnLanguages.Click += new EventHandler(this.btnLanguages_Click);

            btnAuthors = MakeMenuButton("Authors", btnLanguages.Location.Y + btnLanguages.Height + 10);
            btnAuthors.Click += new EventHandler(this.btnAuthors_Click);

            btnBooksMaint = MakeMenuButton("Books Maint.", btnAuthors.Location.Y + btnAuthors.Height + 10);
            btnBooksMaint.Click += new EventHandler(this.btnBooksMaint_Click);

            btnReports = MakeMenuButton("Reports", btnBooksMaint.Location.Y + btnBooksMaint.Height + 10);
            btnReports.Click += new EventHandler(this.btnReports_Click);

            // Move Logout below the last added button so nothing overlaps, and grow the
            // window so all the buttons fit inside it.
            btnLogout.Location = new System.Drawing.Point(
                btnReports.Location.X,
                btnReports.Location.Y + btnReports.Height + 10);

            this.Height = this.Height + (btnCategories.Height + 10) * 4;
        }

        // Helper that builds one menu button matching the look of btnCategories.
        private Button MakeMenuButton(string text, int y)
        {
            Button b = new Button();
            b.Text = text;
            b.Size = btnCategories.Size;
            b.Location = new System.Drawing.Point(btnCategories.Location.X, y);
            b.BackColor = btnCategories.BackColor;
            b.ForeColor = btnCategories.ForeColor;
            b.FlatStyle = btnCategories.FlatStyle;
            b.Font = btnCategories.Font;
            this.Controls.Add(b);
            return b;
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            string roleName = GetRoleName(loggedUser.UserLevel);
            lblWelcome.Text = "Welcome, " + loggedUser.UserName + "  (" + roleName + ")";

            // The brief splits the system in two: a student searches, reserves and
            // borrows books, while staff and managers also maintain the reference data
            // and read the reports. Level 2 and 3 are staff and administrator, so
            // anything below that only gets the Books screen.
            bool isStaff = (loggedUser.UserLevel >= 2);

            ShowStaffButtons(isStaff);

            if (loggedUser.UserLevel == 1)
            {
                this.Text = "Student Menu";
            }
            else if (isStaff)
            {
                this.Text = (loggedUser.UserLevel == 3) ? "Administrator Menu" : "Staff Menu";
            }
            else
            {
                // An unexpected level is treated as the lowest access, not the highest.
                this.Text = "Student Menu";
                MessageBox.Show("Unknown user level. Access restricted to student features.");
            }
        }

        // Shows or hides every staff-only button in one place, so a new button only has
        // to be added to this list to be covered by the rule.
        private void ShowStaffButtons(bool visible)
        {
            btnUsers.Visible = visible;
            btnCategories.Visible = visible;
            btnLanguages.Visible = visible;
            btnAuthors.Visible = visible;
            btnBooksMaint.Visible = visible;
            btnReports.Visible = visible;

            // Hiding a button does not move the ones below it, so for a student the
            // Logout would sit alone at the bottom with a large gap above it. Putting
            // it under Books and shrinking the window keeps the screen tidy.
            if (!visible)
            {
                btnLogout.Location = new System.Drawing.Point(
                    btnBooks.Location.X,
                    btnBooks.Location.Y + btnBooks.Height + 20);

                this.ClientSize = new System.Drawing.Size(
                    this.ClientSize.Width,
                    btnLogout.Location.Y + btnLogout.Height + 20);
            }
        }

        private string GetRoleName(int level)
        {
            switch (level)
            {
                case 1: return "Student";
                case 2: return "Staff";
                case 3: return "Administrator";
                default: return "Unknown";
            }
        }

        // Opens the Books search/browse screen. Every role can reach this one.
        // The logged user is passed in because borrow, reserve and return all need to
        // know which student is doing it.
        private void btnBooks_Click(object sender, EventArgs e)
        {
            FormBook formBook = new FormBook(loggedUser);
            formBook.Show();
        }

        // Opens the User maintenance screen (add / update / delete users).
        // It used to open FormBrowserUsers, which could only list them.
        private void btnUsers_Click(object sender, EventArgs e)
        {
            // Checked again here in case the button is visible for the wrong level.
            if (loggedUser.UserLevel < 2)
            {
                MessageBox.Show("You do not have permission to access user management.");
                return;
            }

            FormUserMaintenance formUsers = new FormUserMaintenance();
            formUsers.Show();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        // Opens the Category maintenance screen.
        private void btnCategories_Click(object sender, EventArgs e)
        {
            FormCategory formCategory = new FormCategory();
            formCategory.Show();
        }

        // Opens the Language maintenance screen.
        private void btnLanguages_Click(object sender, EventArgs e)
        {
            FormLanguage formLanguage = new FormLanguage();
            formLanguage.Show();
        }

        // Opens the Author maintenance screen.
        private void btnAuthors_Click(object sender, EventArgs e)
        {
            FormAuthor formAuthor = new FormAuthor();
            formAuthor.Show();
        }

        // Opens the Book maintenance screen (add/update/delete books, with drop-downs).
        private void btnBooksMaint_Click(object sender, EventArgs e)
        {
            FormBookMaintenance formBookMaint = new FormBookMaintenance();
            formBookMaint.Show();
        }

        // Opens the reports screen (borrowed, overdue + late fee, reservations).
        private void btnReports_Click(object sender, EventArgs e)
        {
            FormReports formReports = new FormReports();
            formReports.Show();
        }
    }
}