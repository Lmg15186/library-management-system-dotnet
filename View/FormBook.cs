using System;
using System.Data;
using System.Windows.Forms;

namespace View
{
    // BOOK SCREEN. Browse and search books (name, author and category), and the
    // student actions from the brief: borrow, reserve and return.
    // It calls BookService for the searches, MasterService for the category list and
    // LoanService for the loans.
    public partial class FormBook : Form
    {
        private BookServiceRef.BookService bookService = new BookServiceRef.BookService();
        private MasterServiceRef.MasterService masterService = new MasterServiceRef.MasterService();
        private LoanServiceRef.LoanService loanService = new LoanServiceRef.LoanService();

        // Who is logged in. Borrow, reserve and return all need the UID.
        private LoggedUser loggedUser;

        public FormBook(LoggedUser user)
        {
            InitializeComponent();
            loggedUser = user;
        }

        private void FormBook_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadBorrowedBooks();
        }

        // The category drop-down shows CategoryName and keeps CID behind each line.
        private void LoadCategories()
        {
            try
            {
                cmbCategory.DataSource = masterService.GetCategories();
                cmbCategory.ValueMember = "CID";
                cmbCategory.DisplayMember = "CategoryName";
                cmbCategory.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading categories: " + CleanMessage(ex));
            }
        }

        // Fills the return drop-down with the books this user still has out. Called on
        // load and after every borrow or return, so the list stays correct.
        private void LoadBorrowedBooks()
        {
            try
            {
                cmbBorrowed.DataSource = null;

                DataTable borrowed = loanService.GetActiveBorrows(loggedUser.UID);

                cmbBorrowed.DataSource = borrowed;
                cmbBorrowed.ValueMember = "BID";
                cmbBorrowed.DisplayMember = "BookName";

                // Nothing on loan is a normal state, so the drop-down is simply empty.
                btnReturn.Enabled = (borrowed.Rows.Count > 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading your borrowed books: " + CleanMessage(ex));
            }
        }

        // Reads the ISBN of the book selected in the grid, or an empty string when no
        // row is selected. Borrow and Reserve both need it.
        private string GetSelectedIsbn()
        {
            if (dgvBooks.CurrentRow == null || dgvBooks.CurrentRow.Cells["ISBN"].Value == null)
            {
                return "";
            }

            return dgvBooks.CurrentRow.Cells["ISBN"].Value.ToString();
        }

        // Same for the title, only used in the confirmation messages.
        private string GetSelectedBookName()
        {
            if (dgvBooks.CurrentRow == null || dgvBooks.CurrentRow.Cells["BookName"].Value == null)
            {
                return "";
            }

            return dgvBooks.CurrentRow.Cells["BookName"].Value.ToString();
        }

        // ---------- BROWSE: show every book ----------
        private void btnBrowseAll_Click(object sender, EventArgs e)
        {
            try
            {
                dgvBooks.DataSource = null;
                dgvBooks.DataSource = bookService.GetBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + CleanMessage(ex));
            }
        }

        // ---------- SEARCH CRITERIA 1: by book name ----------
        private void btnSearchName_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtBookName.Text))
                {
                    MessageBox.Show("Please type a book name to search.");
                    return;
                }

                dgvBooks.DataSource = null;
                dgvBooks.DataSource = bookService.SearchByBookName(txtBookName.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching by name: " + CleanMessage(ex));
            }
        }

        // ---------- SEARCH CRITERIA 2: by author ----------
        private void btnSearchAuthor_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtAuthor.Text))
                {
                    MessageBox.Show("Please type an author name to search.");
                    return;
                }

                dgvBooks.DataSource = null;
                dgvBooks.DataSource = bookService.SearchByAuthor(txtAuthor.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching by author: " + CleanMessage(ex));
            }
        }

        // ---------- SEARCH CRITERIA 3: by category ----------
        private void btnSearchCategory_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCategory.SelectedIndex < 0)
                {
                    MessageBox.Show("Please select a category to search.");
                    return;
                }

                // The combo is bound to a table, so the visible text is the category
                // name that the search needs.
                dgvBooks.DataSource = null;
                dgvBooks.DataSource = bookService.SearchByCategory(cmbCategory.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error searching by category: " + CleanMessage(ex));
            }
        }

        // ---------- BORROW ----------
        private void btnBorrow_Click(object sender, EventArgs e)
        {
            try
            {
                string isbn = GetSelectedIsbn();

                if (isbn == "")
                {
                    MessageBox.Show("Please select a book from the list first.");
                    return;
                }

                // The service decides whether this is allowed: not already borrowed,
                // and not reserved by somebody else.
                int rows = loanService.BorrowBook(loggedUser.UID, isbn);

                if (rows > 0)
                {
                    MessageBox.Show("Book borrowed successfully: " + GetSelectedBookName());
                    LoadBorrowedBooks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(CleanMessage(ex));
            }
        }

        // ---------- RESERVE ----------
        private void btnReserve_Click(object sender, EventArgs e)
        {
            try
            {
                string isbn = GetSelectedIsbn();

                if (isbn == "")
                {
                    MessageBox.Show("Please select a book from the list first.");
                    return;
                }

                int rows = loanService.ReserveBook(loggedUser.UID, isbn);

                if (rows > 0)
                {
                    MessageBox.Show("Book reserved successfully: " + GetSelectedBookName());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(CleanMessage(ex));
            }
        }

        // ---------- RETURN ----------
        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbBorrowed.SelectedValue == null)
                {
                    MessageBox.Show("Please select one of your borrowed books.");
                    return;
                }

                // Only the BID is sent, because a Borrow object cannot cross the
                // service. The fee comes back so it can be shown here.
                int bid = Convert.ToInt32(cmbBorrowed.SelectedValue);
                decimal lateFee = loanService.ReturnBook(loggedUser.UID, bid);

                if (lateFee > 0)
                {
                    MessageBox.Show("Book returned late. Late fee: $" + lateFee.ToString("0.00"));
                }
                else
                {
                    MessageBox.Show("Book returned on time. No late fee.");
                }

                LoadBorrowedBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show(CleanMessage(ex));
            }
        }

        // A rule broken inside the service comes back wrapped by SOAP, with the
        // exception type and the stack trace attached. This keeps only the sentence.
        private string CleanMessage(Exception ex)
        {
            string message = ex.Message;

            int arrow = message.LastIndexOf("---> ");
            if (arrow >= 0)
            {
                message = message.Substring(arrow + 5);
            }

            // The rest is the stack trace.
            int lineBreak = message.IndexOf('\n');
            if (lineBreak >= 0)
            {
                message = message.Substring(0, lineBreak);
            }

            // Drop the type name .NET puts in front.
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