using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public class AddMedicalReportForm : Form
    {
        private int patientId;
        private int doctorId;
        private string patientName;

        private ComboBox cmbTemplates;
        private FlowLayoutPanel flpDynamicControls;
        private Button btnSaveReport;

        public AddMedicalReportForm(int pId, string pName, int dId)
        {
            this.patientId = pId;
            this.patientName = pName;
            this.doctorId = dId;

            this.Text = "Add Medical Report - " + pName;
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.White;

            InitializeUI();
            UITheme.ApplyTheme(this);
            LoadTemplates();
        }

        private void InitializeUI()
        {
            Label lblHeader = new Label
            {
                Text = "DYNAMIC MEDICAL REPORT: " + patientName,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            Label lblTemplate = new Label { Text = "Select Template:", Location = new Point(20, 70), AutoSize = true, Font = new Font("Segoe UI", 10) };
            cmbTemplates = new ComboBox { Location = new Point(150, 68), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbTemplates.SelectedIndexChanged += CmbTemplates_SelectedIndexChanged;

            this.Controls.Add(lblTemplate);
            this.Controls.Add(cmbTemplates);

            flpDynamicControls = new FlowLayoutPanel
            {
                Location = new Point(20, 110),
                Size = new Size(540, 480),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };
            this.Controls.Add(flpDynamicControls);

            btnSaveReport = new Button { Text = "Save Report", Location = new Point(410, 600), Width = 150, Height = 40, Enabled = false, Anchor = AnchorStyles.Bottom | AnchorStyles.Right };
            btnSaveReport.Click += BtnSaveReport_Click;
            this.Controls.Add(btnSaveReport);
        }

        private void LoadTemplates()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    string query = "SELECT TemplateID, TemplateName FROM ReportTemplates WHERE IsActive = 1";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cmbTemplates.DataSource = dt;
                    cmbTemplates.DisplayMember = "TemplateName";
                    cmbTemplates.ValueMember = "TemplateID";
                    cmbTemplates.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to load templates: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void CmbTemplates_SelectedIndexChanged(object sender, EventArgs e)
        {
            flpDynamicControls.Controls.Clear();
            btnSaveReport.Enabled = false;

            if (cmbTemplates.SelectedIndex == -1 || cmbTemplates.SelectedValue == null || cmbTemplates.SelectedValue is System.Data.DataRowView) return;

            int templateId = Convert.ToInt32(cmbTemplates.SelectedValue);

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    string query = "SELECT * FROM TemplateFields WHERE TemplateID = @TID";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TID", templateId);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        GenerateFieldControl(row);
                    }
                    
                    if (dt.Rows.Count > 0)
                        btnSaveReport.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to load template fields: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void GenerateFieldControl(DataRow field)
        {
            int fieldId = Convert.ToInt32(field["FieldID"]);
            string fieldName = field["FieldName"].ToString();
            string fieldType = field["FieldType"].ToString();
            string unit = field["Unit"] != DBNull.Value ? field["Unit"].ToString() : "";

            Panel pnl = new Panel { Width = 500, Height = 60 };

            Label lbl = new Label
            {
                Text = fieldName + (string.IsNullOrEmpty(unit) ? "" : " (" + unit + ")"),
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            pnl.Controls.Add(lbl);

            if (fieldType == "Numeric")
            {
                TextBox txt = new TextBox { Location = new Point(10, 35), Width = 300, Tag = fieldId };
                
                decimal min = field["MinValue"] != DBNull.Value ? Convert.ToDecimal(field["MinValue"]) : decimal.MinValue;
                decimal max = field["MaxValue"] != DBNull.Value ? Convert.ToDecimal(field["MaxValue"]) : decimal.MaxValue;

                txt.TextChanged += (s, e) =>
                {
                    decimal val;
                    if (decimal.TryParse(txt.Text, out val))
                    {
                        if (val < min || val > max)
                            txt.ForeColor = Color.Red;
                        else
                            txt.ForeColor = Color.Black;
                    }
                    else
                    {
                        txt.ForeColor = Color.Black;
                    }
                };

                txt.KeyPress += (s, e) =>
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
                    {
                        e.Handled = true;
                    }
                };

                pnl.Controls.Add(txt);
            }
            else if (fieldType == "Boolean")
            {
                ComboBox cmb = new ComboBox { Location = new Point(10, 35), Width = 300, DropDownStyle = ComboBoxStyle.DropDownList, Tag = fieldId };
                cmb.Items.Add("Positive");
                cmb.Items.Add("Negative");
                cmb.Items.Add("Normal");
                cmb.Items.Add("Abnormal");
                pnl.Controls.Add(cmb);
            }
            else if (fieldType == "Text")
            {
                pnl.Height = 100;
                TextBox txtMulti = new TextBox { Location = new Point(10, 35), Width = 480, Height = 55, Multiline = true, Tag = fieldId };
                pnl.Controls.Add(txtMulti);
            }

            flpDynamicControls.Controls.Add(pnl);
        }

        private void BtnSaveReport_Click(object sender, EventArgs e)
        {
            if (cmbTemplates.SelectedIndex == -1) return;
            int templateId = Convert.ToInt32(cmbTemplates.SelectedValue);

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            string reportQuery = "INSERT INTO PatientReports (PatientID, TemplateID, DoctorID, ReportDate) OUTPUT INSERTED.ReportID VALUES (@PID, @TID, @DID, GETDATE())";
                            int reportId;
                            using (SqlCommand cmd = new SqlCommand(reportQuery, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@PID", patientId);
                                cmd.Parameters.AddWithValue("@TID", templateId);
                                cmd.Parameters.AddWithValue("@DID", doctorId);
                                reportId = (int)cmd.ExecuteScalar();
                            }

                            string resultQuery = "INSERT INTO ReportResults (ReportID, FieldID, EncryptedValue) VALUES (@RID, @FID, @EncVal)";
                            foreach (Control pnl in flpDynamicControls.Controls)
                            {
                                if (pnl is Panel)
                                {
                                    foreach (Control c in pnl.Controls)
                                    {
                                        if (c.Tag != null)
                                        {
                                            int fieldId = (int)c.Tag;
                                            string rawValue = "";

                                            if (c is TextBox) rawValue = ((TextBox)c).Text.Trim();
                                            else if (c is ComboBox) rawValue = ((ComboBox)c).Text;

                                            string encryptedValue = Encrypt(rawValue);

                                            using (SqlCommand cmdResult = new SqlCommand(resultQuery, conn, transaction))
                                            {
                                                cmdResult.Parameters.AddWithValue("@RID", reportId);
                                                cmdResult.Parameters.AddWithValue("@FID", fieldId);
                                                cmdResult.Parameters.AddWithValue("@EncVal", encryptedValue);
                                                cmdResult.ExecuteNonQuery();
                                            }
                                        }
                                    }
                                }
                            }

                            transaction.Commit();
                            new ToastNotification("Medical Report securely saved!", ToastType.Success).Show();
                            if (!this.Modal)
                            {
                                cmbTemplates.SelectedIndex = -1;
                            }
                            else
                            {
                                this.Close();
                            }
                        }
                        catch (Exception innerEx)
                        {
                            transaction.Rollback();
                            throw new Exception("Transaction rolled back. " + innerEx.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to save report: " + ex.Message, ToastType.Error).Show();
            }
        }

        private string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;
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
    }
}
