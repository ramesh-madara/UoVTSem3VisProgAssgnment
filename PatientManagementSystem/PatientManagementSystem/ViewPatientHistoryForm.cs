using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public partial class ViewPatientHistoryForm : Form
    {
        SqlConnection mySqlConnection = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;");
        DataTable dtAllDecryptedRecords = new DataTable();
        private string fixedRecordType = "";

        public ViewPatientHistoryForm()
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);
        }

        public void SetRecordFilter(string recordType)
        {
            fixedRecordType = recordType;
            ApplyFilters();
        }

        private void ViewPatientHistoryForm_Load(object sender, EventArgs e)
        {
            TabControl tabControlRecords = new TabControl();
            tabControlRecords.Location = new Point(47, 150);
            tabControlRecords.Size = new Size(907, 373);
            tabControlRecords.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControlRecords.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            TabPage tabPrescription = new TabPage("Prescription Records");
            TabPage tabReport = new TabPage("Medical Reports");

            tabControlRecords.TabPages.Add(tabPrescription);
            tabControlRecords.TabPages.Add(tabReport);

            dgvHistory.Dock = DockStyle.Fill;
            tabPrescription.Controls.Add(dgvHistory);

            this.Controls.Add(tabControlRecords);
            
            // Set default filter
            fixedRecordType = "Prescription";

            tabControlRecords.SelectedIndexChanged += (s, ev) => {
                if (tabControlRecords.SelectedIndex == 0) {
                    tabPrescription.Controls.Add(dgvHistory);
                    SetRecordFilter("Prescription");
                } else {
                    tabReport.Controls.Add(dgvHistory);
                    SetRecordFilter("Report");
                }
            };

            StyleDataGridView();
            LoadAllRecords();
            cmbSortBy.SelectedIndex = 0; // Default sort
            dgvHistory.CellContentClick += DgvHistory_CellContentClick;
            dgvHistory.CellFormatting += DgvHistory_CellFormatting;
        }

        private void StyleDataGridView()
        {
            dgvHistory.BorderStyle = BorderStyle.None;
            dgvHistory.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 244, 248);
            dgvHistory.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvHistory.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvHistory.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvHistory.BackgroundColor = Color.White;

            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHistory.ColumnHeadersHeight = 40;
            
            dgvHistory.EnableHeadersVisualStyles = false;
            dgvHistory.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvHistory.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 62, 80);
            dgvHistory.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistory.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHistory.ColumnHeadersHeight = 40;
            
            dgvHistory.DefaultCellStyle.Font = new Font("Segoe UI", 9.5f);
            dgvHistory.RowTemplate.Height = 35;
            dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void LoadAllRecords()
        {
            try
            {
                string query = @"
                    SELECT p.FullName, p.NIC, m.RecordId, m.Diagnosis, m.Prescription, m.CreatedDate, 'Prescription' AS RecordType 
                    FROM MedicalRecords m 
                    INNER JOIN Patients p ON m.PatientId = p.PatientId
                    UNION ALL
                    SELECT p.FullName, p.NIC, r.ReportID AS RecordId, t.TemplateName AS Diagnosis, '' AS Prescription, r.ReportDate AS CreatedDate, 'Report' AS RecordType
                    FROM PatientReports r
                    INNER JOIN Patients p ON r.PatientID = p.PatientId
                    INNER JOIN ReportTemplates t ON r.TemplateID = t.TemplateID";

                SqlDataAdapter adapter = new SqlDataAdapter(query, mySqlConnection);

                DataSet ds = new DataSet();
                adapter.Fill(ds, "Records");

                dtAllDecryptedRecords.Columns.Clear();
                dtAllDecryptedRecords.Rows.Clear();

                dtAllDecryptedRecords.Columns.Add("Patient Name", typeof(string));
                dtAllDecryptedRecords.Columns.Add("NIC", typeof(string));
                dtAllDecryptedRecords.Columns.Add("Record ID", typeof(int));
                dtAllDecryptedRecords.Columns.Add("Type", typeof(string));
                dtAllDecryptedRecords.Columns.Add("Diagnosis", typeof(string));
                dtAllDecryptedRecords.Columns.Add("Prescription", typeof(string));
                dtAllDecryptedRecords.Columns.Add("Created Date", typeof(DateTime));

                foreach (DataRow row in ds.Tables["Records"].Rows)
                {
                    string name = row["FullName"].ToString();
                    string nic = row["NIC"].ToString();
                    int recordId = Convert.ToInt32(row["RecordId"]);
                    string recordType = row["RecordType"].ToString();
                    string encDiag = row["Diagnosis"].ToString();
                    string encPres = row["Prescription"].ToString();
                    DateTime createdDate = Convert.ToDateTime(row["CreatedDate"]);

                    dtAllDecryptedRecords.Rows.Add(
                        name,
                        nic,
                        recordId,
                        recordType,
                        recordType == "Prescription" ? Decrypt(encDiag) : encDiag, // Template Name is unencrypted
                        recordType == "Prescription" ? Decrypt(encPres) : "",
                        createdDate
                    );
                }

                dgvHistory.DataSource = dtAllDecryptedRecords;
                
                // Allow Fill mode to handle widths dynamically, but set some relative fill weights if needed.
                // Or just clear the hardcoded widths since Fill takes over.

                if (!dgvHistory.Columns.Contains("ViewAction"))
                {
                    DataGridViewButtonColumn btnCol = new DataGridViewButtonColumn();
                    btnCol.Name = "ViewAction";
                    btnCol.HeaderText = "View";
                    btnCol.Text = "👁";
                    btnCol.UseColumnTextForButtonValue = true;
                    btnCol.FlatStyle = FlatStyle.Flat;
                    btnCol.DefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
                    btnCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                    btnCol.DefaultCellStyle.ForeColor = Color.White;
                    btnCol.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgvHistory.Columns.Add(btnCol);
                }

                if (!dgvHistory.Columns.Contains("PrintAction"))
                {
                    DataGridViewButtonColumn printCol = new DataGridViewButtonColumn();
                    printCol.Name = "PrintAction";
                    printCol.HeaderText = "Print";
                    printCol.Text = "🖨";
                    printCol.UseColumnTextForButtonValue = true;
                    printCol.FlatStyle = FlatStyle.Flat;
                    printCol.DefaultCellStyle.BackColor = Color.FromArgb(52, 152, 219);
                    printCol.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                    printCol.DefaultCellStyle.ForeColor = Color.White;
                    printCol.DefaultCellStyle.SelectionForeColor = Color.White;
                    dgvHistory.Columns.Add(printCol);
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to load records: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void DgvHistory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && (dgvHistory.Columns[e.ColumnIndex].Name == "ViewAction" || dgvHistory.Columns[e.ColumnIndex].Name == "PrintAction"))
            {
                e.CellStyle.BackColor = Color.FromArgb(52, 152, 219);
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                e.CellStyle.SelectionForeColor = Color.White;
            }
        }

        private void DgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && (dgvHistory.Columns[e.ColumnIndex].Name == "ViewAction" || dgvHistory.Columns[e.ColumnIndex].Name == "PrintAction"))
            {
                DataGridViewRow row = dgvHistory.Rows[e.RowIndex];
                string name = row.Cells["Patient Name"].Value.ToString();
                string nic = row.Cells["NIC"].Value.ToString();
                int recordId = Convert.ToInt32(row.Cells["Record ID"].Value);
                string recordType = row.Cells["Type"].Value.ToString();
                string diagnosis = row.Cells["Diagnosis"].Value.ToString();
                string prescription = row.Cells["Prescription"].Value.ToString();
                string date = Convert.ToDateTime(row.Cells["Created Date"].Value).ToString("f");

                if (recordType == "Report")
                {
                    ReportDetailsModal modal = new ReportDetailsModal(name, nic, recordId, diagnosis, date);
                    if (dgvHistory.Columns[e.ColumnIndex].Name == "ViewAction")
                    {
                        modal.ShowDialog();
                    }
                    else
                    {
                        modal.ExportToPdf();
                    }
                }
                else
                {
                    if (dgvHistory.Columns[e.ColumnIndex].Name == "ViewAction")
                    {
                        RecordDetailsModal modal = new RecordDetailsModal(name, nic, recordId.ToString(), date, diagnosis, prescription);
                        modal.ShowDialog();
                    }
                    else
                    {
                        RecordDetailsModal.ExportToPdf(name, nic, recordId.ToString(), date, diagnosis, prescription);
                    }
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void cmbSortBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (dtAllDecryptedRecords == null || dtAllDecryptedRecords.Rows.Count == 0) return;

            string search = txtSearch.Text.Trim().Replace("'", "''");
            string filter = "";

            if (!string.IsNullOrEmpty(search))
            {
                filter = string.Format("([Patient Name] LIKE '%{0}%' OR [NIC] LIKE '%{0}%')", search);
            }

            if (!string.IsNullOrEmpty(fixedRecordType))
            {
                if (!string.IsNullOrEmpty(filter)) filter += " AND ";
                filter += string.Format("[Type] = '{0}'", fixedRecordType.Replace("'", "''"));
            }

            string sort = "";
            if (cmbSortBy.SelectedIndex == 0) sort = "[Created Date] DESC";
            else if (cmbSortBy.SelectedIndex == 1) sort = "[Created Date] ASC";
            else if (cmbSortBy.SelectedIndex == 2) sort = "[Patient Name] ASC";

            DataView dv = dtAllDecryptedRecords.DefaultView;
            dv.RowFilter = filter;
            dv.Sort = sort;
        }

        // AES-256 Decryption Method
        private string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return string.Empty;
            try
            {
                byte[] keyBytes = System.Text.Encoding.UTF8.GetBytes("PatientClinicKey1234567890123456");
                byte[] ivBytes = System.Text.Encoding.UTF8.GetBytes("PatientClinicIV1");
                using (var aes = System.Security.Cryptography.Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.IV = ivBytes;
                    var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (var ms = new System.IO.MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (var cs = new System.Security.Cryptography.CryptoStream(ms, decryptor, System.Security.Cryptography.CryptoStreamMode.Read))
                        {
                            using (var sr = new System.IO.StreamReader(cs))
                            {
                                return sr.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch { return "[Error Decrypting]"; }
        }
    }

    public class RecordDetailsModal : Form
    {
        public RecordDetailsModal(string name, string nic, string recordId, string date, string diagnosis, string prescription)
        {
            this.Text = "Medical Record Details";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10f);

            Label lblHeader = new Label
            {
                Text = "CLINICAL RECORD",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(30, 30),
                AutoSize = true
            };

            Panel pnlLine = new Panel
            {
                BackColor = Color.FromArgb(52, 152, 219),
                Location = new Point(30, 70),
                Size = new Size(520, 3)
            };

            int currentY = 100;
            
            AddField("Patient Name:", name, ref currentY);
            AddField("NIC Number:", nic, ref currentY);
            AddField("Record ID:", recordId, ref currentY);
            AddField("Date Created:", date, ref currentY);

            currentY += 10;
            
            AddLargeField("Diagnosis:", diagnosis, ref currentY);
            AddLargeField("Prescription:", prescription, ref currentY);

            Button btnClose = new Button
            {
                Text = "Close",
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(160, currentY + 20)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            Button btnExport = new Button
            {
                Text = "Export PDF",
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(290, currentY + 20)
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += (s, e) => ExportToPdf(name, nic, recordId, date, diagnosis, prescription);

            this.Controls.Add(lblHeader);
            this.Controls.Add(pnlLine);
            this.Controls.Add(btnClose);
            this.Controls.Add(btnExport);
        }

        private void AddField(string labelText, string valueText, ref int y)
        {
            Label lbl = new Label { Text = labelText, Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(30, y), AutoSize = true };
            Label val = new Label { Text = valueText, Font = new Font("Segoe UI", 11f), ForeColor = Color.Black, Location = new Point(200, y), AutoSize = true };
            this.Controls.Add(lbl);
            this.Controls.Add(val);
            y += 40;
        }

        private void AddLargeField(string labelText, string valueText, ref int y)
        {
            Label lbl = new Label { Text = labelText, Font = new Font("Segoe UI", 12f, FontStyle.Bold), ForeColor = Color.FromArgb(44, 62, 80), Location = new Point(30, y), AutoSize = true };
            TextBox txt = new TextBox
            {
                Text = valueText,
                Font = new Font("Segoe UI", 10f),
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(30, y + 30),
                Size = new Size(520, 100),
                ScrollBars = ScrollBars.Vertical
            };
            this.Controls.Add(lbl);
            this.Controls.Add(txt);
            y += 150;
        }

        public static void ExportToPdf(string name, string nic, string recordId, string date, string diagnosis, string prescription)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Document|*.pdf",
                Title = "Save Medical Record PDF",
                FileName = string.Format("MedicalRecord_{0}_{1}.pdf", name.Replace(" ", ""), date.Replace(":", "").Replace("/", "").Replace(" ", ""))
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string pContact = "", pAddress = "", pGender = "", pBlood = "";
                try {
                    using (SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;")) {
                        SqlCommand cmd = new SqlCommand("SELECT ContactNumber, Address, Gender, BloodGroup FROM Patients WHERE NIC = @NIC", conn);
                        cmd.Parameters.AddWithValue("@NIC", nic);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader()) {
                            if (reader.Read()) {
                                pContact = reader["ContactNumber"].ToString();
                                pAddress = reader["Address"].ToString();
                                pGender = reader["Gender"].ToString();
                                pBlood = reader["BloodGroup"].ToString();
                            }
                        }
                    }
                } catch {}

                try
                {
                    System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();
                    pd.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                    pd.PrinterSettings.PrintToFile = true;
                    pd.PrinterSettings.PrintFileName = sfd.FileName;

                    pd.PrintPage += (sender, e) => 
                    {
                        Graphics g = e.Graphics;
                        int yPos = 40;
                        int margin = 50;
                        int width = e.PageBounds.Width - (margin * 2);

                        // Fonts
                        Font titleFont = new Font("Segoe UI", 28, FontStyle.Bold);
                        Font subTitleFont = new Font("Segoe UI", 14, FontStyle.Bold);
                        Font boldFont = new Font("Segoe UI", 12, FontStyle.Bold);
                        Font regularFont = new Font("Segoe UI", 12, FontStyle.Regular);
                        
                        // Letterhead
                        g.DrawString("ARA Labs", titleFont, Brushes.DarkBlue, margin, yPos);
                        yPos += 45;
                        g.DrawString("Air Port Junction Ratmalana, Colombo", regularFont, Brushes.Gray, margin, yPos);
                        yPos += 20;
                        g.DrawString("Tel: 011-2345678 | Web: www.aralabs.lk", regularFont, Brushes.Gray, margin, yPos);
                        yPos += 35;
                        
                        g.DrawString("OFFICIAL PATIENT CLINICAL RECORD", subTitleFont, Brushes.DarkBlue, margin, yPos);
                        yPos += 25;
                        
                        // Line
                        g.DrawLine(new Pen(Color.DarkBlue, 3), margin, yPos, margin + width, yPos);
                        yPos += 25;

                        // Details Box
                        g.FillRectangle(Brushes.WhiteSmoke, margin, yPos, width, 175);
                        g.DrawRectangle(Pens.LightGray, margin, yPos, width, 175);
                        
                        yPos += 15;
                        g.DrawString("Patient Name:  " + name, boldFont, Brushes.Black, margin + 15, yPos);
                        g.DrawString("Gender: " + pGender + "   Blood: " + pBlood, boldFont, Brushes.DarkBlue, margin + 450, yPos);
                        yPos += 25;
                        g.DrawString("NIC Number:    " + nic, regularFont, Brushes.Black, margin + 15, yPos);
                        yPos += 25;
                        g.DrawString("Contact:       " + pContact, regularFont, Brushes.Black, margin + 15, yPos);
                        yPos += 25;
                        g.DrawString("Address:       " + pAddress, regularFont, Brushes.Black, margin + 15, yPos);
                        yPos += 25;
                        g.DrawString("Record ID:     " + recordId, regularFont, Brushes.Black, margin + 15, yPos);
                        yPos += 25;
                        g.DrawString("Date Created:  " + date, regularFont, Brushes.Black, margin + 15, yPos);
                        
                        yPos += 70;

                        // Diagnosis
                        g.DrawString("DIAGNOSIS", subTitleFont, Brushes.DarkBlue, margin, yPos);
                        yPos += 30;
                        RectangleF diagRect = new RectangleF(margin, yPos, width, 150);
                        g.DrawString(diagnosis, regularFont, Brushes.Black, diagRect);
                        
                        yPos += 170;

                        // Prescription
                        g.DrawString("PRESCRIPTION", subTitleFont, Brushes.DarkBlue, margin, yPos);
                        yPos += 30;
                        RectangleF presRect = new RectangleF(margin, yPos, width, 150);
                        g.DrawString(prescription, regularFont, Brushes.Black, presRect);

                        yPos += 200;
                        
                        // Footer
                        g.DrawLine(new Pen(Color.LightGray, 1), margin, yPos, margin + width, yPos);
                        yPos += 15;
                        g.DrawString("Report generated automatically by Secure Medical System on " + DateTime.Now.ToString("f"), new Font("Segoe UI", 9, FontStyle.Italic), Brushes.Gray, margin, yPos);
                        
                        // Signature line
                        yPos += 60;
                        g.DrawLine(Pens.Black, e.PageBounds.Width - margin - 200, yPos, e.PageBounds.Width - margin, yPos);
                        g.DrawString("Authorized Signature", new Font("Segoe UI", 10), Brushes.Black, e.PageBounds.Width - margin - 180, yPos + 10);
                    };

                    pd.Print();
                    new ToastNotification("PDF Report generated successfully!", ToastType.Success).Show();
                }
                catch (Exception ex)
                {
                    new ToastNotification("Failed to create PDF. Ensure 'Microsoft Print to PDF' is available.", ToastType.Error).Show();
                }
            }
        }
    }
}
