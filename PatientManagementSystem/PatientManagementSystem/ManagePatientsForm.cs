using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public class ManagePatientsForm : Form
    {
        private DataGridView dgvPatients;
        private Button btnAddNew;
        private TextBox txtSearch;
        private ComboBox cmbFilterGender;
        private DataTable patientsTable;
        private DataView patientsView;

        public ManagePatientsForm()
        {
            this.Text = "Manage Patients";
            this.Size = new Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            InitializeUI();
            UITheme.ApplyTheme(this);
            cmbFilterGender.FlatStyle = FlatStyle.Standard; // Keep border visible
            LoadPatients();
        }

        private void InitializeUI()
        {
            Label lblHeader = new Label
            {
                Text = "PATIENTS DASHBOARD",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            txtSearch = new TextBox
            {
                Location = new Point(300, 20),
                Width = 200,
                Font = new Font("Segoe UI", 12)
            };
            // Set Placeholder programmatically for traditional WinForms
            txtSearch.Text = "Search Name or NIC...";
            txtSearch.ForeColor = Color.Gray;
            txtSearch.GotFocus += (s, e) => { if (txtSearch.Text == "Search Name or NIC...") { txtSearch.Text = ""; txtSearch.ForeColor = Color.Black; } };
            txtSearch.LostFocus += (s, e) => { if (string.IsNullOrWhiteSpace(txtSearch.Text)) { txtSearch.Text = "Search Name or NIC..."; txtSearch.ForeColor = Color.Gray; } };
            txtSearch.TextChanged += FilterData;
            this.Controls.Add(txtSearch);

            cmbFilterGender = new ComboBox
            {
                Location = new Point(520, 20),
                Width = 120,
                Font = new Font("Segoe UI", 12),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbFilterGender.Items.AddRange(new string[] { "All Genders", "Male", "Female", "Other" });
            cmbFilterGender.SelectedIndex = 0;
            cmbFilterGender.SelectedIndexChanged += FilterData;
            this.Controls.Add(cmbFilterGender);

            btnAddNew = new Button
            {
                Text = "+ Patient",
                Location = new Point(840, 20),
                Width = 120,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnAddNew.FlatAppearance.BorderSize = 0;
            btnAddNew.Click += (s, e) => 
            {
                PatientRegistrationForm form = new PatientRegistrationForm(-1);
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog();
                LoadPatients();
            };
            this.Controls.Add(btnAddNew);

            dgvPatients = new DataGridView
            {
                Location = new Point(20, 80),
                Size = new Size(940, 450),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            dgvPatients.CellContentClick += DgvPatients_CellContentClick;
            dgvPatients.ColumnHeaderMouseClick += DgvPatients_ColumnHeaderMouseClick;

            this.Controls.Add(dgvPatients);
        }

        private void FilterData(object sender, EventArgs e)
        {
            if (patientsView == null) return;
            string search = txtSearch.Text.Trim().Replace("'", "''");
            string genderFilter = cmbFilterGender.SelectedItem.ToString();

            string filter = "";
            if (!string.IsNullOrEmpty(search) && search != "Search Name or NIC...")
            {
                filter += string.Format("(Name LIKE '%{0}%' OR NIC LIKE '%{0}%')", search);
            }
            if (genderFilter != "All Genders")
            {
                if (filter.Length > 0) filter += " AND ";
                filter += string.Format("Gender = '{0}'", genderFilter);
            }
            patientsView.RowFilter = filter;
        }

        private void DgvPatients_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgvPatients.Columns[e.ColumnIndex].Name == "Age")
            {
                string currentSort = patientsView.Sort;
                if (currentSort == "Age ASC")
                    patientsView.Sort = "Age DESC";
                else
                    patientsView.Sort = "Age ASC";
            }
        }

        private void LoadPatients()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    string query = "SELECT PatientId AS 'ID', FullName AS 'Name', NIC, Age, ContactNumber AS 'Contact', Gender FROM Patients";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    patientsTable = new DataTable();
                    da.Fill(patientsTable);
                    
                    patientsView = new DataView(patientsTable);
                    dgvPatients.DataSource = patientsView;

                    if (!dgvPatients.Columns.Contains("EditAction"))
                    {
                        DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                        btnEdit.Name = "EditAction";
                        btnEdit.HeaderText = "Edit";
                        btnEdit.Text = "✏";
                        btnEdit.UseColumnTextForButtonValue = true;
                        btnEdit.FlatStyle = FlatStyle.Flat;
                        btnEdit.Width = 60;
                        btnEdit.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvPatients.Columns.Add(btnEdit);
                    }

                    if (!dgvPatients.Columns.Contains("DeleteAction"))
                    {
                        DataGridViewButtonColumn btnDelete = new DataGridViewButtonColumn();
                        btnDelete.Name = "DeleteAction";
                        btnDelete.HeaderText = "Delete";
                        btnDelete.Text = "🗑";
                        btnDelete.UseColumnTextForButtonValue = true;
                        btnDelete.FlatStyle = FlatStyle.Flat;
                        btnDelete.Width = 60;
                        btnDelete.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                        dgvPatients.Columns.Add(btnDelete);
                    }
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to load patients: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void DgvPatients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int patientId = Convert.ToInt32(dgvPatients.Rows[e.RowIndex].Cells["ID"].Value);

                if (dgvPatients.Columns[e.ColumnIndex].Name == "EditAction")
                {
                    PatientRegistrationForm form = new PatientRegistrationForm(patientId);
                    form.StartPosition = FormStartPosition.CenterParent;
                    form.ShowDialog();
                    LoadPatients();
                }
                else if (dgvPatients.Columns[e.ColumnIndex].Name == "DeleteAction")
                {
                    var result = MessageBox.Show("Are you sure you want to delete this patient? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                            {
                                string query = "DELETE FROM Patients WHERE PatientId = @ID";
                                SqlCommand cmd = new SqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@ID", patientId);
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                new ToastNotification("Patient deleted successfully.", ToastType.Success).Show();
                                LoadPatients();
                            }
                        }
                        catch (Exception ex)
                        {
                            new ToastNotification("Failed to delete patient. Ensure there are no dependent records.", ToastType.Error).Show();
                        }
                    }
                }
            }
        }
    }
}
