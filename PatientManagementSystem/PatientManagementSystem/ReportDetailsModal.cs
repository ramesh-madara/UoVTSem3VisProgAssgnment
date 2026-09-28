using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public class ReportDetailsModal : Form
    {
        private string patientName;
        private string nic;
        private int reportId;
        private string templateName;
        private string date;

        private DataGridView dgvResults;

        public ReportDetailsModal(string name, string nic, int reportId, string templateName, string date)
        {
            this.patientName = name;
            this.nic = nic;
            this.reportId = reportId;
            this.templateName = templateName;
            this.date = date;

            this.Text = "Medical Report Details";
            this.Size = new Size(700, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            this.Font = new Font("Segoe UI", 10f);

            InitializeUI();
            UITheme.ApplyTheme(this);
            LoadResults();
        }

        private void InitializeUI()
        {
            Label lblHeader = new Label
            {
                Text = templateName.ToUpper() + " REPORT",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(30, 30),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            Panel pnlLine = new Panel
            {
                BackColor = Color.FromArgb(52, 152, 219),
                Location = new Point(30, 70),
                Size = new Size(620, 3)
            };
            this.Controls.Add(pnlLine);

            int currentY = 90;

            AddField("Patient Name:", patientName, ref currentY);
            AddField("NIC Number:", nic, ref currentY);
            AddField("Report ID:", reportId.ToString(), ref currentY);
            AddField("Date:", date, ref currentY);

            currentY += 10;

            dgvResults = new DataGridView
            {
                Location = new Point(30, currentY),
                Size = new Size(620, 300),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvResults.Columns.Add("Field", "Test Parameter");
            dgvResults.Columns.Add("Result", "Result");
            dgvResults.Columns.Add("Unit", "Unit");
            dgvResults.Columns.Add("RefRange", "Reference Range");
            dgvResults.Columns.Add("Flag", "Flag");

            dgvResults.CellFormatting += DgvResults_CellFormatting;

            this.Controls.Add(dgvResults);

            Button btnClose = new Button
            {
                Text = "Close",
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(200, currentY + 320)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();
            this.Controls.Add(btnClose);

            Button btnExport = new Button
            {
                Text = "Export PDF",
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                Size = new Size(120, 40),
                Location = new Point(350, currentY + 320)
            };
            btnExport.FlatAppearance.BorderSize = 0;
            btnExport.Click += (s, e) => ExportToPdf();
            this.Controls.Add(btnExport);
        }

        private void AddField(string labelText, string valueText, ref int y)
        {
            Label lbl = new Label { Text = labelText, Font = new Font("Segoe UI", 10f, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(30, y), AutoSize = true };
            Label val = new Label { Text = valueText, Font = new Font("Segoe UI", 11f), ForeColor = Color.Black, Location = new Point(150, y), AutoSize = true };
            this.Controls.Add(lbl);
            this.Controls.Add(val);
            y += 35;
        }

        private void LoadResults()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    string query = @"
                        SELECT TF.FieldName, TF.FieldType, TF.MinValue, TF.MaxValue, TF.Unit, TF.NormalBoolean, RR.EncryptedValue
                        FROM ReportResults RR
                        INNER JOIN TemplateFields TF ON RR.FieldID = TF.FieldID
                        WHERE RR.ReportID = @RID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RID", reportId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        string fieldName = row["FieldName"].ToString();
                        string fieldType = row["FieldType"].ToString();
                        string unit = row["Unit"] != DBNull.Value ? row["Unit"].ToString() : "";
                        string encryptedValue = row["EncryptedValue"].ToString();
                        string decryptedValue = Decrypt(encryptedValue);

                        string refRange = "";
                        string flag = "";

                        if (fieldType == "Numeric")
                        {
                            decimal min = row["MinValue"] != DBNull.Value ? Convert.ToDecimal(row["MinValue"]) : decimal.MinValue;
                            decimal max = row["MaxValue"] != DBNull.Value ? Convert.ToDecimal(row["MaxValue"]) : decimal.MaxValue;
                            
                            refRange = string.Format("{0} - {1}", 
                                min == decimal.MinValue ? "" : min.ToString(), 
                                max == decimal.MaxValue ? "" : max.ToString());
                            
                            if (min != decimal.MinValue || max != decimal.MaxValue)
                            {
                                decimal val;
                                if (decimal.TryParse(decryptedValue, out val))
                                {
                                    if (val < min) flag = "LOW";
                                    else if (val > max) flag = "HIGH";
                                }
                            }
                        }
                        else if (fieldType == "Boolean")
                        {
                            string normalBool = row["NormalBoolean"] != DBNull.Value ? row["NormalBoolean"].ToString() : "";
                            refRange = normalBool;
                            if (!string.IsNullOrEmpty(normalBool) && !decryptedValue.Equals(normalBool, StringComparison.OrdinalIgnoreCase))
                            {
                                flag = "ABNORMAL";
                            }
                        }

                        dgvResults.Rows.Add(fieldName, decryptedValue, unit, refRange, flag);
                    }
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to load report results: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void DgvResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string flag = dgvResults.Rows[e.RowIndex].Cells["Flag"].Value != null ? dgvResults.Rows[e.RowIndex].Cells["Flag"].Value.ToString() : "";
                if (!string.IsNullOrEmpty(flag))
                {
                    dgvResults.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 200, 200); // Light Red
                    dgvResults.Rows[e.RowIndex].DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 150, 150);
                    dgvResults.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.DarkRed;
                    dgvResults.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.DarkRed;
                }
            }
        }

        public void ExportToPdf()
        {
            string defaultFileName = string.Format("{0}_Report_{1}_{2}.pdf", templateName.Replace(" ", ""), patientName.Replace(" ", ""), date.Replace(":", "").Replace("/", "").Replace(" ", ""));

            string pContact = "", pAddress = "", pGender = "", pBlood = "", pAge = "";
            try {
                using (SqlConnection conn = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=medicaldb;Integrated Security=True;")) {
                    SqlCommand cmd = new SqlCommand("SELECT ContactNumber, Address, Gender, BloodGroup, Age FROM Patients WHERE NIC = @NIC", conn);
                    cmd.Parameters.AddWithValue("@NIC", this.nic);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        if (reader.Read()) {
                            pContact = reader["ContactNumber"].ToString();
                            pAddress = reader["Address"].ToString();
                            pGender = reader["Gender"].ToString();
                            pBlood = reader["BloodGroup"].ToString();
                            pAge = reader["Age"].ToString();
                        }
                    }
                }
            } catch {}

            try
            {
                System.Drawing.Printing.PrintDocument pd = new System.Drawing.Printing.PrintDocument();

                pd.PrintPage += (sender, e) =>
                {
                    Graphics g = e.Graphics;
                    int yPos = 40;
                    int margin = 50;
                    int width = e.PageBounds.Width - (margin * 2);

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
                    
                    g.DrawString("OFFICIAL REPORT: " + templateName.ToUpper(), subTitleFont, Brushes.DarkBlue, margin, yPos);
                    yPos += 25;

                    g.DrawLine(new Pen(Color.DarkBlue, 3), margin, yPos, margin + width, yPos);
                    yPos += 25;

                    // Details Box
                    g.FillRectangle(Brushes.WhiteSmoke, margin, yPos, width, 175);
                    g.DrawRectangle(Pens.LightGray, margin, yPos, width, 175);

                    yPos += 15;
                    g.DrawString("Patient Name:  " + patientName, boldFont, Brushes.Black, margin + 15, yPos);
                    g.DrawString("Gender: " + pGender + "   Age: " + pAge + "   Blood: " + pBlood, boldFont, Brushes.DarkBlue, margin + 450, yPos);
                    yPos += 25;
                    g.DrawString("NIC Number:    " + nic, regularFont, Brushes.Black, margin + 15, yPos);
                    yPos += 25;
                    g.DrawString("Contact:       " + pContact, regularFont, Brushes.Black, margin + 15, yPos);
                    yPos += 25;
                    g.DrawString("Address:       " + pAddress, regularFont, Brushes.Black, margin + 15, yPos);
                    yPos += 25;
                    g.DrawString("Report ID:     " + reportId, regularFont, Brushes.Black, margin + 15, yPos);
                    yPos += 25;
                    g.DrawString("Date:          " + date, regularFont, Brushes.Black, margin + 15, yPos);
                    
                    yPos += 60;
                    g.DrawString("TEST RESULTS", subTitleFont, Brushes.Black, margin, yPos);
                    yPos += 30;

                    // Table Header
                    g.FillRectangle(Brushes.DarkBlue, margin, yPos, width, 30);
                    g.DrawString("Parameter", boldFont, Brushes.White, margin + 10, yPos + 5);
                    g.DrawString("Result", boldFont, Brushes.White, margin + 200, yPos + 5);
                    g.DrawString("Ref Range", boldFont, Brushes.White, margin + 350, yPos + 5);
                    g.DrawString("Flag", boldFont, Brushes.White, margin + 500, yPos + 5);
                    yPos += 40;

                    foreach (DataGridViewRow row in dgvResults.Rows)
                    {
                        string fName = row.Cells["Field"].Value.ToString();
                        string res = row.Cells["Result"].Value.ToString();
                        string unit = row.Cells["Unit"].Value.ToString();
                        string refRange = row.Cells["RefRange"].Value.ToString();
                        string flag = row.Cells["Flag"].Value != null ? row.Cells["Flag"].Value.ToString() : "";

                        string displayResult = res + (string.IsNullOrEmpty(unit) ? "" : " " + unit);
                        
                        Brush textBrush = Brushes.Black;
                        if (!string.IsNullOrEmpty(flag))
                        {
                            textBrush = Brushes.Red;
                            displayResult += " *"; // Print asterisk for abnormalities
                        }

                        g.DrawString(fName, regularFont, Brushes.Black, margin + 10, yPos);
                        g.DrawString(displayResult, boldFont, textBrush, margin + 200, yPos);
                        g.DrawString(refRange, regularFont, Brushes.Gray, margin + 350, yPos);
                        g.DrawString(flag, boldFont, textBrush, margin + 500, yPos);
                        
                        g.DrawLine(Pens.LightGray, margin, yPos + 25, margin + width, yPos + 25);
                        yPos += 35;
                    }

                    yPos += 50;
                    g.DrawString("Authorized Signature: _______________________", regularFont, Brushes.Black, margin, yPos);
                };

                PdfPreviewModal preview = new PdfPreviewModal(pd, defaultFileName);
                preview.ShowDialog();
            }
            catch (Exception ex)
            {
                new ToastNotification("PDF Generation failed: " + ex.Message, ToastType.Error).Show();
            }
        }

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
}
