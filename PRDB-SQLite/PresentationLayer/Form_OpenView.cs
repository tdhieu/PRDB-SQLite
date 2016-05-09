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
    public partial class Form_OpenView : Form
    {
        public Form_OpenView()
        {
            InitializeComponent();
        }

        private string viewname;
        public string ViewName { get { return viewname; } set { viewname = value; } }

        private void Form_OpenView_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.ViewName;
            foreach (string item in items)  ComboBox_OpenView.Items.Add(item);
            viewname = null;
        }

        private void ButtonOK_OpenView_Click(object sender, EventArgs e)
        {
            if (ComboBox_OpenView.SelectedItem == null)
                MessageBox.Show("You have not selected a view");
            else
            {
                viewname = ComboBox_OpenView.SelectedItem.ToString();
                this.Close();
            }
        }

        private void ButtonCancel_OpenView_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
