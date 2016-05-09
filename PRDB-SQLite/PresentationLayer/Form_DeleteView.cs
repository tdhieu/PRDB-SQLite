using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PRDB_SQLite.PresentationLayer
{
    public partial class Form_DeleteView : Form
    {
        public Form_DeleteView()
        {
            InitializeComponent();
        }

        private string viewname;
        public string ViewName { get { return viewname; } set { viewname = value; } }

        private void Form_DeleteView_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.ViewName;
            foreach (string item in items)
            {
                ComboBox_DeleteView.Items.Add(item);
            }
            ComboBox_DeleteView.Items.Add("");
            ComboBox_DeleteView.SelectedIndex = 0;
        }

        private void ButtonOK_DeleteView_Click(object sender, EventArgs e)
        {
            if (ComboBox_DeleteView.SelectedItem.ToString().Equals("")) viewname = null;
            else viewname = ComboBox_DeleteView.SelectedItem.ToString();
            this.Close();
        }

        private void ButtonCancel_DeleteView_Click(object sender, EventArgs e)
        {
            viewname = null;
            this.Close();
        }
    }
}
