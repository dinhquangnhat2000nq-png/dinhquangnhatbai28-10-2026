
namespace bài4._1nagcap
{
    using bài4._1nagcap.Database;

    public partial class Form1 : Form
    {
        private readonly UserRepository userRepository = new();
        private bool databaseAvailable;

        public Form1()
        {
            InitializeComponent();
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            btnLogin.Click += btnLogin_Click;
            btnRegister.Click += btnRegister_Click;
            btnExit.Click += btnExit_Click;
            txtUsername.Validating += RequiredField_Validating;
            txtPassword.Validating += RequiredField_Validating;

            try
            {
                userRepository.EnsureDatabase();
                databaseAvailable = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Không thể kết nối SQL Server/LocalDB.\n\n{ex.Message}",
                    "Lỗi cơ sở dữ liệu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void RequiredField_Validating(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            if (sender is not TextBox textBox)
            {
                return;
            }

            string fieldName = textBox == txtUsername ? "Tên đăng nhập" : "Mật khẩu";
            errorProvider.SetError(textBox,
                string.IsNullOrWhiteSpace(textBox.Text) ? $"Vui lòng nhập {fieldName.ToLowerInvariant()}." : string.Empty);
        }

        private bool ValidateInput()
        {
            bool isValid = true;
            errorProvider.Clear();

            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                errorProvider.SetError(txtUsername, "Vui lòng nhập tên đăng nhập.");
                isValid = false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                errorProvider.SetError(txtPassword, "Vui lòng nhập mật khẩu.");
                isValid = false;
            }

            return isValid;
        }

        private void btnLogin_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput() || !databaseAvailable)
            {
                return;
            }

            try
            {
                if (userRepository.ValidateUser(txtUsername.Text.Trim(), txtPassword.Text))
                {
                    MessageBox.Show("Đăng nhập thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnRegister_Click(object? sender, EventArgs e)
        {
            if (!ValidateInput() || !databaseAvailable)
            {
                return;
            }

            try
            {
                string username = txtUsername.Text.Trim();
                if (userRepository.UserExists(username))
                {
                    errorProvider.SetError(txtUsername, "Tên đăng nhập đã tồn tại.");
                    return;
                }

                int userId = userRepository.CreateUser(username, txtPassword.Text);
                // Read the row back on a new connection. This both verifies that the
                // insert was committed and makes a stale Object Explorer grid obvious.
                if (!userRepository.UserExists(username))
                {
                    throw new InvalidOperationException("The user was inserted but could not be read back.");
                }

                MessageBox.Show(
                    $"Đăng ký tài khoản thành công (Id: {userId}).\n\n" +
                    $"Dữ liệu được lưu tại: {userRepository.GetDatabaseTarget()}\n\n" +
                    "Nếu SQL Server Object Explorer chưa hiển thị dòng này, hãy đóng tab [Data] và mở lại hoặc refresh bảng.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Clear();
                txtUsername.Focus();
            }
            catch (Exception ex)
            {
                ShowDatabaseError(ex);
            }
        }

        private void btnExit_Click(object? sender, EventArgs e)
        {
            Close();
        }

        private static void ShowDatabaseError(Exception ex)
        {
            MessageBox.Show(
                $"Thao tác với cơ sở dữ liệu thất bại.\n\n{ex.Message}",
                "Lỗi cơ sở dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
