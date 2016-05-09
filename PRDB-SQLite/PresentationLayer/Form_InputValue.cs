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
    public partial class Form_InputValue : Form
    {
        public Form_InputValue()
        {
            InitializeComponent();
        }

        #region Các biến và đối tượng
        private double minprob;
        public double MinProb { get { return minprob; } set {minprob = value; }}

        private double maxprob;
        public double MaxProb { get { return maxprob; } set { maxprob = value; } }

        private string strvalue;
        public string StrValue { get { return strvalue; } set { strvalue = value; } }

        public struct ProbTriple
        {
            public string value;
            public double min, max;
        }

        private ProbTriple[] attribute;
        public ProbTriple[] Attribute { get { return attribute; } set { attribute = value; } }

        bool flag;
        int CurrentRow, CurrentCell;
        string ErrorMessage;
        private char[] SpecialCharacter;
        private string specialcharacter;
        private int n;
        #endregion

        #region Các phương thức
        private void Form_InputValue_Load(object sender, EventArgs e)
        {
            // Khởi tạo các kí tự đặc biệt
            SpecialCharacter = new char[] { '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '+', '`', ';', ',', '<', '>', '?', '/', ':', '\"', '\'', '=', '{', '}', '[', ']', '\\', '|' };
            specialcharacter = "";
            for (int i = 0; i < SpecialCharacter.Length; i++)
                specialcharacter += SpecialCharacter[i];
            CheckBox_UD.Checked = false;
            label1.Enabled = false;
            label2.Enabled = false;
            txtMinProb.Enabled = false;
            txtMaxProb.Enabled = false;
            txtValue.Visible = false;
            GridViewVaLue.Visible = true;
            CurrentRow = 0;
            ErrorMessage = "";
        }

        private void CheckBox_UD_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox_UD.Checked == false)
            {
                // Mở các control liên quan đến việc nhập từng giá trị rời rạc
                label1.Enabled = false;
                label2.Enabled = false;
                txtMinProb.Enabled = false;
                txtMaxProb.Enabled = false;
                txtValue.Visible = false;
                GridViewVaLue.Visible = true;
            }
            else
            {
                // Mở các control liên quan đến việc nhập một tập các giá trị
                label1.Enabled = true;
                label2.Enabled = true;
                txtMinProb.Enabled = true;
                txtMaxProb.Enabled = true;
                txtValue.Visible = true;
                GridViewVaLue.Visible = false;
            }
        }

        // Xác thực dữ liệu trên dòng
        private bool ValidateRow(int RowIndex)
        {
            bool Value = (GridViewVaLue.Rows[RowIndex].Cells["ColumnValue"].Value != null);
            bool MinProb = (GridViewVaLue.Rows[RowIndex].Cells["ColumnMinProb"].Value != null);
            bool MaxProb = (GridViewVaLue.Rows[RowIndex].Cells["ColumnMaxProb"].Value != null);
            if (!Value || !MinProb || !MaxProb)  // Tồn tại một trong ba trường chưa có giá trị
            {
                if (!Value) // Value == null
                {
                    ErrorMessage = "Bạn chưa nhập giá trị";
                    CurrentCell = 0;
                    return false;
                }
                if (!MinProb)  // MinProb == null 
                {
                    ErrorMessage = "Bạn chưa nhập xác suất của cận dưới";
                    CurrentCell = 1;
                    return false;
                }
                else               // MaxProb == null
                {
                    ErrorMessage = "Bạn chưa nhập xác suất của cận trên";
                    CurrentCell = 2;
                    return false;
                }
            }
            return true;
        }

        // Xử lý sự kiện thay đổi lựa chọn trên GridView
        private void GridViewVaLue_SelectionChanged(object sender, EventArgs e)
        {
            if (GridViewVaLue.CurrentRow != null)
            {
                if (CurrentRow < 0) CurrentRow = GridViewVaLue.CurrentRow.Index;
                if (GridViewVaLue.CurrentRow.Index != CurrentRow && ValidateRow(CurrentRow) == false)
                {
                    // Xuất ra thông báo lỗi
                    if (!flag)  // Đặt biến cờ để tránh sự kiện SelectionChanged lặp lại 2 lần
                    {
                        flag = true;
                        MessageBox.Show(ErrorMessage);
                        GridViewVaLue.CurrentCell = GridViewVaLue.Rows[CurrentRow].Cells[CurrentCell];
                    }
                }
                else CurrentRow = GridViewVaLue.CurrentRow.Index;
                flag = false;
            }
        }

        //Standardize String
        private string Stdize(string S)
        {
            // Chuẩn hóa chuỗi cắt bỏ các dấu , dư thừa
            string R = "";
            int i = 0;
            while (S[i] == ',') i++;
            int k = S.Length - 1;
            while (S[k] == ',') k--;
            for (int j = i; j <= k; j++)
                if (S[j] != ',') R += S[j];
                else if (S[j - 1] != ',') R += S[j];
            return R;
        }

        // Xử lý sự kiện nhấn nút Add
        private void ButtonAdd_InputValue_Click(object sender, EventArgs e)
        {
            // Nếu chưa có dữ liệu thì ta thêm vào dòng mới
            if (GridViewVaLue.Rows.Count == 0)
                GridViewVaLue.Rows.Add();
            // Nếu đã có dữ liệu thì xác thực dữ liệu trên dòng cũ
            else
            {
                if (!ValidateRow(CurrentRow))
                {
                    MessageBox.Show(ErrorMessage);
                    GridViewVaLue.CurrentCell = GridViewVaLue.Rows[CurrentRow].Cells[CurrentCell];
                }
                else
                {
                    GridViewVaLue.Rows.Add();
                    CurrentRow = GridViewVaLue.Rows.Count - 1;
                }
            }
        }

        // Xử lý sự kiện nhấn nút Remove
        private void ButtonRemove_InputValue_Click(object sender, EventArgs e)
        {
            // Nếu chưa có dữ liệu
            if (GridViewVaLue.Rows.Count == 0)
                MessageBox.Show("Bạn chưa nhập giá trị");
            else
            {
                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa giá trị này ?", "Xóa giá trị của thuộc tính", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    CurrentRow = GridViewVaLue.CurrentRow.Index;
                    GridViewVaLue.Rows.RemoveAt(CurrentRow);
                    CurrentRow = GridViewVaLue.Rows.Count - 1;
                }
            }
        }

        // Xử lý sự kiện nhấn nút Cancel
        private void ButtonCancel_InputType_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Xử lý sự kiện nhấn nút OK
        private void ButtonOK_InputValue_Click(object sender, EventArgs e)
        {
            if (CheckBox_UD.Checked == false) // Nhập bộ ba xác suất cho từng giá trị riêng biệt
            {
                if (GridViewVaLue.Rows.Count == 0)
                    MessageBox.Show("Bạn chưa nhập giá trị");
                else if (!ValidateRow(CurrentRow))
                {
                    MessageBox.Show(ErrorMessage);
                    GridViewVaLue.CurrentCell = GridViewVaLue.Rows[CurrentRow].Cells[CurrentCell];
                }
                else
                {
                    // Lấy tập giá trị từ GridView
                    n = GridViewVaLue.Rows.Count;
                    attribute = new ProbTriple[n];
                    for (int i = 0; i < n; i++)
                    {
                        attribute[i].value = GridViewVaLue.Rows[i].Cells["ColumnValue"].Value.ToString();
                        attribute[i].min = Convert.ToDouble(GridViewVaLue.Rows[i].Cells["ColumnMinProb"].Value);
                        attribute[i].max = Convert.ToDouble(GridViewVaLue.Rows[i].Cells["ColumnMaxProb"].Value);
                    }
                    ToStringValue();
                    this.Close();
                }
            }
            else // Nhập bộ ba xác suất theo phân bố đều 
            {
                if (txtMinProb.Text == "" || txtMaxProb.Text == "")
                    MessageBox.Show("Bạn chưa nhập giá trị cho hàm phân bố xác suất");
                else if (txtValue.Text == "")
                    MessageBox.Show("Bạn chưa nhập các giá trị");
                else
                {
                    // Lấy tập giá trị từ TextBox và phân bố xác suất cho từng giá trị
                    try
                    {
                        double minprob, maxprob;
                        string[] value;
                        value = Stdize(txtValue.Text.Replace("\r\n", ",")).Split(',');
                        for (int i = 0; i < value.Length; i++) value[i] = value[i].Trim();

                        //for (int i = 0; i < value.Length; i++)
                        //    for (int j = value.Length-1; j > i;  j--)
                        //        if (value[i].CompareTo(value[j]) == 0)
                        //            throw new Exception("Không được nhập các phần tử có giá trị giống nhau") { };

                        minprob = Convert.ToDouble(txtMinProb.Text);
                        maxprob = Convert.ToDouble(txtMaxProb.Text);
                        n = value.Length;
                        attribute = new ProbTriple[n];
                        for (int i = 0; i < n; i++)
                        {
                            attribute[i].value = value[i];
                            attribute[i].min = minprob / n;
                            attribute[i].max = maxprob / n;
                        }
                        ToStringValue(minprob,maxprob);
                        this.Close();
                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                }
            }
        }

        // Xử lý sự kiện cập nhật giá trị trên lưới
        private void GridViewVaLue_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (GridViewVaLue.CurrentCell.Value != null)
            {
                if (e.ColumnIndex > 0)  // Giá trị nhập vào các ô MinProb và MaxProb
                {
                    try
                    {
                        double Prob = Convert.ToDouble(GridViewVaLue.CurrentCell.Value);
                        if (Prob < 0.0 || Prob > 1.0)
                            MessageBox.Show("Giá trị của xác suất phải nằm trong khoảng [0,1]");
                    }
                    catch
                    {
                        MessageBox.Show("Giá trị của xác suất phải là một số thực");
                    }
                }
                else          // Giá trị nhập vào ô Value
                {
                    string StrValue = GridViewVaLue.CurrentCell.Value.ToString();
                    for (int j = 0; j < SpecialCharacter.Length; j++)
                        if (StrValue.Contains(SpecialCharacter[j]))
                        {
                            MessageBox.Show("Bạn không được nhập các kí tự đặc biệt " + specialcharacter);
                            break;
                        }
                }
            }
        }

        // Xử lý sự kiện cập nhật giá trị trên TextBox
        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            //if (txtValue.Text[txtValue.Text.Length - 1] != '\n' && specialcharacter.Contains(txtValue.Text[txtValue.Text.Length - 1]))
            //{
            //    MessageBox.Show("Bạn không được nhập kí tự " + txtValue.Text[txtValue.Text.Length - 1]);
            //    txtValue.Text = txtValue.Text.Remove(txtValue.Text.Length - 1) + "";
            //}
        }

        private void ToStringValue() // Phân bố đều
        {
            strvalue = "{";
            for (int i = 0; i < n - 1; i++)
                strvalue += attribute[i].value + ",";
            strvalue += attribute[n - 1].value + "}";
            strvalue += "{";
            for (int i = 0; i < n; i++)
                strvalue += "[" + attribute[i].min + ", " + attribute[i].max + "],";
            strvalue = strvalue.Remove(strvalue.Length - 1);
            strvalue += "}";
        }

        private void ToStringValue(double minprob, double maxprob) // Phân bố rời rạc
        {
            strvalue = "{";
            for (int i = 0; i < n - 1; i++)
                strvalue += attribute[i].value + ",";
            strvalue += attribute[n - 1].value + "}";
            strvalue += "[" + minprob.ToString() + ", " + maxprob.ToString() + "]";
        }
        #endregion
    }
}
