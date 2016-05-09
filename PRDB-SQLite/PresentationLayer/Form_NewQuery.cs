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
    public partial class Form_NewQuery : Form
    {
        public Form_NewQuery()
        {
            InitializeComponent();
        }

        private string queryname;
        public string QueryName { get { return queryname; } set { queryname = value; } }

        private void ButtonOK_NewQuery_Click(object sender, EventArgs e)
        {
            if (txtQueryName.Text == null)
                MessageBox.Show("You have not enter a query name");
            else if (Program.QueryName.Contains(txtQueryName.Text))
                MessageBox.Show("The query name has already existed in the database");
            else
            {
                queryname = txtQueryName.Text;
                this.Close();
            }
        }
        private void ButtonCancel_NewQuery_Click(object sender, EventArgs e)
        {
            queryname = null;
            this.Close();
        }

        private void Form_NewQuery_Load(object sender, EventArgs e)
        {
            queryname = null;
        }
    }
}
