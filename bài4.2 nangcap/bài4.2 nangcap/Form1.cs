using System.Data;

namespace bài4._2_nangcap
{
    public partial class Form1 : Form
    {
        private readonly List<CourseItem> courses =
        [
            new("Lập trình C# cơ bản", "CSHARP-01"),
            new("Lập trình Windows Forms", "WINFORM-02"),
            new("Lập trình ASP.NET Core", "ASPNET-03")
        ];

        public Form1()
        {
            InitializeComponent();
            cboCourse.DataSource = courses;
            cboCourse.DisplayMember = nameof(CourseItem.Name);
            cboCourse.ValueMember = nameof(CourseItem.Code);
        }

        private void btnViewRegistrations_Click(object? sender, EventArgs e)
        {
            try
            {
                EnsureDatabaseAndTable();
                using var connection = new Microsoft.Data.SqlClient.SqlConnection(GetDatabaseConnectionString());
                connection.Open();
                using var command = connection.CreateCommand();
                command.CommandText = "SELECT Id, FullName, Phone, BirthDate, CourseCode, CourseName, Gender, IsGraduated, RegisteredAt FROM dbo.CourseRegistrations ORDER BY RegisteredAt DESC, Id DESC;";
                using var adapter = new Microsoft.Data.SqlClient.SqlDataAdapter(command);
                var registrations = new DataTable();
                adapter.Fill(registrations);

                using var viewer = new Form
                {
                    Text = "Danh sách đăng ký khóa học",
                    StartPosition = FormStartPosition.CenterParent,
                    Size = new Size(1000, 450),
                    MinimumSize = new Size(700, 300)
                };
                var grid = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                    DataSource = registrations
                };
                viewer.Controls.Add(grid);
                viewer.ShowDialog(this);
            }
            catch (Exception ex) when (ex is Microsoft.Data.SqlClient.SqlException or InvalidOperationException)
            {
                MessageBox.Show(
                    $"Không thể tải danh sách đăng ký từ SQL Server LocalDB.\n\nChi tiết: {ex.Message}",
                    "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return;
            }

            if (!mtxtPhone.MaskCompleted)
            {
                MessageBox.Show("Vui lòng nhập đủ số điện thoại.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtxtPhone.Focus();
                return;
            }

            if (cboCourse.SelectedItem is not CourseItem selectedCourse)
            {
                MessageBox.Show("Vui lòng chọn khóa học.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var registration = new RegistrationData(
                txtFullName.Text.Trim(),
                mtxtPhone.Text,
                dtpBirthDate.Value.Date,
                selectedCourse,
                rdoMale.Checked ? "Nam" : "Nữ",
                chkGraduated.Checked);

            try
            {
                SaveRegistration(registration);

                MessageBox.Show(
                    $"Đăng ký thành công!\n\n" +
                    $"Họ và tên: {registration.FullName}\n" +
                    $"Số điện thoại: {registration.Phone}\n" +
                    $"Ngày sinh: {registration.BirthDate:dd/MM/yyyy}\n" +
                    $"Khóa học: {registration.Course.Name}\n" +
                    $"Mã khóa học: {registration.Course.Code}\n" +
                    $"Giới tính: {registration.Gender}\n" +
                    $"Tình trạng: {(registration.IsGraduated ? "Đã tốt nghiệp" : "Chưa tốt nghiệp")}\n\n" +
                    "Dữ liệu được lưu tại CourseRegistrationAppDb.dbo.CourseRegistrations.",
                    "Kết quả đăng ký", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) when (ex is Microsoft.Data.SqlClient.SqlException or InvalidOperationException)
            {
                MessageBox.Show(
                    $"Không thể lưu đăng ký vào SQL Server LocalDB.\n\nChi tiết: {ex.Message}",
                    "Lỗi cơ sở dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void SaveRegistration(RegistrationData registration)
        {
            EnsureDatabaseAndTable();
            using var connection = new Microsoft.Data.SqlClient.SqlConnection(GetDatabaseConnectionString());
            connection.Open();

            using var insertCommand = connection.CreateCommand();
            insertCommand.CommandText = """
                INSERT INTO dbo.CourseRegistrations
                    (FullName, Phone, BirthDate, CourseCode, CourseName, Gender, IsGraduated)
                VALUES
                    (@FullName, @Phone, @BirthDate, @CourseCode, @CourseName, @Gender, @IsGraduated);
                """;
            insertCommand.Parameters.AddWithValue("@FullName", registration.FullName);
            insertCommand.Parameters.AddWithValue("@Phone", registration.Phone);
            insertCommand.Parameters.AddWithValue("@BirthDate", registration.BirthDate);
            insertCommand.Parameters.AddWithValue("@CourseCode", registration.Course.Code);
            insertCommand.Parameters.AddWithValue("@CourseName", registration.Course.Name);
            insertCommand.Parameters.AddWithValue("@Gender", registration.Gender);
            insertCommand.Parameters.AddWithValue("@IsGraduated", registration.IsGraduated);
            insertCommand.ExecuteNonQuery();
        }

        private static void EnsureDatabaseAndTable()
        {
            const string server = @"(localdb)\MSSQLLocalDB";
            var masterConnectionString = $"Server={server};Database=master;Integrated Security=True;TrustServerCertificate=True;";

            using (var masterConnection = new Microsoft.Data.SqlClient.SqlConnection(masterConnectionString))
            {
                masterConnection.Open();
                using var createDatabaseCommand = masterConnection.CreateCommand();
                createDatabaseCommand.CommandText = "IF DB_ID(N'CourseRegistrationAppDb') IS NULL CREATE DATABASE [CourseRegistrationAppDb];";
                createDatabaseCommand.ExecuteNonQuery();
            }

            using var connection = new Microsoft.Data.SqlClient.SqlConnection(GetDatabaseConnectionString());
            connection.Open();
            using var createTableCommand = connection.CreateCommand();
            createTableCommand.CommandText = """
                IF OBJECT_ID(N'dbo.CourseRegistrations', N'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.CourseRegistrations
                    (
                        Id INT IDENTITY(1,1) PRIMARY KEY,
                        FullName NVARCHAR(150) NOT NULL,
                        Phone VARCHAR(20) NOT NULL,
                        BirthDate DATE NOT NULL,
                        CourseCode VARCHAR(30) NOT NULL,
                        CourseName NVARCHAR(150) NOT NULL,
                        Gender NVARCHAR(20) NOT NULL,
                        IsGraduated BIT NOT NULL,
                        RegisteredAt DATETIME2 NOT NULL DEFAULT SYSDATETIME()
                    );
                END;
                """;
            createTableCommand.ExecuteNonQuery();
        }

        private static string GetDatabaseConnectionString() =>
            @"Server=(localdb)\MSSQLLocalDB;Database=CourseRegistrationAppDb;Integrated Security=True;TrustServerCertificate=True;";

        private sealed record CourseItem(string Name, string Code);

        private sealed record RegistrationData(
            string FullName,
            string Phone,
            DateTime BirthDate,
            CourseItem Course,
            string Gender,
            bool IsGraduated);
    }
}
