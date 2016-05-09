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
    public partial class Form_DeleteSchema : Form
    {
        public Form_DeleteSchema()
        {
            InitializeComponent();
        }

        private string schemaname;
        public string SchemaName { get { return schemaname; } set { schemaname = value; } }

        private void Form_DeleteSchema_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.SchemaName;
            foreach (string item in items)
            {
                ComboBox_DeleteSchema.Items.Add(item);
            }
            ComboBox_DeleteSchema.Items.Add("");
            ComboBox_DeleteSchema.SelectedIndex = 0;
        }

        private void ButtonOK_DeleteSchema_Click(object sender, EventArgs e)
        {
            if (ComboBox_DeleteSchema.SelectedItem.ToString().Equals("")) schemaname = null;
            else schemaname = ComboBox_DeleteSchema.SelectedItem.ToString();
            this.Close();
        }

        private void ButtonCancel_DeleteSchema_Click(object sender, EventArgs e)
        {
            schemaname = null;
            this.Close();
        }
    }
}
