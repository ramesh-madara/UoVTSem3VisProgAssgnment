using System;
using System.Drawing;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public class AboutModal : Form
    {
        public AboutModal()
        {
            this.Text = "About";
            this.Size = new Size(500, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;
            
            Panel pnlHeader = new Panel();
            pnlHeader.BackColor = Color.FromArgb(41, 128, 185);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Height = 60;

            Label lblTitle = new Label();
            lblTitle.Text = "ℹ️ About System";
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblTitle.AutoSize = false;
            lblTitle.Dock = DockStyle.Fill;
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            pnlHeader.Controls.Add(lblTitle);

            Label lblContent = new Label();
            lblContent.Text = "UoVT SOF 23/24 Semester 3\n" +
                              "Visual Programming II Group Assignment\n" +
                              "Patient Medical Report Management System\n\n" +
                              "Group Members:\n" +
                              "SOF/23/B2/29 - Nuwan Hasanka\n" +
                              "SOF/23/B2/02 - Ramesh Madara\n" +
                              "SOF/23/B2/23 - Gihan Lavnidu\n" +
                              "SOF/23/B2/28 - Chiranthi Bhagya\n" +
                              "SOF/23/B2/20 - Suresh Indika\n" +
                              "SOF/23/B2/14 - Thisari Wijerathne";
            lblContent.Font = new Font("Segoe UI", 11, FontStyle.Regular);
            lblContent.ForeColor = Color.FromArgb(64, 64, 64);
            lblContent.Location = new Point(30, 80);
            lblContent.Size = new Size(420, 250);
            lblContent.TextAlign = ContentAlignment.MiddleCenter;
            
            Button btnClose = new Button();
            btnClose.Text = "Close";
            btnClose.Size = new Size(120, 40);
            btnClose.Location = new Point((this.ClientSize.Width - 120) / 2, 350);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.BackColor = Color.FromArgb(52, 73, 94);
            btnClose.ForeColor = Color.White;
            btnClose.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(lblContent);
            this.Controls.Add(btnClose);
            this.Controls.Add(pnlHeader);
        }
    }
}
