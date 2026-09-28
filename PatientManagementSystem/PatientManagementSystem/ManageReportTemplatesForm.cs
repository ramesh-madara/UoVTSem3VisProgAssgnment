using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public class ManageReportTemplatesForm : Form
    {
        private DataGridView dgvTemplates;
        private Button btnAddNew;

        public ManageReportTemplatesForm()
        {
            this.Text = "Manage Report Templates";
            this.Size = new Size(800, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.White;

            InitializeUI();
            UITheme.ApplyTheme(this);
            LoadTemplates();
        }

        private void InitializeUI()
        {
            Label lblHeader = new Label
            {
                Text = "REPORT TEMPLATES DASHBOARD",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            btnAddNew = new Button
            {
                Text = "+ Create New Template",
                Location = new Point(580, 20),
                Width = 180,
                Height = 35,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnAddNew.FlatAppearance.BorderSize = 0;
            btnAddNew.Click += (s, e) => 
            {
                TemplateBuilderModal modal = new TemplateBuilderModal(-1, false);
                modal.ShowDialog();
                LoadTemplates();
            };
            this.Controls.Add(btnAddNew);

            dgvTemplates = new DataGridView
            {
                Location = new Point(20, 80),
                Size = new Size(740, 450),
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false
            };
            dgvTemplates.CellContentClick += DgvTemplates_CellContentClick;
            dgvTemplates.CellFormatting += DgvTemplates_CellFormatting;

            this.Controls.Add(dgvTemplates);
        }

        private void LoadTemplates()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    string query = "SELECT TemplateID AS 'ID', TemplateName AS 'Template Name', 'Active' AS 'Status' FROM ReportTemplates WHERE IsActive = 1";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvTemplates.DataSource = dt;

                    if (!dgvTemplates.Columns.Contains("ViewAction"))
                    {
                        DataGridViewButtonColumn btnView = new DataGridViewButtonColumn();
                        btnView.Name = "ViewAction";
                        btnView.HeaderText = "View";
                        btnView.Text = "👁";
                        btnView.UseColumnTextForButtonValue = true;
                        btnView.FlatStyle = FlatStyle.Flat;
                        dgvTemplates.Columns.Add(btnView);
                    }

                    if (!dgvTemplates.Columns.Contains("EditAction"))
                    {
                        DataGridViewButtonColumn btnEdit = new DataGridViewButtonColumn();
                        btnEdit.Name = "EditAction";
                        btnEdit.HeaderText = "Edit";
                        btnEdit.Text = "✏️";
                        btnEdit.UseColumnTextForButtonValue = true;
                        btnEdit.FlatStyle = FlatStyle.Flat;
                        dgvTemplates.Columns.Add(btnEdit);
                    }

                    if (!dgvTemplates.Columns.Contains("DeleteAction"))
                    {
                        DataGridViewButtonColumn btnDel = new DataGridViewButtonColumn();
                        btnDel.Name = "DeleteAction";
                        btnDel.HeaderText = "Delete";
                        btnDel.Text = "🗑️";
                        btnDel.UseColumnTextForButtonValue = true;
                        btnDel.FlatStyle = FlatStyle.Flat;
                        dgvTemplates.Columns.Add(btnDel);
                    }
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to load templates: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void DgvTemplates_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dgvTemplates.Columns[e.ColumnIndex].Name == "ViewAction" || dgvTemplates.Columns[e.ColumnIndex].Name == "EditAction")
                {
                    e.CellStyle.BackColor = Color.FromArgb(52, 152, 219);
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
                    e.CellStyle.SelectionForeColor = Color.White;
                }
                else if (dgvTemplates.Columns[e.ColumnIndex].Name == "DeleteAction")
                {
                    e.CellStyle.BackColor = Color.FromArgb(231, 76, 60); // Red
                    e.CellStyle.ForeColor = Color.White;
                    e.CellStyle.SelectionBackColor = Color.FromArgb(231, 76, 60);
                    e.CellStyle.SelectionForeColor = Color.White;
                }
            }
        }

        private void DgvTemplates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string colName = dgvTemplates.Columns[e.ColumnIndex].Name;
                if (colName == "ViewAction" || colName == "EditAction")
                {
                    int templateId = Convert.ToInt32(dgvTemplates.Rows[e.RowIndex].Cells["ID"].Value);
                    bool isReadOnly = colName == "ViewAction";

                    TemplateBuilderModal modal = new TemplateBuilderModal(templateId, isReadOnly);
                    modal.ShowDialog();
                    LoadTemplates();
                }
                else if (colName == "DeleteAction")
                {
                    int templateId = Convert.ToInt32(dgvTemplates.Rows[e.RowIndex].Cells["ID"].Value);
                    if (MessageBox.Show("Are you sure you want to delete this template?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                        {
                            conn.Open();
                            // Soft delete to preserve existing reports
                            using (SqlCommand cmd = new SqlCommand("UPDATE ReportTemplates SET IsActive = 0 WHERE TemplateID = @TID", conn))
                            {
                                cmd.Parameters.AddWithValue("@TID", templateId);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        LoadTemplates();
                    }
                }
            }
        }
    }

    public class TemplateBuilderModal : Form
    {
        private int templateId;
        private bool isReadOnly;

        private TextBox txtTemplateName;
        private FlowLayoutPanel flpFields;
        private Button btnAddField;
        private Button btnAction;
        private Label lblHeader;

        public TemplateBuilderModal(int templateId, bool isReadOnly)
        {
            this.templateId = templateId;
            this.isReadOnly = isReadOnly;

            this.Text = templateId == -1 ? "Create Template" : (isReadOnly ? "View Template" : "Edit Template");
            this.Size = new Size(820, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            InitializeUI();
            UITheme.ApplyTheme(this);

            if (templateId != -1)
            {
                LoadExistingData();
            }
        }

        private void InitializeUI()
        {
            lblHeader = new Label
            {
                Text = templateId == -1 ? "BUILD NEW TEMPLATE" : (isReadOnly ? "VIEW TEMPLATE" : "EDIT TEMPLATE"),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(44, 62, 80),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(lblHeader);

            Label lblName = new Label { Text = "Template Name:", Location = new Point(20, 70), AutoSize = true, Font = new Font("Segoe UI", 10) };
            txtTemplateName = new TextBox { Location = new Point(140, 68), Width = 300, ReadOnly = isReadOnly };
            
            this.Controls.Add(lblName);
            this.Controls.Add(txtTemplateName);

            flpFields = new FlowLayoutPanel
            {
                Location = new Point(20, 110),
                Size = new Size(760, 390),
                AutoScroll = true,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(245, 246, 250)
            };
            this.Controls.Add(flpFields);

            btnAddField = new Button 
            { 
                Text = "+ Add Field", 
                Location = new Point(20, 510), 
                Width = 150, 
                Height = 40, 
                BackColor = Color.FromArgb(46, 204, 113), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Visible = !isReadOnly
            };
            btnAddField.FlatAppearance.BorderSize = 0;
            btnAddField.Click += (s, e) => AddFieldControlPanel();
            this.Controls.Add(btnAddField);

            btnAction = new Button 
            { 
                Text = isReadOnly ? "Edit Template" : "Save Template", 
                Location = new Point(630, 510), 
                Width = 150, 
                Height = 40, 
                BackColor = isReadOnly ? Color.FromArgb(243, 156, 18) : Color.FromArgb(52, 152, 219), // Orange for Edit, Blue for Save
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat 
            };
            btnAction.FlatAppearance.BorderSize = 0;
            btnAction.Click += BtnAction_Click;
            this.Controls.Add(btnAction);
        }

        private void AddFieldControlPanel(string fName = "", string fType = "Numeric", string min = "", string max = "", string unit = "", string normal = "")
        {
            Panel pnl = new Panel { Width = 730, Height = 70, BackColor = Color.White, BorderStyle = BorderStyle.FixedSingle };
            
            Label lblName = new Label { Text = "Name:", Location = new Point(10, 10), AutoSize = true };
            TextBox txtName = new TextBox { Name = "txtName", Text = fName, Location = new Point(10, 30), Width = 140, ReadOnly = isReadOnly };
            pnl.Controls.Add(lblName); pnl.Controls.Add(txtName);

            Label lblType = new Label { Text = "Type:", Location = new Point(160, 10), AutoSize = true };
            ComboBox cmbType = new ComboBox { Name = "cmbType", Location = new Point(160, 30), Width = 90, DropDownStyle = ComboBoxStyle.DropDownList, Enabled = !isReadOnly };
            cmbType.Items.AddRange(new object[] { "Numeric", "Boolean", "Text" });
            cmbType.SelectedItem = string.IsNullOrEmpty(fType) ? "Numeric" : fType;
            pnl.Controls.Add(lblType); pnl.Controls.Add(cmbType);

            Label lblMin = new Label { Text = "Min:", Location = new Point(260, 10), AutoSize = true };
            TextBox txtMin = new TextBox { Name = "txtMin", Text = min, Location = new Point(260, 30), Width = 60, ReadOnly = isReadOnly };
            pnl.Controls.Add(lblMin); pnl.Controls.Add(txtMin);

            Label lblMax = new Label { Text = "Max:", Location = new Point(330, 10), AutoSize = true };
            TextBox txtMax = new TextBox { Name = "txtMax", Text = max, Location = new Point(330, 30), Width = 60, ReadOnly = isReadOnly };
            pnl.Controls.Add(lblMax); pnl.Controls.Add(txtMax);

            Label lblUnit = new Label { Text = "Unit:", Location = new Point(400, 10), AutoSize = true };
            TextBox txtUnit = new TextBox { Name = "txtUnit", Text = unit, Location = new Point(400, 30), Width = 70, ReadOnly = isReadOnly };
            pnl.Controls.Add(lblUnit); pnl.Controls.Add(txtUnit);

            Label lblNorm = new Label { Text = "Normal:", Location = new Point(480, 10), AutoSize = true };
            TextBox txtNorm = new TextBox { Name = "txtNorm", Text = normal, Location = new Point(480, 30), Width = 90, ReadOnly = isReadOnly };
            pnl.Controls.Add(lblNorm); pnl.Controls.Add(txtNorm);

            Action toggleVisibility = () => 
            {
                bool isNum = cmbType.Text == "Numeric";
                bool isBool = cmbType.Text == "Boolean";
                
                txtMin.Visible = lblMin.Visible = isNum;
                txtMax.Visible = lblMax.Visible = isNum;
                txtUnit.Visible = lblUnit.Visible = isNum;
                txtNorm.Visible = lblNorm.Visible = isBool;
            };
            cmbType.SelectedIndexChanged += (s, e) => toggleVisibility();
            toggleVisibility();

            Button btnDel = new Button { Name = "btnDel", Text = "✖", Location = new Point(680, 20), Width = 40, Height = 40, FlatStyle = FlatStyle.Flat, ForeColor = Color.Red, BackColor = Color.White, Visible = !isReadOnly };
            btnDel.FlatAppearance.BorderSize = 0;
            btnDel.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
            btnDel.Click += (s, e) => flpFields.Controls.Remove(pnl);
            pnl.Controls.Add(btnDel);

            flpFields.Controls.Add(pnl);
        }

        private void LoadExistingData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("SELECT TemplateName FROM ReportTemplates WHERE TemplateID = @TID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TID", templateId);
                        object result = cmd.ExecuteScalar();
                        txtTemplateName.Text = result != null ? result.ToString() : "";
                    }

                    using (SqlCommand cmd = new SqlCommand("SELECT FieldName, FieldType, MinValue, MaxValue, Unit, NormalBoolean FROM TemplateFields WHERE TemplateID = @TID", conn))
                    {
                        cmd.Parameters.AddWithValue("@TID", templateId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AddFieldControlPanel(
                                    reader["FieldName"].ToString(),
                                    reader["FieldType"].ToString(),
                                    reader["MinValue"] != DBNull.Value ? reader["MinValue"].ToString() : "",
                                    reader["MaxValue"] != DBNull.Value ? reader["MaxValue"].ToString() : "",
                                    reader["Unit"] != DBNull.Value ? reader["Unit"].ToString() : "",
                                    reader["NormalBoolean"] != DBNull.Value ? reader["NormalBoolean"].ToString() : ""
                                );
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Failed to load template data: " + ex.Message, ToastType.Error).Show();
            }
        }

        private void BtnAction_Click(object sender, EventArgs e)
        {
            if (isReadOnly)
            {
                EnableEditMode();
            }
            else
            {
                SaveTemplate();
            }
        }

        private void EnableEditMode()
        {
            isReadOnly = false;
            this.Text = "Edit Template";
            lblHeader.Text = "EDIT TEMPLATE";
            txtTemplateName.ReadOnly = false;
            
            btnAddField.Visible = true;
            btnAction.Text = "Save Template";
            btnAction.BackColor = Color.FromArgb(52, 152, 219); // Blue

            foreach (Control c in flpFields.Controls)
            {
                Panel pnl = c as Panel;
                if (pnl != null)
                {
                    ((TextBox)pnl.Controls["txtName"]).ReadOnly = false;
                    ((ComboBox)pnl.Controls["cmbType"]).Enabled = true;
                    ((TextBox)pnl.Controls["txtMin"]).ReadOnly = false;
                    ((TextBox)pnl.Controls["txtMax"]).ReadOnly = false;
                    ((TextBox)pnl.Controls["txtUnit"]).ReadOnly = false;
                    ((TextBox)pnl.Controls["txtNorm"]).ReadOnly = false;
                    
                    if (pnl.Controls.ContainsKey("btnDel"))
                    {
                        pnl.Controls["btnDel"].Visible = true;
                    }
                }
            }
        }

        private void SaveTemplate()
        {
            string templateName = txtTemplateName.Text.Trim();
            if (string.IsNullOrEmpty(templateName))
            {
                new ToastNotification("Please enter a template name.", ToastType.Warning).Show();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(@"Server=.\SQLEXPRESS;Database=medicaldb;Integrated Security=True;"))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int activeTemplateId = templateId;

                            if (templateId == -1)
                            {
                                using (SqlCommand cmd = new SqlCommand("INSERT INTO ReportTemplates (TemplateName, IsActive) OUTPUT INSERTED.TemplateID VALUES (@Name, 1)", conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Name", templateName);
                                    activeTemplateId = (int)cmd.ExecuteScalar();
                                }
                            }
                            else
                            {
                                using (SqlCommand cmd = new SqlCommand("UPDATE ReportTemplates SET TemplateName = @Name WHERE TemplateID = @TID", conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@Name", templateName);
                                    cmd.Parameters.AddWithValue("@TID", activeTemplateId);
                                    cmd.ExecuteNonQuery();
                                }

                                using (SqlCommand cmd = new SqlCommand("DELETE FROM TemplateFields WHERE TemplateID = @TID", conn, transaction))
                                {
                                    cmd.Parameters.AddWithValue("@TID", activeTemplateId);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            foreach (Control c in flpFields.Controls)
                            {
                                if (c is Panel)
                                {
                                    Panel pnl = (Panel)c;
                                    TextBox txtName = (TextBox)pnl.Controls["txtName"];
                                    ComboBox cmbType = (ComboBox)pnl.Controls["cmbType"];
                                    TextBox txtMin = (TextBox)pnl.Controls["txtMin"];
                                    TextBox txtMax = (TextBox)pnl.Controls["txtMax"];
                                    TextBox txtUnit = (TextBox)pnl.Controls["txtUnit"];
                                    TextBox txtNorm = (TextBox)pnl.Controls["txtNorm"];

                                    string fieldName = txtName.Text.Trim();
                                    string fieldType = cmbType.Text;
                                    
                                    if (string.IsNullOrEmpty(fieldName) || string.IsNullOrEmpty(fieldType))
                                    {
                                        throw new Exception("Field Name and Type cannot be empty.");
                                    }

                                    string minVal = txtMin.Text.Trim();
                                    string maxVal = txtMax.Text.Trim();
                                    string unit = txtUnit.Text.Trim();
                                    string normalBool = txtNorm.Text.Trim();

                                    if (fieldType == "Numeric" && (string.IsNullOrEmpty(minVal) || string.IsNullOrEmpty(maxVal)))
                                    {
                                        throw new Exception(string.Format("Numeric field '{0}' must have Min and Max values.", fieldName));
                                    }
                                    if (fieldType == "Boolean" && string.IsNullOrEmpty(normalBool))
                                    {
                                        throw new Exception(string.Format("Boolean field '{0}' must have a Normal Boolean value (e.g. Negative).", fieldName));
                                    }

                                    string query = "INSERT INTO TemplateFields (TemplateID, FieldName, FieldType, MinValue, MaxValue, Unit, NormalBoolean) " +
                                                   "VALUES (@TID, @Name, @Type, @Min, @Max, @Unit, @Norm)";
                                    
                                    using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                                    {
                                        cmd.Parameters.AddWithValue("@TID", activeTemplateId);
                                        cmd.Parameters.AddWithValue("@Name", fieldName);
                                        cmd.Parameters.AddWithValue("@Type", fieldType);
                                        cmd.Parameters.AddWithValue("@Min", string.IsNullOrEmpty(minVal) ? (object)DBNull.Value : Convert.ToDecimal(minVal));
                                        cmd.Parameters.AddWithValue("@Max", string.IsNullOrEmpty(maxVal) ? (object)DBNull.Value : Convert.ToDecimal(maxVal));
                                        cmd.Parameters.AddWithValue("@Unit", string.IsNullOrEmpty(unit) ? (object)DBNull.Value : unit);
                                        cmd.Parameters.AddWithValue("@Norm", string.IsNullOrEmpty(normalBool) ? (object)DBNull.Value : normalBool);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            transaction.Commit();
                            new ToastNotification("Template saved successfully!", ToastType.Success).Show();
                            this.Close();
                        }
                        catch (Exception innerEx)
                        {
                            transaction.Rollback();
                            throw new Exception("Validation failed: " + innerEx.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new ToastNotification("Error saving template: " + ex.Message, ToastType.Error).Show();
            }
        }
    }
}
