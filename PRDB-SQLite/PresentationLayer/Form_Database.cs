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
    public partial class Form_Database : Form
    {
        public Form_Database()
        {
            InitializeComponent();
        }

        private string dbname;
        public string dbName { get { return dbname; } set { dbname = value; } }

        private void ButtonOK_NewTable_Click(object sender, EventArgs e)
        {
            if (txtDBName.Text == null)
            {
                MessageBox.Show("Bạn chưa nhập tên Cơ Sở Dữ Liệu");
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
