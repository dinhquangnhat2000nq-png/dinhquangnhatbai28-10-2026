namespace bài4._1nagcap
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblUsername;
        private Label lblPassword;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private Button btnLogin;
        private Button btnRegister;
        private Button btnExit;
        private ErrorProvider errorProvider;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            lblUsername = new Label();
            lblPassword = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            btnLogin = new Button();
            btnRegister = new Button();
            btnExit = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();

            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.Location = new Point(126, 35);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(220, 32);
            lblTitle.Text = "ĐĂNG NHẬP";

            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(75, 105);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(89, 15);
            lblUsername.Text = "Tên đăng nhập:";

            txtUsername.Location = new Point(190, 101);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(220, 23);
            txtUsername.TabIndex = 0;

            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(75, 145);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(59, 15);
            lblPassword.Text = "Mật khẩu:";

            txtPassword.Location = new Point(190, 141);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(220, 23);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;

            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(190, 175);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(124, 19);
            chkShowPassword.TabIndex = 2;
            chkShowPassword.Text = "Hiển thị mật khẩu";
            chkShowPassword.UseVisualStyleBackColor = true;

            btnLogin.Location = new Point(75, 225);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(100, 32);
            btnLogin.TabIndex = 3;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = true;

            btnRegister.Location = new Point(185, 225);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(100, 32);
            btnRegister.TabIndex = 4;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;

            btnExit.DialogResult = DialogResult.Cancel;
            btnExit.Location = new Point(295, 225);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(100, 32);
            btnExit.TabIndex = 5;
            btnExit.Text = "Thoát";
            btnExit.UseVisualStyleBackColor = true;

            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;

            AutoScaleMode = AutoScaleMode.Font;
            AcceptButton = btnLogin;
            CancelButton = btnExit;
            ClientSize = new Size(480, 310);
            Controls.Add(lblTitle);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(btnLogin);
            Controls.Add(btnRegister);
            Controls.Add(btnExit);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
