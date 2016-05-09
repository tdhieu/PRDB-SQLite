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
    public partial class Form_NewTable : Form
    {
        Form_Main frm_main = new Form_Main();
        private string tablename;
        public string TableName { get { return tablename; } set { tablename = value; } }

        public Form_NewTable()
        {
            InitializeComponent();
        }

        private void ButtonCancel_NewTable_Click(object sender, EventArgs e)
        {
            tablename = null;    
            this.Close();
        }

        private void ButtonOK_NewTable_Click(object sender, EventArgs e)
        {
            if (txtTableName.Text == null)
            {
                MessageBox.Show("Bạn chưa nhập tên bảng");
            }
            else
            {
                tablename = txtTableName.Text;
                this.Close();
            }
        }
    }
}
