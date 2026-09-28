using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public class PdfPreviewModal : Form
    {
        private PrintPreviewControl previewControl;
        private PrintDocument document;
        private string defaultFileName;
        
        public PdfPreviewModal(PrintDocument doc, string defaultFileName)
        {
            this.document = doc;
            this.defaultFileName = defaultFileName;
            this.Text = "PDF Preview";
            this.Size = new Size(800, 900);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(236, 240, 241);

            previewControl = new PrintPreviewControl();
            previewControl.Document = doc;
            previewControl.Dock = DockStyle.Fill;
            previewControl.Zoom = 1.0;
            previewControl.UseAntiAlias = true;

            Panel pnlBottom = new Panel { Dock = DockStyle.Bottom, Height = 60, BackColor = Color.White };
            
            FlowLayoutPanel flp = new FlowLayoutPanel 
            { 
                Dock = DockStyle.Right, 
                FlowDirection = FlowDirection.RightToLeft, 
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(10, 10, 10, 10)
            };

            Button btnClose = new Button { 
                Text = "❌ Close", 
                Width = 120, 
                Height = 40, 
                BackColor = Color.FromArgb(231, 76, 60), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(10, 0, 0, 0)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            Button btnSave = new Button { 
                Text = "💾 Save PDF", 
                Width = 150, 
                Height = 40, 
                BackColor = Color.FromArgb(46, 204, 113), 
                ForeColor = Color.White, 
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                Margin = new Padding(10, 0, 0, 0)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            flp.Controls.Add(btnClose);
            flp.Controls.Add(btnSave);
            pnlBottom.Controls.Add(flp);
            
            this.Controls.Add(previewControl);
            this.Controls.Add(pnlBottom);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF Document|*.pdf";
            sfd.Title = "Save PDF Report";
            sfd.FileName = defaultFileName;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                document.PrinterSettings.PrintToFile = true;
                document.PrinterSettings.PrintFileName = sfd.FileName;
                document.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                try
                {
                    document.Print();
                    new ToastNotification("PDF saved successfully!", ToastType.Success).Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving PDF: " + ex.Message + "\n\nMake sure 'Microsoft Print to PDF' feature is installed on Windows.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
