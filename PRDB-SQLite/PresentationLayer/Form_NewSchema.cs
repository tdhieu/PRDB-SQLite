using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using PRDB_SQLite.DataAccessLayer;
using System.Windows.Forms;

namespace PRDB_SQLite.PresentationLayer
{
    public partial class Form_NewSchema : Form
    {
        private string schemaname;
        public string SchemaName { get { return schemaname; } set { schemaname = value; } }

        public Form_NewSchema()
        {
            InitializeComponent();
        }

        private void ButtonOK_NewTable_Click(object sender, EventArgs e)
        {
            if (txtSchemaName.Text == null)
                MessageBox.Show("You have not entered a schema name");
            else if (Program.SchemaName.Contains(txtSchemaName.Text))
                MessageBox.Show("The schema name has already existed in the database");
            else
            {
                schemaname = txtSchemaName.Text;
                this.Close();
            }
        }

        private void ButtonCancel_NewTable_Click(object sender, EventArgs e)
        {
            schemaname = null;
            this.Close();
        }

        private void Form_NewSchema_Load(object sender, EventArgs e)
        {
            schemaname = null;
        }

    }
}
