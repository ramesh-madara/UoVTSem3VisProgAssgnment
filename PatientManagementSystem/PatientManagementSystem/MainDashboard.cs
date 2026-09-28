using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public partial class MainDashboard : Form
    {
        private string currentUser;
        private string currentRole;
        public MainDashboard(string username, string role)
        {
            InitializeComponent();
            UITheme.ApplyTheme(this);

            currentUser = username;
            currentRole = role;

            lblUserStatus.Text = string.Format("User: {0} | Role: {1}", currentUser, currentRole);

            // Limit specific operations according to Roles if necessary
            if (currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                // Admins can register patients but cannot add medical records (medical info is doctor only)
                btnAddRecord.Enabled = false;
                btnAddRecord.BackColor = Color.Gray;

                Button btnManageTemplates = new Button();
                btnManageTemplates.Name = "btnManageTemplates";
                btnManageTemplates.Text = "📝 Manage Templates";
                btnManageTemplates.Dock = DockStyle.Top;
                btnManageTemplates.Height = 50;
                btnManageTemplates.FlatStyle = FlatStyle.Flat;
                btnManageTemplates.FlatAppearance.BorderSize = 0;
                btnManageTemplates.ForeColor = Color.White;
                btnManageTemplates.Font = new Font("Segoe UI", 10F);
                btnManageTemplates.TextAlign = ContentAlignment.MiddleLeft;
                btnManageTemplates.Padding = new Padding(15, 0, 0, 0);
                btnManageTemplates.Click += BtnManageTemplates_Click;
                
                pnlSidebar.Controls.Add(btnManageTemplates);
                btnManageTemplates.BringToFront();
            }
        }

        private void BtnManageTemplates_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ManageReportTemplatesForm());
        }

        private void MainDashboard_Load(object sender, EventArgs e)
        {
            // Show clinical greetings initially inside the panel
            lblContentPlaceholder.Text = string.Format("Welcome, {0} ({1})!\n\n", currentUser, currentRole) + "Please select an operation from the left sidebar to begin.\n";
            
            // Show custom toast notification guaranteed to display
            new ToastNotification(string.Format("Welcome, {0} ({1})!", currentUser, currentRole)).Show();
        }
        private void ShowChildForm(Form childForm)
        {
            pnlContent.Controls.Clear();
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.Show();
        }

        private void btnRegisterPatient_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ManagePatientsForm());
        }

        private void btnAddRecord_Click(object sender, EventArgs e)
        {
            ShowChildForm(new AddMedicalRecordForm());
        }

        private void btnViewHistory_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewPatientHistoryForm());
        }
        private void btnManageUsers_Click(object sender, EventArgs e)
        {
            if (!currentRole.Equals("Admin", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Access Denied: Only users with the Admin role can access Staff User Management.",
                                "Access Restricted", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ShowChildForm(new UserManagementForm());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to log out of the system?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Restart();
                Environment.Exit(0);
            }
        }
    }

    public enum ToastType { Success, Error, Info, Warning }

    public class ToastNotification : Form
    {
        private Timer fadeTimer;
        private string action = "fadeIn";
        private int stayCount = 0;
        
        public ToastNotification(string message, ToastType type = ToastType.Success)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(400, 70);
            
            if (type == ToastType.Success) this.BackColor = Color.FromArgb(46, 204, 113); // Green
            else if (type == ToastType.Error) this.BackColor = Color.FromArgb(231, 76, 60); // Red
            else if (type == ToastType.Info) this.BackColor = Color.FromArgb(52, 152, 219); // Blue
            else if (type == ToastType.Warning) this.BackColor = Color.FromArgb(243, 156, 18); // Orange
            
            this.ForeColor = Color.White;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.Opacity = 0.0;
            
            Label lblMsg = new Label();
            lblMsg.Text = message;
            lblMsg.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            lblMsg.AutoSize = false;
            lblMsg.Dock = DockStyle.Fill;
            lblMsg.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(lblMsg);
            
            this.Load += ToastNotification_Load;
        }

        private void ToastNotification_Load(object sender, EventArgs e)
        {
            // Position at bottom right of the screen
            this.Location = new Point(Screen.PrimaryScreen.WorkingArea.Width - this.Width - 20,
                                      Screen.PrimaryScreen.WorkingArea.Height - this.Height - 20);

            fadeTimer = new Timer();
            fadeTimer.Interval = 30; // 30ms ticks for smooth fade
            fadeTimer.Tick += FadeTimer_Tick;
            fadeTimer.Start();
        }

        private void FadeTimer_Tick(object sender, EventArgs e)
        {
            if (action == "fadeIn")
            {
                if (this.Opacity < 1.0) this.Opacity += 0.1;
                else action = "stay";
            }
            else if (action == "stay")
            {
                stayCount++;
                if (stayCount > 60) action = "fadeOut"; // Stay for roughly 1.8 seconds
            }
            else if (action == "fadeOut")
            {
                if (this.Opacity > 0.0) this.Opacity -= 0.1;
                else
                {
                    fadeTimer.Stop();
                    this.Close();
                }
            }
        }
    }
}
