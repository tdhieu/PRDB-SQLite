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
    public partial class Form_InputType : Form
    {
        public Form_InputType()
        {
            InitializeComponent();
        }

        #region Các biến và đối tượng
        private string typename;
        public string TypeName { get { return typename; } set { typename = value; } }

        private string datatype;
        public string DataType { get { return datatype; } set { datatype = value; } }

        private string valuetype;
        public string ValueType { get {return valuetype;} set {valuetype = value;} }

        private char[] SpecialCharacter;
        private string specialcharacter;
        #endregion

        #region Các sự kiện và phương thức
        private void Form_InputType_Load(object sender, EventArgs e)
        {
            typename = datatype = "";
            txtListValue.Enabled = false;
            label2.Enabled = false;
            txtUserDefined.Enabled = false;
            label3.Enabled = false;
            SpecialCharacter = new char[] {'~', '!', '@', '#', '$', '%', '^', '&', '*', '(',')', '+', '`', ';', ',', '<', '>', '?', '/', ':', '\"', '\'', '=', '{', '}', '[', ']', '\\', '|'};
            specialcharacter = "";
            for (int i = 0; i < SpecialCharacter.Length; i++)
                specialcharacter += SpecialCharacter[i];
        }

        private void ComboBox_DataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_DataType.SelectedIndex == ComboBox_DataType.Items.Count - 1)
            {
                txtListValue.Enabled = true;
                label2.Enabled = true;
                txtUserDefined.Enabled = true;
                label3.Enabled = true;
                txtUserDefined.Focus();
            }
            else
            {
                txtListValue.Enabled = false;
                label2.Enabled = false;
                txtUserDefined.Enabled = false;
                label3.Enabled = false;
            }
        }

        private void txtListValue_TextChanged(object sender, EventArgs e) // Check for special character
        {
            try
            {
                int start = txtListValue.SelectionStart;
                char CharInput = txtListValue.Text[start - 1];
                if (specialcharacter.Contains(CharInput))
                {
                    MessageBox.Show("Do not input the special character '" + CharInput + "'");
                    txtListValue.Text = txtListValue.Text.Remove(start - 1, 1);
                    txtListValue.SelectionStart = start;
                }
            }
            catch { }
        }

        private string Stdize(string S) //Standardize String
        {
            // Chuẩn hóa chuỗi cắt bỏ các dấu , dư thừa
            string R = "";            
            int i = 0;
            while (S[i] == ',') i++;
            int k = S.Length - 1;
            while (S[k] == ',') k--;
            for (int j = i; j <= k; j++)
                if (S[j] != ',') R += S[j];
                else if (S[j - 1] != ',') R += S[j] + " ";
            return R;
        }

        private string SetValueType(string S) //Gán trường giá trị cho các kiểu
        {     
            switch (S)
            {
                case "Int16": return "[-32768  ...  32767]"; 
                case "Int32": return "[-2147483648  ...  2147483647]"; 
                case "Int64": return " [-9223372036854775808  ...  9223372036854775807]"; 
                case "Byte": return "[0  ...  255]"; 
                case "String": return "[0  ...  32767] characters"; 
                case "Single": return "[1.5 x 10^-45  ...  3.4 x 10^38]"; 
                case "Double": return "[5.0 x 10^-324  ...  1.7 x 10^308]"; 
                case "Boolean": return "true  /  false"; 
                case "Decimal": return "[1.0 x 10^-28  ...  7.9 x 10^28]";
                case "DateTime": return "[01/01/0001 C.E  ...  31/12/9999 A.D]";
                case "Binary": return "[1  ...  8000] bytes";
                case "Currency": return "[-2^-63  ...  2^63 - 1]";
            }
            return "";
        }

        private void ButtonOK_InputType_Click(object sender, EventArgs e)
        {
            if (ComboBox_DataType.Text != "")            // Kiểm tra xem người dùng đã chọn kiểu dữ liệu chưa
            {
                if (txtUserDefined.Enabled == true)      // Nếu TextBox để nhập UserDefined được kích hoạt 
                {
                    if (txtListValue.Text == "" && txtUserDefined.Text == "")   // Kiểm tra người dùng đã nhập tên kiểu và giá trị kiểu chưa
                        MessageBox.Show("You have not entered type name and value type");
                    else if (txtUserDefined.Text == "")                                         // Nếu người dùng chưa nhập tên kiểu 
                        MessageBox.Show("You have not enter a type name");
                    else if (txtListValue.Text == "")                                               // Nếu người dùng chưa nhập giá trị kiểu 
                        MessageBox.Show("You have not entered a value type");
                    else                                                                                           // Nếu người dùng đã nhập đầy đủ thông tin
                    {
                        typename = txtUserDefined.Text;              
                        datatype = ComboBox_DataType.Items[ComboBox_DataType.SelectedIndex].ToString();  // Kiểu dữ liệu người dùng chọn trong ComboBox
                        valuetype = "{" + Stdize(txtListValue.Text.Replace("\r\n", ",")) + "}";                                              // Trường giá trị sau khi đã chuẩn hóa
                        this.Close();
                    }
                }
                else                      // Nếu người dùng chọn kiểu dữ liệu thông thường
                {
                    typename = "";
                    datatype = ComboBox_DataType.Items[ComboBox_DataType.SelectedIndex].ToString();
                    valuetype = SetValueType(datatype);
                    this.Close();
                }
            }
            else MessageBox.Show("You have not selected data type");
        }

        private void ButtonCancel_InputType_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
