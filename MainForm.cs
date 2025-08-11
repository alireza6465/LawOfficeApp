using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

namespace LawOfficeApp
{
    public class MainForm : Form
    {
        private TabControl tab;
        private DataGridView dgvClients;
        private Button btnAddClient, btnEditClient, btnDeleteClient, btnRefreshClients;

        public MainForm()
        {
            Text = "Law Office Manager";
            Width = 1000;
            Height = 700;
            StartPosition = FormStartPosition.CenterScreen;

            tab = new TabControl { Dock = DockStyle.Fill };
            var tabClients = new TabPage("موکلین");
            var tabCases = new TabPage("پرونده‌ها");
            var tabChecks = new TabPage("چک‌ها");
            var tabReminders = new TabPage("یادآورها");

            // Clients tab UI
            dgvClients = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AutoGenerateColumns = false };
            dgvClients.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClients.MultiSelect = false;
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Id", DataPropertyName = "Id", Visible = false });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "نام", DataPropertyName = "Name", Width = 250 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "تلفن", DataPropertyName = "Phone", Width = 150 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "ایمیل", DataPropertyName = "Email", Width = 200 });
            dgvClients.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "پرونده‌ها", DataPropertyName = "CaseCount", Width = 100 });

            var pnl = new Panel { Dock = DockStyle.Top, Height = 46 };
            btnAddClient = new Button { Text = "موکل جدید", Left = 10, Width = 110, Top = 8 };
            btnEditClient = new Button { Text = "ویرایش", Left = 130, Width = 90, Top = 8 };
            btnDeleteClient = new Button { Text = "حذف", Left = 230, Width = 90, Top = 8 };
            btnRefreshClients = new Button { Text = "بارگذاری", Left = 330, Width = 90, Top = 8 };

            btnAddClient.Click += (s,e) => { AddClient(); };
            btnEditClient.Click += (s,e) => { EditSelectedClient(); };
            btnDeleteClient.Click += (s,e) => { DeleteSelectedClient(); };
            btnRefreshClients.Click += (s,e) => LoadClients();

            pnl.Controls.AddRange(new Control[] { btnAddClient, btnEditClient, btnDeleteClient, btnRefreshClients });
            tabClients.Controls.Add(pnl);
            tabClients.Controls.Add(dgvClients);

            // Add tabs to control
            tab.TabPages.Add(tabClients);
            tab.TabPages.Add(tabCases);
            tab.TabPages.Add(tabChecks);
            tab.TabPages.Add(tabReminders);

            Controls.Add(tab);

            Load += (s,e) => { LoadClients(); };
        }

        private void LoadClients()
        {
            var list = DataAccess.GetClients(Program.DbPath);
            // For simplicity, case count set to 0 for now
            var rows = list.Select(c => new { c.Id, c.Name, c.Phone, c.Email, CaseCount = 0 }).ToList();
            dgvClients.DataSource = rows;
        }

        private void AddClient()
        {
            using var f = new ClientForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                // copy avatar if provided
                var avatarPath = f.AvatarPath;
                string saved = null;
                if (!string.IsNullOrEmpty(avatarPath) && File.Exists(avatarPath))
                {
                    var avatarsDir = Path.Combine(Program.DataFolder, "avatars");
                    if (!Directory.Exists(avatarsDir)) Directory.CreateDirectory(avatarsDir);
                    var dest = Path.Combine(avatarsDir, Path.GetFileName(avatarPath));
                    File.Copy(avatarPath, dest, true);
                    saved = dest;
                }
                DataAccess.AddClient(Program.DbPath, f.ClientName, f.Phone, f.Email, f.Address, f.Note, saved);
                LoadClients();
            }
        }

        private void EditSelectedClient()
        {
            if (dgvClients.SelectedRows.Count == 0) return;
            var id = (int)dgvClients.SelectedRows[0].Cells["Id"].Value;
            // fetch full client record
            var clients = DataAccess.GetClients(Program.DbPath);
            var client = clients.Find(c => c.Id == id);
            if (client == null) return;
            using var f = new ClientForm();
            f.ClientName = client.Name;
            f.Phone = client.Phone;
            f.Email = client.Email;
            f.Address = client.Address;
            f.Note = client.Note;
            f.AvatarPath = client.AvatarPath;
            if (f.ShowDialog() == DialogResult.OK)
            {
                // Update not implemented fully for brevity
                MessageBox.Show(\"ویرایش ذخیره نشد (نسخه آزمایشی). برای ویرایش کامل به‌روزرسانی بعدی مراجعه کنید.\", \"توجه\");
            }
        }

        private void DeleteSelectedClient()
        {
            if (dgvClients.SelectedRows.Count == 0) return;
            var id = (int)dgvClients.SelectedRows[0].Cells["Id"].Value;
            if (MessageBox.Show(\"آیا از حذف این موکل اطمینان دارید؟\", \"حذف\", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DataAccess.DeleteClient(Program.DbPath, id);
                LoadClients();
            }
        }
    }
}
