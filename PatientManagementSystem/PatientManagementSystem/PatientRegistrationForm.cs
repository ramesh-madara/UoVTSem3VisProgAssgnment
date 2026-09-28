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
    public partial class PatientRegistrationForm : Form
    {
        SqlConnection mySqlConnection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;");

        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
        DataSet myDataSet = new DataSet();
        private int editingPatientId = -1;
        public PatientRegistrationForm(int patientId = -1)
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
            this.editingPatientId = patientId;
        }

        private void PatientRegistrationForm_Load(object sender, EventArgs e)
        {
            if (editingPatientId > 0)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;"))
                    {
                        string query = "SELECT FullName, NIC, ContactNumber, Email, Address, BloodGroup, Gender FROM Patients WHERE PatientId = @ID";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@ID", editingPatientId);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                txtName.Text = reader["FullName"].ToString();
                                txtNIC.Text = reader["NIC"].ToString();
                                txtContactNumber.Text = reader["ContactNumber"].ToString();
                                txtEmail.Text = reader["Email"].ToString();
                                txtAddress.Text = reader["Address"].ToString();
                                cmbBloodGroup.SelectedItem = reader["BloodGroup"].ToString();
                                cmbGender.SelectedItem = reader["Gender"].ToString();
                                btnSave.Text = "Update Patient";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    new ToastNotification("Failed to load patient: " + ex.Message, ToastType.Error).Show();
                }
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Gather text field variables
            string name = txtName.Text.Trim();
            string nic = txtNIC.Text.Trim();
            string contact = txtContactNumber.Text.Trim();
            string email = txtEmail.Text.Trim();
            string address = txtAddress.Text.Trim();
            string bloodGroup = cmbBloodGroup.SelectedItem != null ? cmbBloodGroup.SelectedItem.ToString() : "";
            string gender = cmbGender.SelectedItem != null ? cmbGender.SelectedItem.ToString() : "";

            // Validate Empty Fields
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(nic) || string.IsNullOrEmpty(contact) ||
                string.IsNullOrEmpty(email) || string.IsNullOrEmpty(address) ||
                string.IsNullOrEmpty(bloodGroup) || string.IsNullOrEmpty(gender))
            {
                new ToastNotification("Please fill all fields before saving the profile.", ToastType.Warning).Show();
                return;
            }

            // Regular Expression (RegEx) Validations

            // NIC formats: 9 digits with 'v/x' OR 12 digits (new NIC)
            string nicPattern = @"^([0-9]{9}[vVxX]|[0-9]{12})$";
            if (!Regex.IsMatch(nic, nicPattern))
            {
                new ToastNotification("Invalid NIC format. Enter 9 digits with V/X or 12 digits.", ToastType.Warning).Show();
                txtNIC.Focus();
                return;
            }

            // Phone Number: Must be exactly 10 digits
            string phonePattern = @"^[0-9]{10}$";
            if (!Regex.IsMatch(contact, phonePattern))
            {
                new ToastNotification("Invalid Contact Number. Must be exactly 10 digits.", ToastType.Warning).Show();
                txtContactNumber.Focus();
                return;
            }

            // Email format verification
            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                new ToastNotification("Invalid Email Address format.", ToastType.Warning).Show();
                txtEmail.Focus();
                return;
            }

            // INSERT or UPDATE into SQL Server database
            try
            {
                string query = editingPatientId > 0 
                    ? "UPDATE Patients SET FullName=@Name, NIC=@NIC, ContactNumber=@Contact, Email=@Email, Address=@Address, BloodGroup=@Blood, Gender=@Gender WHERE PatientId=@ID"
                    : "INSERT INTO Patients (FullName, NIC, ContactNumber, Email, Address, BloodGroup, Gender) VALUES (@Name, @NIC, @Contact, @Email, @Address, @Blood, @Gender)";

                mySqlDataAdapter.InsertCommand = new SqlCommand(query, mySqlConnection);

                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@Name", name);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@NIC", nic);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@Contact", contact);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@Email", email);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@Address", address);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@Blood", bloodGroup);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@Gender", gender);
                if (editingPatientId > 0)
                    mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@ID", editingPatientId);

                if (mySqlConnection.State == ConnectionState.Closed)
                    mySqlConnection.Open();

                int rowsAffected = mySqlDataAdapter.InsertCommand.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    new ToastNotification(editingPatientId > 0 ? "Patient profile updated successfully!" : "Patient profile saved successfully!", ToastType.Success).Show();
                    
                    if (editingPatientId > 0) {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    } else {
                        ClearFields();
                    }
                }

                mySqlConnection.Close();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627)
                {
                    new ToastNotification("A patient with this NIC is already registered.", ToastType.Error).Show();
                }
                else
                {
                    new ToastNotification("Database write failed: " + sqlEx.Message, ToastType.Error).Show();
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Database write failed: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtNIC.Clear();
            txtContactNumber.Clear();
            txtEmail.Clear();
            txtAddress.Clear();
            cmbBloodGroup.SelectedIndex = -1;
            cmbGender.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void PatientRegistrationForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
