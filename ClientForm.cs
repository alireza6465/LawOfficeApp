using System;
using System.Windows.Forms;

namespace LawOfficeApp
{
    public class ClientForm : Form
    {
        public string ClientName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Note { get; set; } = "";
        public string AvatarPath { get; set; } = "";

        private TextBox txtName, txtPhone, txtEmail, txtAddress, txtNote;
        private Button btnOk, btnCancel, btnChooseAvatar;

        public ClientForm()
        {
            Text = "موکل جدید";
            Width = 500;
            Height = 420;
            StartPosition = FormStartPosition.CenterParent;

            var lbl = new Label { Text = "نام کامل", Left = 10, Top = 10 };
            txtName = new TextBox { Left = 10, Top = 30, Width = 450 };

            var lbl2 = new Label { Text = "تلفن", Left = 10, Top = 70 };
            txtPhone = new TextBox { Left = 10, Top = 90, Width = 450 };

            var lbl3 = new Label { Text = "ایمیل", Left = 10, Top = 130 };
            txtEmail = new TextBox { Left = 10, Top = 150, Width = 450 };

            var lbl4 = new Label { Text = "آدرس", Left = 10, Top = 190 };
            txtAddress = new TextBox { Left = 10, Top = 210, Width = 450 };

            var lbl5 = new Label { Text = "یادداشت", Left = 10, Top = 250 };
            txtNote = new TextBox { Left = 10, Top = 270, Width = 450, Height = 40, Multiline = true };

            btnChooseAvatar = new Button { Text = "انتخاب عکس", Left = 10, Top = 320, Width = 120 };
            btnChooseAvatar.Click += (s,e) => { using var ofd = new OpenFileDialog(){ Filter = "Images|*.jpg;*.jpeg;*.png;*.bmp" }; if (ofd.ShowDialog()==DialogResult.OK) { AvatarPath = ofd.FileName; } };

            btnOk = new Button { Text = "ذخیره", Left = 300, Top = 320, Width = 80 };
            btnCancel = new Button { Text = "انصراف", Left = 390, Top = 320, Width = 80 };
            btnOk.Click += (s,e) => { ClientName = txtName.Text; Phone = txtPhone.Text; Email = txtEmail.Text; Address = txtAddress.Text; Note = txtNote.Text; DialogResult = DialogResult.OK; Close(); };
            btnCancel.Click += (s,e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[] { lbl, txtName, lbl2, txtPhone, lbl3, txtEmail, lbl4, txtAddress, lbl5, txtNote, btnChooseAvatar, btnOk, btnCancel });
        }
    }
}
