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
using System.Xml.Linq;

namespace PatientManagementSystem
{

    public partial class AddMedicalRecordForm : Form
    {
        SqlConnection mySqlConnection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;");

        SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter();
        DataSet myDataSet = new DataSet();
        private int selectedPatientId = -1;

        private TabPage tabReport;
        
        public AddMedicalRecordForm()
        {
            InitializeComponent();
            
            TabControl tabControlRecords = new TabControl();
            tabControlRecords.Location = new Point(47, 190);
            tabControlRecords.Size = new Size(905, 380);
            tabControlRecords.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            tabControlRecords.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            TabPage tabPrescription = new TabPage("Current Prescription");
            tabPrescription.BackColor = Color.White;
            
            tabReport = new TabPage("Medical Report");
            tabReport.BackColor = Color.White;

            tabControlRecords.TabPages.Add(tabPrescription);
            tabControlRecords.TabPages.Add(tabReport);

            lblDiagnosis.Location = new Point(20, 15);
            txtDiagnosis.Location = new Point(20, 45);
            txtDiagnosis.Size = new Size(860, 70);
            txtDiagnosis.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            
            lblPrescription.Location = new Point(20, 125);
            txtPrescription.Location = new Point(20, 155);
            txtPrescription.Size = new Size(860, 70); 
            txtPrescription.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            
            Panel pnlBottom = new Panel();
            pnlBottom.Location = new Point(0, 240);
            pnlBottom.Size = new Size(900, 50);
            pnlBottom.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            pnlBottom.BackColor = Color.White;

            btnSaveRecord.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnSaveRecord.Top = 5; // Reset Y relative to the new panel
            btnCancel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnCancel.Top = 5;
            
            tabPrescription.Resize += (s, ev) => 
            {
                int pad = 40;
                int innerW = tabPrescription.ClientSize.Width;
                if (innerW < 200) return; // Prevent layout math bugs during early initialization
                
                txtDiagnosis.Width = innerW - pad;
                txtPrescription.Width = innerW - pad;
                pnlBottom.Width = innerW;
                
                // Pin buttons exactly to the right side
                btnCancel.Left = innerW - btnCancel.Width - 20;
                btnSaveRecord.Left = btnCancel.Left - btnSaveRecord.Width - 10;
            };

            pnlBottom.Controls.Add(btnSaveRecord);
            pnlBottom.Controls.Add(btnCancel);

            tabPrescription.Controls.Add(lblDiagnosis);
            tabPrescription.Controls.Add(txtDiagnosis);
            tabPrescription.Controls.Add(lblPrescription);
            tabPrescription.Controls.Add(txtPrescription);
            tabPrescription.Controls.Add(pnlBottom);
            
            // Absolutely guarantee pnlBottom is brought to the very front so nothing overlaps it
            tabPrescription.Controls.SetChildIndex(pnlBottom, 0);

            this.Controls.Add(tabControlRecords);

            // Fix the search bar anchors
            cmbSearchPatient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            btnSearch.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            cmbSearchPatient.TextUpdate += CmbSearchPatient_TextUpdate;
            cmbSearchPatient.SelectedIndexChanged += (s, ev) => { btnSearch.PerformClick(); };
            
            UITheme.ApplyTheme(this);
        }

        private void EmbedMedicalReportForm(string pName)
        {
            tabReport.Controls.Clear();
            
            int doctorId = 1; // Fallback
            try {
                using (SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;")) {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT UserId FROM Users WHERE Username = (SELECT TOP 1 Username FROM Users WHERE Role='Doctor')", conn)) {
                        object res = cmd.ExecuteScalar();
                        if (res != null) doctorId = Convert.ToInt32(res);
                    }
                }
            } catch {}

            AddMedicalReportForm reportForm = new AddMedicalReportForm(selectedPatientId, pName, doctorId);
            reportForm.TopLevel = false;
            reportForm.FormBorderStyle = FormBorderStyle.None;
            reportForm.Dock = DockStyle.Fill;
            tabReport.Controls.Add(reportForm);
            reportForm.Show();
        }

        private void AddMedicalRecordForm_Load(object sender, EventArgs e)
        {
        }

        private void CmbSearchPatient_TextUpdate(object sender, EventArgs e)
        {
            string searchText = cmbSearchPatient.Text.Trim();
            if (searchText.Length < 2) return;

            try
            {
                int cursorPosition = cmbSearchPatient.SelectionStart;
                
                string query = "SELECT FullName, NIC FROM Patients WHERE FullName LIKE @Search OR NIC LIKE @Search";
                using (SqlCommand cmd = new SqlCommand(query, mySqlConnection))
                {
                    cmd.Parameters.AddWithValue("@Search", "%" + searchText + "%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbSearchPatient.Items.Clear();
                    foreach (DataRow row in dt.Rows)
                    {
                        cmbSearchPatient.Items.Add(row["FullName"].ToString() + " - " + row["NIC"].ToString());
                    }
                    
                    cmbSearchPatient.Select(cursorPosition, 0);
                    cmbSearchPatient.DroppedDown = true;
                    Cursor.Current = Cursors.Default;
                }
            }
            catch { }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchInput = cmbSearchPatient.Text.Trim();
            if (string.IsNullOrEmpty(searchInput))
            {
                new ToastNotification("Please enter a Patient's Name or NIC to search.", ToastType.Warning).Show();
                return;
            }

            string searchNIC = searchInput;
            if (searchInput.Contains(" - "))
            {
                searchNIC = searchInput.Substring(searchInput.LastIndexOf(" - ") + 3).Trim();
            }

            try
            {
                string query = "SELECT PatientId, FullName, BloodGroup, Gender FROM Patients WHERE NIC = @NIC";

                mySqlDataAdapter.SelectCommand = new SqlCommand(query, mySqlConnection);
                mySqlDataAdapter.SelectCommand.Parameters.AddWithValue("@NIC", searchNIC);

                // Fill DataSet
                mySqlDataAdapter.Fill(myDataSet, "Patients");

                if (myDataSet.Tables["Patients"].Rows.Count > 0)
                {
                    DataRow row = myDataSet.Tables["Patients"].Rows[0];

                    selectedPatientId = Convert.ToInt32(row["PatientId"]);

                    lblPatientInfo.Text = string.Format("Selected Patient: {0} | Gender: {1} | Blood: {2}", row["FullName"], row["Gender"], row["BloodGroup"]);

                    lblPatientInfo.ForeColor = System.Drawing.Color.ForestGreen;
                    btnSaveRecord.Enabled = true;
                    
                    EmbedMedicalReportForm(row["FullName"].ToString());
                }
                else
                {
                    selectedPatientId = -1;

                    lblPatientInfo.Text ="No patient found with this NIC. Please register the patient first.";

                    lblPatientInfo.ForeColor = System.Drawing.Color.Crimson;
                    btnSaveRecord.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error searching for patient:\n" + ex.Message,
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnSaveRecord_Click(object sender, EventArgs e)
        {
            if (selectedPatientId == -1)
            {
                new ToastNotification("Please search and select a valid patient first.", ToastType.Warning).Show();
                return;
            }

            string diagnosisText = txtDiagnosis.Text.Trim();
            string prescriptionText = txtPrescription.Text.Trim();

            if (string.IsNullOrEmpty(diagnosisText) || string.IsNullOrEmpty(prescriptionText))
            {
                new ToastNotification("Both Diagnosis and Prescription fields are required.", ToastType.Warning).Show();
                return;
            }

            //AES Encryption of Sensitive Medical Fields
            string encryptedDiagnosis = Encrypt(diagnosisText);
            string encryptedPrescription = Encrypt(prescriptionText);

            //MessageBox.Show(encryptedDiagnosis);

            // Save encrypted records (Ciphertext) to Database
            try
            {
                mySqlDataAdapter.InsertCommand = new SqlCommand("INSERT INTO MedicalRecords(PatientId, Diagnosis, Prescription) VALUES (@PatientId, @Diagnosis, @Prescription)",mySqlConnection);

                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@PatientId", selectedPatientId);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue("@Diagnosis", encryptedDiagnosis);
                mySqlDataAdapter.InsertCommand.Parameters.AddWithValue( "@Prescription", encryptedPrescription);

                mySqlConnection.Open();
                // Execute INSERT through DataAdapter
                int rows = mySqlDataAdapter.InsertCommand.ExecuteNonQuery();

                if (rows > 0)
                {
                    new ToastNotification("Medical Record encrypted and saved!", ToastType.Success).Show();

                    ClearForm();
                }

                mySqlConnection.Close();
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to save encrypted medical record: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void ClearForm()
        {
            cmbSearchPatient.Text = "";
            txtDiagnosis.Clear();
            txtPrescription.Clear();
            lblPatientInfo.Text = "Search for a patient using their NIC above to begin.";
            lblPatientInfo.ForeColor = System.Drawing.Color.Gray;
            selectedPatientId = -1;
            btnSaveRecord.Enabled = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void AddMedicalRecordForm_Load_1(object sender, EventArgs e)
        {

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
