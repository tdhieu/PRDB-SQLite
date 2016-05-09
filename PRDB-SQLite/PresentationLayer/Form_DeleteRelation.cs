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
    public partial class Form_DeleteRelation : Form
    {
        public Form_DeleteRelation()
        {
            InitializeComponent();
        }

        private string relationname;
        public string RelationName { get { return relationname; } set { relationname = value; } }

        private void Form_DeleteRelation_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.RelationName;
            foreach (string item in items)
            {
                ComboBox_DeleteRelation.Items.Add(item);
            }
            ComboBox_DeleteRelation.Items.Add("");
            ComboBox_DeleteRelation.SelectedIndex = 0;
        }

        private void ButtonOK_DeleteRelation_Click(object sender, EventArgs e)
        {
            if (ComboBox_DeleteRelation.SelectedItem.ToString().Equals("")) relationname = null;
            else relationname = ComboBox_DeleteRelation.SelectedItem.ToString();
            this.Close();
        }

        private void ButtonCancel_DeleteRelation_Click(object sender, EventArgs e)
        {
            relationname = null;
            this.Close();
        }
    }
}
