using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public partial class UserManagementForm : Form
    {
        public UserManagementForm()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            cmbRole.SelectedIndex = 0; // Default "Doctor"
            LoadUsersList();
        }
        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string role = cmbRole.SelectedItem != null ? cmbRole.SelectedItem.ToString() : "Doctor";

            // 1. Validation Checks
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                new ToastNotification("Username and Password fields are required.", ToastType.Warning).Show();
                return;
            }

            // Username validation: Alphanumeric and underscore, 3-30 chars
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,30}$"))
            {
                new ToastNotification("Username must be 3-30 chars (letters/numbers/_).", ToastType.Warning).Show();
                return;
            }

            try
            {
                // 2. AES-256 Encryption of Password
                string encryptedPassword = Encrypt(password);

                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string insertQuery = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, @Role)";
                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@Password", encryptedPassword);
                        cmd.Parameters.AddWithValue("@Role", role);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            new ToastNotification(string.Format("New {0} user '{1}' registered!", role, username), ToastType.Success).Show();
                            ClearForm();
                            LoadUsersList();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627 || ex.Number == 2601) // Unique constraint violation
                {
                    new ToastNotification("The username already exists.", ToastType.Error).Show();
                }
                else
                {
                    new ToastNotification("Database error: " + ex.Message, ToastType.Error).Show();
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("An unexpected error occurred: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            cmbRole.SelectedIndex = 0;
        }

        private void LoadUsersList()
        {
            try
            {
                string connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT UserId, Username, Role, Password FROM Users ORDER BY UserId DESC";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            DataTable displayDt = new DataTable();
                            displayDt.Columns.Add("UserId", typeof(int));
                            displayDt.Columns.Add("Username", typeof(string));
                            displayDt.Columns.Add("Role", typeof(string));
                            displayDt.Columns.Add("EncryptedPassword", typeof(string));

                            foreach (DataRow row in dt.Rows)
                            {
                                string rawPass = row["Password"] != DBNull.Value ? row["Password"].ToString() : "";

                                displayDt.Rows.Add(
                                    row["UserId"],
                                    row["Username"].ToString(),
                                    row["Role"].ToString(),
                                    string.IsNullOrEmpty(rawPass) ? "-" : (rawPass.Length > 20 ? rawPass.Substring(0, 20) + "..." : rawPass)
                                );
                            }

                            dgvUsers.DataSource = displayDt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Error loading user records: " + ex.Message, ToastType.Error).Show();
            }
        }


        // AES-256 Encryption Method
        private string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return string.Empty;
            try
            {
                byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes("PatientClinicKey1234567890123456");
                byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes("PatientClinicIV1");
                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;
                    var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (var ms = new System.IO.MemoryStream())
                    {
                        using (var cs = new System.Security.Cryptography.CryptoStream(ms, encryptor, System.Security.Cryptography.CryptoStreamMode.Write))
                        {
                            using (var sw = new System.IO.StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                        }
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Encryption failed: " + ex.Message);
                return string.Empty;
            }
        }
    }
}
