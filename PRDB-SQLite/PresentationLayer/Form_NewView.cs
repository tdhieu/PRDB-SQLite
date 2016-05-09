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
    public partial class Form_NewView : Form
    {
        public Form_NewView()
        {
            InitializeComponent();
        }

        private string viewname;
        public string ViewName { get { return viewname; } set { viewname = value; } }

        private void ButtonOK_NewView_Click(object sender, EventArgs e)
        {
            if (txtViewName.Text == null)
                MessageBox.Show("You have not enter a view name");
            else if (Program.ViewName.Contains(txtViewName.Text))
                MessageBox.Show("The view name has already existed in the database");
            else
            {
                viewname = txtViewName.Text;
                this.Close();
            }
        }

        private void ButtonCancel_NewView_Click(object sender, EventArgs e)
        {
            viewname = null;
            this.Close();
        }

        private void Form_NewView_Load(object sender, EventArgs e)
        {
            viewname = null;
        }
    }
}
