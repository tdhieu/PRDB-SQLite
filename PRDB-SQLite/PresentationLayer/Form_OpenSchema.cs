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
    public partial class Form_OpenSchema : Form
    {
        public Form_OpenSchema()
        {
            InitializeComponent();
        }

        private string schemaname;
        public string SchemaName { get { return schemaname; } set { schemaname = value; } }

        private void Form_OpenSchema_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.SchemaName;
            foreach (string item in items) ComboBox_OpenSchema.Items.Add(item);
            schemaname = null;
        }

        private void ButtonOK_OpenSchema_Click(object sender, EventArgs e)
        {
            if (ComboBox_OpenSchema.SelectedItem == null)
                MessageBox.Show("You have not selected a schema");
            else
            {
                schemaname = ComboBox_OpenSchema.SelectedItem.ToString();
                this.Close();
            }
        }

        private void ButtonCancel_OpenSchema_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
