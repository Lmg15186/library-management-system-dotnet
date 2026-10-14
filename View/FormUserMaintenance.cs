using System;
using System.Data;
using System.Windows.Forms;

namespace View
{
    // USER MAINTENANCE SCREEN.
    // Lets staff and administrators add, update and delete users, and lists them in
    // the grid. It talks only to the UserService, never to a controller or a DAO.
    public partial class FormUserMaintenance : Form
    {
        private UserServiceRef.UserService userService = new UserServiceRef.UserService();

        // UID of the row clicked in the grid. Update and Delete need it to know which
        // record to change, and 0 means nothing is selected (no user has UID 0).
        private int selectedUid = 0;

        public FormUserMaintenance()
        {
            InitializeComponent();
        }

        private void FormUserMaintenance_Load(object sender, EventArgs e)
        {
            LoadUserLevels();
            LoadUsers();
        }

        // The three levels are fixed by the system, so they are not read from a table.
        // A drop-down also stops an invalid level being typed.
        private void LoadUserLevels()
        {
            cmbUserLevel.Items.Clear();
            cmbUserLevel.Items.Add("1 - Student");
            cmbUserLevel.Items.Add("2 - Staff");
            cmbUserLevel.Items.Add("3 - Administrator");
            cmbUserLevel.SelectedIndex = -1;
        }

        // The service returns a DataTable, so the grid is bound to it directly.
        private void LoadUsers()
        {
            try
            {
                dgvUsers.DataSource = null;
                dgvUsers.DataSource = userService.GetUsers();

                // Passwords are stored as hashes and never shown.
                if (dgvUsers.Columns.Contains("Password"))
                {
                    dgvUsers.Columns["Password"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading users: " + CleanMessage(ex));
            }
        }

        // Copies the clicked row into the edit fields.
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // RowIndex is -1 when the column header is clicked.
            if (e.RowIndex < 0)
            {
                return;
            }

            // The grid is bound to a table now, so the values are read by column name
            // instead of from a User object.
            DataGridViewRow row = dgvUsers.Rows[e.RowIndex];

            selectedUid = Convert.ToInt32(row.Cells["UID"].Value);

            txtUID.Text = selectedUid.ToString();
            txtUserName.Text = row.Cells["UserName"].Value.ToString();
            // The current password is never shown. Leave it blank to keep it, or type
            // a new one to change it.
            txtPassword.Clear();

            // The list is in level order, so level 1 sits at index 0.
            cmbUserLevel.SelectedIndex = Convert.ToInt32(row.Cells["UserLevel"].Value) - 1;
        }

        // SelectedIndex is -1 when nothing is chosen, so this returns 0 and the service
        // rejects it with a proper message.
        private int ReadUserLevel()
        {
            return cmbUserLevel.SelectedIndex + 1;
        }

        // ---------- ADD ----------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // No UID is sent: it is an identity column.
                int rows = userService.InsertUser(txtUserName.Text.Trim(),
                                                  txtPassword.Text.Trim(),
                                                  ReadUserLevel());

                if (rows > 0)
                {
                    MessageBox.Show("User added successfully.");
                    ClearFields();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(CleanMessage(ex));
            }
        }

        // ---------- UPDATE ----------
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedUid == 0)
                {
                    MessageBox.Show("Please select a user from the list first.");
                    return;
                }

                int rows = userService.UpdateUser(selectedUid,
                                                  txtUserName.Text.Trim(),
                                                  txtPassword.Text.Trim(),
                                                  ReadUserLevel());

                if (rows > 0)
                {
                    MessageBox.Show("User updated successfully.");
                    ClearFields();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(CleanMessage(ex));
            }
        }

        // ---------- DELETE ----------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedUid == 0)
                {
                    MessageBox.Show("Please select a user from the list first.");
                    return;
                }

                // Deleting cannot be undone, so the operator confirms first. This is an
                // interface decision, so it stays in the form.
                DialogResult answer = MessageBox.Show(
                    "Are you sure you want to delete the user '" + txtUserName.Text + "'?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (answer == DialogResult.No)
                {
                    return;
                }

                int rows = userService.DeleteUser(selectedUid);

                if (rows > 0)
                {
                    MessageBox.Show("User deleted successfully.");
                    ClearFields();
                    LoadUsers();
                }
            }
            catch (Exception ex)
            {
                // A user with borrow or reserve records is protected by the foreign
                // keys, so the database refuses the delete.
                MessageBox.Show(
                    "This user cannot be deleted because they still have borrow or reserve records.\n\n"
                    + "Technical detail: " + CleanMessage(ex));
            }
        }

        // ---------- CLEAR ----------
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        // Empties the fields and forgets the selected row, so the next Add does not
        // reuse the last user that was clicked.
        private void ClearFields()
        {
            selectedUid = 0;
            txtUID.Clear();
            txtUserName.Clear();
            txtPassword.Clear();
            cmbUserLevel.SelectedIndex = -1;
            txtUserName.Focus();
        }

        // A rule broken inside the service comes back wrapped by SOAP, carrying the
        // exception type and the whole stack trace with it. The operator only needs
        // the sentence, so this keeps the text after the last arrow, then the first
        // line, then drops the "System.ArgumentException: " in front of it.
        private string CleanMessage(Exception ex)
        {
            string message = ex.Message;

            int arrow = message.LastIndexOf("---> ");
            if (arrow >= 0)
            {
                message = message.Substring(arrow + 5);
            }

            // Everything after the first line break is the stack trace.
            int lineBreak = message.IndexOf('\n');
            if (lineBreak >= 0)
            {
                message = message.Substring(0, lineBreak);
            }

            // The type name is only there because .NET puts it in front of the text.
            if (message.StartsWith("System."))
            {
                int colon = message.IndexOf(": ");
                if (colon >= 0)
                {
                    message = message.Substring(colon + 2);
                }
            }

            return message.Trim();
        }
    }
}