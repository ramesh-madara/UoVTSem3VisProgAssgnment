namespace PatientManagementSystem
{
    partial class MainDashboard
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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnManageUsers = new System.Windows.Forms.Button();
            this.btnViewHistory = new System.Windows.Forms.Button();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.btnRegisterPatient = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblClinicTitle = new System.Windows.Forms.Label();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.lblUserStatus = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.lblContentPlaceholder = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnManageUsers);
            this.pnlSidebar.Controls.Add(this.btnViewHistory);
            this.pnlSidebar.Controls.Add(this.btnAddRecord);
            this.pnlSidebar.Controls.Add(this.btnRegisterPatient);
            this.pnlSidebar.Controls.Add(this.pnlLogo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(260, 561);
            this.pnlSidebar.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(0, 511);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(220, 50);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnViewHistory
            // 
            this.btnViewHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnViewHistory.FlatAppearance.BorderSize = 0;
            this.btnViewHistory.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnViewHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewHistory.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewHistory.ForeColor = System.Drawing.Color.White;
            this.btnViewHistory.Location = new System.Drawing.Point(0, 200);
            this.btnViewHistory.Name = "btnViewHistory";
            this.btnViewHistory.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnViewHistory.Size = new System.Drawing.Size(220, 50);
            this.btnViewHistory.TabIndex = 3;
            this.btnViewHistory.Text = "🔍 View History";
            this.btnViewHistory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnViewHistory.UseVisualStyleBackColor = true;
            this.btnViewHistory.Click += new System.EventHandler(this.btnViewHistory_Click);

            // btnManageUsers
            // 
            this.btnManageUsers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnManageUsers.FlatAppearance.BorderSize = 0;
            this.btnManageUsers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnManageUsers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnManageUsers.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManageUsers.ForeColor = System.Drawing.Color.White;
            this.btnManageUsers.Location = new System.Drawing.Point(0, 250);
            this.btnManageUsers.Name = "btnManageUsers";
            this.btnManageUsers.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnManageUsers.Size = new System.Drawing.Size(220, 50);
            this.btnManageUsers.TabIndex = 5;
            this.btnManageUsers.Text = "👨‍⚕️ Staff Management";
            this.btnManageUsers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnManageUsers.UseVisualStyleBackColor = true;
            this.btnManageUsers.Click += new System.EventHandler(this.btnManageUsers_Click);
            // 

            // 
            // btnAddRecord
            // 
            this.btnAddRecord.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAddRecord.FlatAppearance.BorderSize = 0;
            this.btnAddRecord.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAddRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRecord.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRecord.ForeColor = System.Drawing.Color.White;
            this.btnAddRecord.Location = new System.Drawing.Point(0, 150);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnAddRecord.Size = new System.Drawing.Size(220, 50);
            this.btnAddRecord.TabIndex = 2;
            this.btnAddRecord.Text = "➕ Add Record";
            this.btnAddRecord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddRecord.UseVisualStyleBackColor = true;
            this.btnAddRecord.Click += new System.EventHandler(this.btnAddRecord_Click);
            // 
            // btnRegisterPatient
            // 
            this.btnRegisterPatient.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRegisterPatient.FlatAppearance.BorderSize = 0;
            this.btnRegisterPatient.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnRegisterPatient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegisterPatient.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegisterPatient.ForeColor = System.Drawing.Color.White;
            this.btnRegisterPatient.Location = new System.Drawing.Point(0, 100);
            this.btnRegisterPatient.Name = "btnRegisterPatient";
            this.btnRegisterPatient.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnRegisterPatient.Size = new System.Drawing.Size(220, 50);
            this.btnRegisterPatient.TabIndex = 1;
            this.btnRegisterPatient.Text = "👤 Patients";
            this.btnRegisterPatient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegisterPatient.UseVisualStyleBackColor = true;
            this.btnRegisterPatient.Click += new System.EventHandler(this.btnRegisterPatient_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(48)))), ((int)(((byte)(64)))));
            this.pnlLogo.Controls.Add(this.lblClinicTitle);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(220, 100);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblClinicTitle
            // 
            this.lblClinicTitle.AutoSize = true;
            this.lblClinicTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblClinicTitle.ForeColor = System.Drawing.Color.White;
            this.lblClinicTitle.Location = new System.Drawing.Point(12, 40);
            this.lblClinicTitle.Name = "lblClinicTitle";
            this.lblClinicTitle.Size = new System.Drawing.Size(193, 20);
            this.lblClinicTitle.TabIndex = 0;
            this.lblClinicTitle.Text = "🏥 Patient Management System";
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlFooter.Controls.Add(this.lblUserStatus);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(220, 521);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(764, 40);
            this.pnlFooter.TabIndex = 1;
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.AutoSize = true;
            this.lblUserStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.lblUserStatus.Location = new System.Drawing.Point(15, 12);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Size = new System.Drawing.Size(126, 15);
            this.lblUserStatus.TabIndex = 0;
            this.lblUserStatus.Text = "LoggedIn Status: N/A";
            // 
            // pnlContent
            // 
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.lblContentPlaceholder);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(220, 0);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(764, 521);
            this.pnlContent.TabIndex = 2;
            // 
            // lblContentPlaceholder
            // 
            this.lblContentPlaceholder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContentPlaceholder.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContentPlaceholder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblContentPlaceholder.Location = new System.Drawing.Point(30, 100);
            this.lblContentPlaceholder.Name = "lblContentPlaceholder";
            this.lblContentPlaceholder.Size = new System.Drawing.Size(700, 300);
            this.lblContentPlaceholder.TabIndex = 0;
            this.lblContentPlaceholder.Text = "Please Select an operation from the left sidebar to begin.";
            this.lblContentPlaceholder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1134, 621);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlSidebar);
            this.MinimumSize = new System.Drawing.Size(1150, 660);
            this.Name = "MainDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Secure Medical Clinic System - Dashboard";
            this.Load += new System.EventHandler(this.MainDashboard_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }


        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label lblClinicTitle;
        private System.Windows.Forms.Button btnRegisterPatient;
        private System.Windows.Forms.Button btnViewHistory;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.Button btnManageUsers;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Label lblContentPlaceholder;
    }
}