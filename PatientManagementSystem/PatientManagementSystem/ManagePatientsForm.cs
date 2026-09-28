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

        public ManagePatientsForm()
        {
            this.Text = "Manage Patients";
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            InitializeUI();
            UITheme.ApplyTheme(this);
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

            btnAddNew = new Button
            {
                Text = "+ Register New Patient",
                Location = new Point(680, 20),
                Width = 180,
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
                Size = new Size(840, 450),
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

            this.Controls.Add(dgvPatients);
        }

        private void LoadPatients()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    string query = "SELECT PatientId AS 'ID', FullName AS 'Name', NIC, ContactNumber AS 'Contact', Gender FROM Patients";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvPatients.DataSource = dt;

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
                        catch (SqlException ex)
                        {
                            if (ex.Number == 547) // FK violation
                                new ToastNotification("Cannot delete patient because they have existing medical records.", ToastType.Error).Show();
                            else
                                new ToastNotification("Database error: " + ex.Message, ToastType.Error).Show();
                        }
                        catch (Exception ex)
                        {
                            new ToastNotification("Failed to delete patient: " + ex.Message, ToastType.Error).Show();
                        }
                    }
                }
            }
        }
    }
}
