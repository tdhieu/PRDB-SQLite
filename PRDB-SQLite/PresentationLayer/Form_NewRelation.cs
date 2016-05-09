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
    public partial class Form_NewRelation : Form
    {
        public Form_NewRelation()
        {
            InitializeComponent();
        }

        private string relationname;
        public string RelationName { get { return relationname; } set { relationname = value; } }

        private string schemaname;
        public string SchemaName { get { return schemaname; } set { schemaname = value; } }

        private void Form_NewRelation_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.SchemaName;
            foreach (string item in items) ComboBox_NewRelation.Items.Add(item);
            schemaname = null;
            relationname = null;
        }

        private void ButtonOK_NewTable_Click(object sender, EventArgs e)
        {
            if (ComboBox_NewRelation.SelectedItem == null) 
                MessageBox.Show("You have not selected a schema name");
            else 
            {
                schemaname = ComboBox_NewRelation.SelectedItem.ToString();

                if (txtRelationName.Text == "")
                    MessageBox.Show("You have not entered a relation name");
                else if (Program.RelationName.Contains(txtRelationName.Text))
                    MessageBox.Show("This relation name has already existed in the database");
                else
                {
                    relationname = txtRelationName.Text;
                    this.Close();
                }
            }
        }

        private void ButtonCancel_NewTable_Click(object sender, EventArgs e)
        {
            relationname = null;
            this.Close();
        }
    }
}
