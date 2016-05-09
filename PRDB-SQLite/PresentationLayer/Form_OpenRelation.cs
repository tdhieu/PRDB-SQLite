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
    public partial class Form_OpenRelation : Form
    {
        public Form_OpenRelation()
        {
            InitializeComponent();
        }

        private string relationname;
        public string RelationName { get { return relationname; } set { relationname = value; } }

        private void Form_OpenRelation_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.RelationName;
            foreach (string item in items) ComboBox_OpenRelation.Items.Add(item);
            relationname = null;
        }

        private void ButtonOK_OpenRelation_Click(object sender, EventArgs e)
        {
            if (ComboBox_OpenRelation.SelectedItem == null)
                MessageBox.Show("You have not selected a relation");
            else
            {
                relationname = ComboBox_OpenRelation.SelectedItem.ToString();
                this.Close();
            }
        }

        private void ButtonCancel_OpenRelation_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
