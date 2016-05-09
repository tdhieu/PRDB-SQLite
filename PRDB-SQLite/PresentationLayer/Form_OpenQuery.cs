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
    public partial class Form_OpenQuery : Form
    {
        public Form_OpenQuery()
        {
            InitializeComponent();
        }
        private string queryname;
        public string QueryName { get { return queryname; } set { queryname = value; } }

        private void Form_OpenQuery_Load(object sender, EventArgs e)
        {
            List<string> items = PRDB_SQLite.Program.QueryName;
            foreach (string item in items) ComboBox_OpenQuery.Items.Add(item);
            queryname = null;
        }

        private void ButtonOK_OpenQuery_Click(object sender, EventArgs e)
        {
            if (ComboBox_OpenQuery.SelectedItem == null)
                MessageBox.Show("You have not selected a query");
            else
            {
                queryname = ComboBox_OpenQuery.SelectedItem.ToString();
                this.Close();
            }
        }

        private void ButtonCancel_OpenQuery_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
