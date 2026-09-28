using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public partial class LoginForm : Form
    {
        SqlConnection mySqlConnection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;");

        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
        DataSet myDataSet = new DataSet();
        public string LoggedInUser { get; private set; }
        public string LoggedInRole { get; private set; }
        public LoginForm()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text; // Use raw password string to compare

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                new ToastNotification("Please enter both Username and Password.", ToastType.Warning).Show();
                return;
            }






            try
            {
                
                //ENCRYPT PASSWORD

                string encryptedPassword = Encrypt(password);

                // 2. LOGIN QUERY

                // Secure query parameterized to prevent SQL Injection (supports both plain text & AES-256 encrypted passwords)
                string query = "SELECT Username, Role FROM Users WHERE Username = @Username AND (Password = @Password OR Password = @EncryptedPassword)";


                //CONFIGURE DATA ADAPTER

                mySqlDataAdapter.SelectCommand =new SqlCommand(query, mySqlConnection);

                mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Username", username);

                mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@Password", password);

                mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@EncryptedPassword", encryptedPassword);

                mySqlConnection.Open();
                
                //LOAD LOGIN RESULT INTO DATASET

                mySqlDataAdapter.Fill(myDataSet, "Users");

                //CHECK USER

                if (myDataSet.Tables["Users"].Rows.Count > 0)
                {
                    DataRow userRow = myDataSet.Tables["Users"].Rows[0];

                    // Get username and role
                    LoggedInUser =userRow["Username"].ToString();
                    LoggedInRole =userRow["Role"].ToString();

                    // LOGIN SUCCESSFUL
                    mySqlConnection.Close();
                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // LOGIN FAILED
                    mySqlConnection.Close();
                    MessageBox.Show(
                        "Invalid Username or Password.",
                        "Authentication Failed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Database query error: " + ex.Message,
                    "System Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
