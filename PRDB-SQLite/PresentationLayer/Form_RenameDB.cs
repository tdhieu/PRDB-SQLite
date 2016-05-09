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
    public partial class Form_RenameDB : Form
    {
        public Form_RenameDB()
        {
            InitializeComponent();
        }

        private string dbname;
        public string dbName { get { return dbname; } set { dbname = value; } }

        private void ButtonOK_NewTable_Click(object sender, EventArgs e)
        {
            if (txtDBName.Text == null)
            {
                MessageBox.Show("You did not enter a database name");
            }
            else
            {
                dbname = txtDBName.Text;
                this.Close();
            }
        }

        private void ButtonCancel_NewTable_Click(object sender, EventArgs e)
        {
            dbname = null;
            this.Close();
        }
    }
}
