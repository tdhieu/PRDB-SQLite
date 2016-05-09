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
    public partial class Form_DeleteQuery : Form
    {
        public Form_DeleteQuery()
        {
            InitializeComponent();
        }

        private string queryname;
        public string QueryName { get { return queryname; } set { queryname = value; } }

        private void Form_DeleteQuery_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.QueryName;
            foreach (string item in items)
            {
                ComboBox_DeleteQuery.Items.Add(item);
            }
            ComboBox_DeleteQuery.Items.Add("");
            ComboBox_DeleteQuery.SelectedIndex = 0;
        }

        private void ButtonOK_DeleteQuery_Click(object sender, EventArgs e)
        {
            if (ComboBox_DeleteQuery.SelectedItem.ToString().Equals("")) queryname = null;
            else queryname = ComboBox_DeleteQuery.SelectedItem.ToString();
            this.Close();
        }

        private void ButtonCancel_DeleteQuery_Click(object sender, EventArgs e)
        {
            queryname = null;
            this.Close();
        }
    }
}
