namespace bài4._2_nangcap
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private Label lblTitle;
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblPhone;
        private MaskedTextBox mtxtPhone;
        private Label lblBirthDate;
        private DateTimePicker dtpBirthDate;
        private Label lblCourse;
        private ComboBox cboCourse;
        private Label lblGender;
        private RadioButton rdoMale;
        private RadioButton rdoFemale;
        private CheckBox chkGraduated;
        private Button btnRegister;
        private Button btnViewRegistrations;

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
            lblFullName = new Label();
            txtFullName = new TextBox();
            lblPhone = new Label();
            mtxtPhone = new MaskedTextBox();
            lblBirthDate = new Label();
            dtpBirthDate = new DateTimePicker();
            lblCourse = new Label();
            cboCourse = new ComboBox();
            lblGender = new Label();
            rdoMale = new RadioButton();
            rdoFemale = new RadioButton();
            chkGraduated = new CheckBox();
            btnRegister = new Button();
            btnViewRegistrations = new Button();
            SuspendLayout();
            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(205, 25);
            lblTitle.Text = "ĐĂNG KÝ KHÓA HỌC";
            // lblFullName
            lblFullName.AutoSize = true;
            lblFullName.Location = new Point(75, 90);
            lblFullName.Text = "Họ và tên:";
            // txtFullName
            txtFullName.Location = new Point(205, 87);
            txtFullName.Size = new Size(350, 27);
            // lblPhone
            lblPhone.AutoSize = true;
            lblPhone.Location = new Point(75, 135);
            lblPhone.Text = "Số điện thoại:";
            // mtxtPhone
            mtxtPhone.Location = new Point(205, 132);
            mtxtPhone.Size = new Size(150, 27);
            mtxtPhone.Mask = "(000) 000-0000";
            mtxtPhone.PromptChar = '_';
            // lblBirthDate
            lblBirthDate.AutoSize = true;
            lblBirthDate.Location = new Point(75, 180);
            lblBirthDate.Text = "Ngày sinh:";
            // dtpBirthDate
            dtpBirthDate.Format = DateTimePickerFormat.Custom;
            dtpBirthDate.CustomFormat = "dd/MM/yyyy";
            dtpBirthDate.Location = new Point(205, 177);
            dtpBirthDate.Size = new Size(150, 27);
            // lblCourse
            lblCourse.AutoSize = true;
            lblCourse.Location = new Point(75, 225);
            lblCourse.Text = "Khóa học:";
            // cboCourse
            cboCourse.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCourse.FormattingEnabled = true;
            cboCourse.Location = new Point(205, 222);
            cboCourse.Size = new Size(350, 28);
            // lblGender
            lblGender.AutoSize = true;
            lblGender.Location = new Point(75, 270);
            lblGender.Text = "Giới tính:";
            // rdoMale
            rdoMale.AutoSize = true;
            rdoMale.Checked = true;
            rdoMale.Location = new Point(205, 268);
            rdoMale.Text = "Nam";
            // rdoFemale
            rdoFemale.AutoSize = true;
            rdoFemale.Location = new Point(285, 268);
            rdoFemale.Text = "Nữ";
            // chkGraduated
            chkGraduated.AutoSize = true;
            chkGraduated.Location = new Point(205, 310);
            chkGraduated.Text = "Đã tốt nghiệp";
            // btnRegister
            btnRegister.Location = new Point(205, 355);
            btnRegister.Size = new Size(150, 38);
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // btnViewRegistrations
            btnViewRegistrations.Location = new Point(380, 355);
            btnViewRegistrations.Size = new Size(175, 38);
            btnViewRegistrations.Text = "Xem đăng ký";
            btnViewRegistrations.UseVisualStyleBackColor = true;
            btnViewRegistrations.Click += btnViewRegistrations_Click;
            // Form1
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 440);
            Controls.Add(btnRegister);
            Controls.Add(btnViewRegistrations);
            Controls.Add(chkGraduated);
            Controls.Add(rdoFemale);
            Controls.Add(rdoMale);
            Controls.Add(lblGender);
            Controls.Add(cboCourse);
            Controls.Add(lblCourse);
            Controls.Add(dtpBirthDate);
            Controls.Add(lblBirthDate);
            Controls.Add(mtxtPhone);
            Controls.Add(lblPhone);
            Controls.Add(txtFullName);
            Controls.Add(lblFullName);
            Controls.Add(lblTitle);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng ký khóa học";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
