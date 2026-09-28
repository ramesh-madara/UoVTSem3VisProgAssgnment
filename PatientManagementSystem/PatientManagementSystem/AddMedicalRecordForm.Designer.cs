namespace PatientManagementSystem
{
    partial class AddMedicalRecordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
   

        #endregion

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.cmbSearchPatient = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.lblPrescription = new System.Windows.Forms.Label();
            this.txtPrescription = new System.Windows.Forms.TextBox();
            this.btnSaveRecord = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlSearchGroup = new System.Windows.Forms.Panel();
            this.pnlSearchGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(40, 25);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(385, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SECURE MEDICAL RECORD Entry";
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.Location = new System.Drawing.Point(13, 18);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(186, 21);
            this.lblSearch.TabIndex = 1;
            this.lblSearch.Text = "Search by Name/NIC *";
            // 
            // cmbSearchPatient
            // 
            this.cmbSearchPatient.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSearchPatient.Location = new System.Drawing.Point(200, 14);
            this.cmbSearchPatient.Margin = new System.Windows.Forms.Padding(4);
            this.cmbSearchPatient.Name = "cmbSearchPatient";
            this.cmbSearchPatient.Size = new System.Drawing.Size(300, 31);
            this.cmbSearchPatient.TabIndex = 2;
            this.cmbSearchPatient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(520, 11);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(160, 36);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblPatientInfo
            // 
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPatientInfo.ForeColor = System.Drawing.Color.Gray;
            this.lblPatientInfo.Location = new System.Drawing.Point(47, 154);
            this.lblPatientInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(404, 23);
            this.lblPatientInfo.TabIndex = 4;
            this.lblPatientInfo.Text = "Search for a patient using their Name or NIC above to begin.";
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiagnosis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblDiagnosis.Location = new System.Drawing.Point(47, 197);
            this.lblDiagnosis.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(222, 23);
            this.lblDiagnosis.TabIndex = 5;
            this.lblDiagnosis.Text = "Diagnosis (AES Encrypted)";
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDiagnosis.Location = new System.Drawing.Point(47, 228);
            this.txtDiagnosis.Margin = new System.Windows.Forms.Padding(4);
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDiagnosis.Size = new System.Drawing.Size(905, 110);
            this.txtDiagnosis.TabIndex = 6;
            // 
            // lblPrescription
            // 
            this.lblPrescription.AutoSize = true;
            this.lblPrescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrescription.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblPrescription.Location = new System.Drawing.Point(47, 357);
            this.lblPrescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPrescription.Name = "lblPrescription";
            this.lblPrescription.Size = new System.Drawing.Size(301, 23);
            this.lblPrescription.TabIndex = 7;
            this.lblPrescription.Text = "Prescription Details (AES Encrypted)";
            // 
            // txtPrescription
            // 
            this.txtPrescription.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrescription.Location = new System.Drawing.Point(47, 388);
            this.txtPrescription.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrescription.Multiline = true;
            this.txtPrescription.Name = "txtPrescription";
            this.txtPrescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPrescription.Size = new System.Drawing.Size(905, 110);
            this.txtPrescription.TabIndex = 8;
            // 
            // btnSaveRecord
            // 
            this.btnSaveRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSaveRecord.Enabled = false;
            this.btnSaveRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveRecord.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveRecord.ForeColor = System.Drawing.Color.White;
            this.btnSaveRecord.Location = new System.Drawing.Point(553, 523);
            this.btnSaveRecord.Margin = new System.Windows.Forms.Padding(4);
            this.btnSaveRecord.Name = "btnSaveRecord";
            this.btnSaveRecord.Size = new System.Drawing.Size(200, 49);
            this.btnSaveRecord.TabIndex = 9;
            this.btnSaveRecord.Text = "🔐 Save Encrypted";
            this.btnSaveRecord.UseVisualStyleBackColor = false;
            this.btnSaveRecord.Click += new System.EventHandler(this.btnSaveRecord_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnCancel.Location = new System.Drawing.Point(780, 523);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(173, 49);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Clear";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlSearchGroup
            // 
            this.pnlSearchGroup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlSearchGroup.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSearchGroup.Controls.Add(this.btnSearch);
            this.pnlSearchGroup.Controls.Add(this.cmbSearchPatient);
            this.pnlSearchGroup.Controls.Add(this.lblSearch);
            this.pnlSearchGroup.Location = new System.Drawing.Point(47, 74);
            this.pnlSearchGroup.Margin = new System.Windows.Forms.Padding(4);
            this.pnlSearchGroup.Name = "pnlSearchGroup";
            this.pnlSearchGroup.Size = new System.Drawing.Size(906, 61);
            this.pnlSearchGroup.TabIndex = 11;
            // 
            // AddMedicalRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(997, 593);
            this.Controls.Add(this.pnlSearchGroup);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSaveRecord);
            this.Controls.Add(this.txtPrescription);
            this.Controls.Add(this.lblPrescription);
            this.Controls.Add(this.txtDiagnosis);
            this.Controls.Add(this.lblDiagnosis);
            this.Controls.Add(this.lblPatientInfo);
            this.Controls.Add(this.lblTitle);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AddMedicalRecordForm";
            this.Text = "Add Medical Record";
            this.Load += new System.EventHandler(this.AddMedicalRecordForm_Load_1);
            this.pnlSearchGroup.ResumeLayout(false);
            this.pnlSearchGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.ComboBox cmbSearchPatient;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.Label lblPrescription;
        private System.Windows.Forms.TextBox txtPrescription;
        private System.Windows.Forms.Button btnSaveRecord;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlSearchGroup;

    }
}