
using System.Data;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;

namespace bài4._1nagcap.Database;

public sealed class UserRepository
{
    private const string ServerName = @"(localdb)\MSSQLLocalDB";
    private const string DatabaseName = "LoginApp";
    private readonly string masterConnectionString =
        $"Server={ServerName};Integrated Security=True;TrustServerCertificate=True;";
    private readonly string connectionString =
        $"Server={ServerName};Database={DatabaseName};Integrated Security=True;TrustServerCertificate=True;";

    public void EnsureDatabase()
    {
        using (var connection = new SqlConnection(masterConnectionString))
        {
            connection.Open();
            using var command = connection.CreateCommand();
            command.CommandText = $"IF DB_ID(N'{DatabaseName}') IS NULL CREATE DATABASE [{DatabaseName}];";
            command.ExecuteNonQuery();
        }

        using var databaseConnection = new SqlConnection(connectionString);
        databaseConnection.Open();
        using var tableCommand = databaseConnection.CreateCommand();
        tableCommand.CommandText = """
            IF OBJECT_ID(N'dbo.Users', N'U') IS NULL
            BEGIN
                CREATE TABLE dbo.Users
                (
                    Id INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
                    Username NVARCHAR(100) NOT NULL CONSTRAINT UQ_Users_Username UNIQUE,
                    PasswordHash VARBINARY(32) NOT NULL,
                    PasswordSalt VARBINARY(16) NOT NULL,
                    CreatedAt DATETIME2 NOT NULL CONSTRAINT DF_Users_CreatedAt DEFAULT SYSUTCDATETIME()
                );
            END
            """;
        tableCommand.ExecuteNonQuery();
    }

    public bool UserExists(string username)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = new SqlCommand(
            "SELECT COUNT(1) FROM dbo.Users WHERE Username = @Username;", connection);
        command.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;
        return Convert.ToInt32(command.ExecuteScalar()) > 0;
    }

    public int CreateUser(string username, string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = HashPassword(password, salt);

        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = new SqlCommand("""
            INSERT INTO dbo.Users (Username, PasswordHash, PasswordSalt)
            VALUES (@Username, @PasswordHash, @PasswordSalt);
            SELECT CONVERT(int, SCOPE_IDENTITY());
            """, connection);
        command.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;
        command.Parameters.Add("@PasswordHash", SqlDbType.VarBinary, 32).Value = hash;
        command.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary, 16).Value = salt;
        object? result = command.ExecuteScalar();
        if (result is null || result == DBNull.Value)
        {
            throw new DataException("The user insert did not return an identity value.");
        }

        return Convert.ToInt32(result);
    }

    public string GetDatabaseTarget()
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = new SqlCommand(
            "SELECT CONCAT(@@SERVERNAME, N' / ', DB_NAME());", connection);
        return Convert.ToString(command.ExecuteScalar()) ?? $"{ServerName} / {DatabaseName}";
    }

    public bool ValidateUser(string username, string password)
    {
        using var connection = new SqlConnection(connectionString);
        connection.Open();
        using var command = new SqlCommand(
            "SELECT PasswordHash, PasswordSalt FROM dbo.Users WHERE Username = @Username;", connection);
        command.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = username;

        using SqlDataReader reader = command.ExecuteReader();
        if (!reader.Read())
        {
            return false;
        }

        byte[] expectedHash = (byte[])reader["PasswordHash"];
        byte[] salt = (byte[])reader["PasswordSalt"];
        byte[] actualHash = HashPassword(password, salt);
        return CryptographicOperations.FixedTimeEquals(expectedHash, actualHash);
    }

    private static byte[] HashPassword(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            100_000,
            HashAlgorithmName.SHA256,
            32);
    }
}
