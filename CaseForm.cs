using System;
using System.Windows.Forms;

namespace LawOfficeApp
{
    public class CaseForm : Form
    {
        public string TitleText = "";
        public int ClientId = 0;
        public string Court = "";
        public string Status = "در جریان";
        public string NextDate = "";
        public string Note = "";

        public CaseForm()
        {
            Text = "پرونده جدید (نسخه آزمایشی)";
            Width = 400; Height = 300;
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}
