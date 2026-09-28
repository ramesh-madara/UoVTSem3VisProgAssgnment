using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PatientManagementSystem
{
    public static class UITheme
    {
        public static void ApplyTheme(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c is Button)
                {
                    Button btn = (Button)c;
                    // Ignore sidebar buttons in MainDashboard if they are already styled dark
                    if (btn.Name.StartsWith("btnRegister") || btn.Name.StartsWith("btnAdd") || btn.Name.StartsWith("btnView") || btn.Name.StartsWith("btnManage") || btn.Name == "btnLogout")
                    {
                        continue;
                    }

                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.FromArgb(52, 152, 219); // Modern Blue
                    btn.ForeColor = Color.White;
                    btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                }
                else if (c is TextBox)
                {
                    TextBox txt = (TextBox)c;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.White;
                    txt.Font = new Font("Segoe UI", 10);
                }
                else if (c is ComboBox)
                {
                    ComboBox cmb = (ComboBox)c;
                    cmb.FlatStyle = FlatStyle.Flat;
                    cmb.Font = new Font("Segoe UI", 10);
                }
                
                if (c.HasChildren)
                {
                    ApplyTheme(c);
                }
            }
        }
    }
}
