using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProcessSlCondition
    {
        #region Các thuộc tính
        private string slcondition;
        public string SlCondition { get { return slcondition; } set { slcondition = value; } }

        private ProbTuple tuple;
        public ProbTuple Tuple { get { return tuple; } set { tuple = value; } }

        private string[] Operator = new string[12] { "<", ">", "<=", ">=", "=", "!=", "⊗in", "⊗ig", "⊗me", "⊕in", "⊕ig", "⊕me" };
        private List<Attribute_> Attr = new List<Attribute_>();
        #endregion

        #region Các phương thức
        // Khởi tạo điều kiện chọn
        public ProcessSlCondition(string S)
        {
            slcondition = S;
        }

        // Kiểm tra phép toán là phép so sánh
        public bool isCompareOperator(string S)
        {
            for (int i = 0; i < 6; i++)
                if (Operator[i].CompareTo(S) == 0)
                    return true;
            return false;
        }

        // Kiểm tra phép toán là phép kết hợp
        public bool isCombineOperator(string S)
        {
            for (int i = 6; i < 15; i++)
                if (Operator[i].CompareTo(S) == 0)
                    return true;
            return false;
        }
        
        // Khởi tạo
        public void Initialize(int[] A, string S)
        {
            int n = S.Length, m;
            for (int i = 0; i < n; i++) A[i] = 1;     // ban đầu đánh dấu tất cả đều là giá trị
            for (int i = 0; i < 12; i++)     // Xét các phép toán dựng sẵn
            {
                m = Operator[i].Length;        // Lấy độ dài chuỗi của một phép toán
                for (int j = 0; j < S.Length - m; j++)
                    if (S.Substring(j, m).CompareTo(Operator[i]) == 0)      // Nếu phép toán trên tồn tại ở một vị trí thứ j
                    {
                        switch (m)     // Tùy vào độ dài chuỗi của phép toán mà ta sẽ đánh dấu lại trên mảng A.
                        {
                            case 1: A[j] = 2; break;
                            case 2: A[j] = A[j + 1] = 2; break;
                            case 3: A[j] = A[j + 1] = A[j + 2] = 2; break;
                        }
                    }
            }
        }

        // Đặt độ ưu tiên cho phép toán
        public int Priority(string S)
        {
            switch (S)
            {
                case "<":
                case ">":
                case "<=":
                case ">=":
                case "=":
                case "!=": return 3;
                case "⊗ig":
                case "⊗in":
                case "⊗me":
                case "⊕in":
                case "⊕ig":
                case "⊕me": return 2;
            }
            return 1;
        }

        //Chuyển đổi ra dạng kí pháp Ba Lan
        public List<string> ReversePostfixNotation(string S)
        {
            List<string> Q = new List<string>();
            List<string> P = new List<string>();
            string Operator = "<>!=⊗⊕", temp;
            int[] mark = new int[S.Length];      // Mảng đánh dấu cho biết vị trí thứ i trong chuỗi S là thuộc về giá trị hay toán tử                                                                   
            Initialize(mark, S);
            int i1 = 0;
            for (int i2 = 0; i2 < S.Length - 1; i2++)
                if (mark[i2 + 1] != mark[i2])
                {
                    temp = S.Substring(i1, i2 - i1 + 1);
                    if (mark[i2] == 1) P.Add(temp);    // Nếu temp là giá trị --> thêm vào P
                    else         // Ngược lại --> Thêm vào Q
                    {
                        while (Q.Count > 0 && Priority(Q[Q.Count - 1]) >= Priority(temp))   // Nếu gặp toán tử có độ ưu tiên cao hơn có sẵn trong stack
                        {
                            P.Add(" " + Q[Q.Count - 1] + " ");            // Thêm toán tử này vào P
                            Q.RemoveAt(Q.Count - 1);     // Xóa khỏi stack
                        }
                        Q.Add(temp);     // Ngược lại thêm toán tử vào stack
                    }
                    i1 = i2 + 1;
                }
            P.Add(S.Substring(i1, S.Length - i1));       // Lấy giá trị đang xét sau cùng đưa vào P

            while (Q.Count > 0)      // Sau khi kết thúc, lấy toàn bộ giá trị trong stack đưa vào P
            {
                P.Add(" " + Q[Q.Count - 1] + " ");
                Q.RemoveAt(Q.Count - 1);
            }
            //S = "";
            //for (int i = 0; i < P.Count; i++) S += P[i];       // Tạo chuỗi Postfix Notation mới
            //S = CutSpareSpace(S.Trim());
            return P;
        }

        public bool isOperator(string S)
        {
            S = S.Trim();
            for (int i = 0; i < 12; i++)
                if (Operator[i].CompareTo(S) == 0)
                    return true;
            return false;
        }

        public double min(double a, double b)
        {
            if (a < b) return a;
            return b;
        }

        public double max(double a, double b)
        {
            if (a < b) return b;
            return a;
        }

        public string GetProbInterval(string v1, string v2, string opr)
        {
            int i1 = 0, i2 = 0;
            double minProb = 0, maxProb = 0;
            object value1, value2;
            if (isCompareOperator(opr))        // Nếu là biểu thức điều kiện giữa hai giá trị / thuộc tính
            {
                if (v2.Contains("x."))
                {
                    v1 = v1.Replace("x.", "").Trim();
                    v2 = v2.Replace("x.", "").Trim();
                    for (int i = 0; i < Attr.Count; i++) if (Attr[i].Name.CompareTo(v1) == 0) { i1 = i; break; }
                    for (int i = 0; i < Attr.Count; i++) if (Attr[i].Name.CompareTo(v2) == 0) { i2 = i; break; }
                    for (int j1 = 0; j1 < tuple.Triple[i1].Value.Count; j1++)
                        for (int j2 = 0; j2 < tuple.Triple[i2].Value.Count; j2++)
                        {
                            value1 = tuple.Triple[i1].Value[j1];
                            value2 = tuple.Triple[i2].Value[j2];
                            if (Attr[i1].Compare(value1, value2, opr))
                            {
                                minProb += tuple.Triple[i1].MinProb[j1] * tuple.Triple[i2].MinProb[j2];
                                maxProb = min(1, maxProb + tuple.Triple[i1].MaxProb[j1] * tuple.Triple[i2].MaxProb[j2]);
                            }
                        }
                }
                else
                {
                    v1 = v1.Replace("x.", "").Trim();
                    v2 = v2.Trim();
                    for (int i = 0; i < Attr.Count; i++) if (Attr[i].Name.CompareTo(v1) == 0) { i1 = i; break; }
                    for (int j1 = 0; j1 < tuple.Triple[i1].Value.Count; j1++)
                    {
                        value1 = tuple.Triple[i1].Value[j1];
                        if (Attr[i1].Compare(value1, (object)v2, opr))
                        {
                            minProb += tuple.Triple[i1].MinProb[j1];
                            maxProb = min(1, maxProb + tuple.Triple[i1].MaxProb[j1]);
                        }
                    }
                }
            }
            else             // Nếu là biểu thức điều kiện giữa hai khoảng xác suất
            {
                double L1, L2, U1, U2;
                string[] StrProb;

                v1 = v1.Replace("[", "");  // [L,U]
                v1 = v1.Replace("]", "");
                v1 = v1.Replace(" ", "");
                StrProb = v1.Split(',');
                L1 = Convert.ToDouble(StrProb[0]);
                U1 = Convert.ToDouble(StrProb[1]);

                v2 = v2.Replace("[", "");  // [L,U]
                v2 = v2.Replace("]", "");
                v2 = v2.Replace(" ", "");
                StrProb = v2.Split(',');
                L2 = Convert.ToDouble(StrProb[0]);
                U2 = Convert.ToDouble(StrProb[1]);

                switch (opr)
                {
                    case "⊗ig": minProb = max(0,L1 + L2-1); maxProb = min(U1,U2); break;
                    case "⊗in": minProb = L1 * L2; maxProb = U1 * U2; break;
                    case "⊗me": minProb = 0; maxProb = 0; break;
                    case "⊕ig": minProb = max(L1, L2); maxProb = min(1,U1 + U2); break;
                    case "⊕in": minProb = L1 + L2 - (L1 * L2); maxProb = U1 + U2 - (U1 * U2); break;
                    case "⊕me": minProb = min(1,L1 + L2); maxProb = min(1, U1 + U2); break;
                }
            }
            return "["+minProb.ToString()+","+maxProb.ToString()+"]";
        }

        public bool GetValue(List<string> S, double L, double U)
        {
            List<string> stack = new List<string>();
            string value1, value2;
            for (int i = 0; i < S.Count; i++)
                if (!isOperator(S[i])) stack.Add(S[i]);
                else
                {
                    value1 = stack[stack.Count - 1].Trim();
                    value2 = stack[stack.Count - 2].Trim();
                    stack.RemoveAt(stack.Count - 1);
                    stack.RemoveAt(stack.Count - 2);
                    stack.Add(GetProbInterval(value1, value2, S[i].Trim()));
                }
            string result = stack[0];
            // Lấy khoảng xác suất của biểu thức chọn sau khi tính toán
            int j1 = 1, j2 = result.Length - 2;
            result = result.Substring(j1, j2 - j1 + 1);
            result = result.Replace(" ", "");
            string[] StrProb = result.Split(',');

            double minProb = Convert.ToDouble(StrProb[0]);
            double maxProb = Convert.ToDouble(StrProb[1]);

            return (L <= minProb && maxProb <= U);
        }

        // Xử lý biểu thức chọn
        public bool ExpSelection(string S)    // Có dạng (a > b)[L, U]
        {
            // Lấy khoảng xác suất
            int j1 = S.IndexOf('[') + 1, j2 = S.IndexOf(']') - 1;
            string StrInterval = S.Substring(j1, j2 - j1 + 1);
            S = S.Replace("[" + S.Substring(j1, j2 - j1 + 1) + "]", "");             // Xóa khoảng xác suất trong biểu thức chọn 
            StrInterval = StrInterval.Replace(" ", "");

            string[] StrProb = StrInterval.Split(',');
            double _MinProb = Convert.ToDouble(StrProb[0]);
            double _MaxProb = Convert.ToDouble(StrProb[1]);

            // Lấy biểu thức chọn
            string ExpSelection = S;     // Chuỗi biểu thức chọn

            bool positive = true;
            if (S.Contains("not"))    // Xóa bỏ phép Not trong biểu thức chọn, đặt positive = false
            {
                positive = false;
                ExpSelection = S.Replace("not", "");
            }
            ExpSelection = ExpSelection.Replace("(", "");                             // Cắt bỏ tất cả các kí tự ')' và '(' trong chuỗi điều kiện chọn
            ExpSelection = ExpSelection.Replace(")", "");
            ExpSelection = ExpSelection.Trim();
            List<string> RPN = new List<string>();
            RPN = ReversePostfixNotation(ExpSelection);            // Chuyển đổi về dạng hậu tố
            return (GetValue(RPN, _MinProb, _MaxProb) && positive);
        }

        public bool Satisfied(List<Attribute_> A, ProbTuple _tuple)
        {
            Attr = A;
            tuple = _tuple;
            List<string> result = new List<string>();  // Chuỗi kết quả
            int i1 = 1, i2;

            for (int i = 1; i < slcondition.Length; i++)
            if (slcondition[i] == ' ')
            {
                
                if (slcondition.Substring(i, i+4).CompareTo(" and ") == 0)
                {
                    i2 = i - 1;
                    result.Add(ExpSelection(slcondition.Substring(i1, i2 - i1 + 1)) ? "1" : "0");
                    result.Add("&");
                    i1 = i + 5;
                }

                else if (slcondition.Substring(i, i+3).CompareTo(" or ") == 0)
                {
                    i2 = i - 1;
                    result.Add(ExpSelection(slcondition.Substring(i1, i2 - i1 + 1)) ? "1" : "0");
                    result.Add("|");
                    i1 = i + 4;
                }
            }
            result.Add(ExpSelection(slcondition.Substring(i1, slcondition.Length-i1)) ? "1" : "0");
            bool t, t2;
            t = (result[0].CompareTo("1") == 0 ? true : false);
            int k = 1;
            while (k < result.Count)
            {
                if (result[k].CompareTo("&&") == 0)
                {
                    t2 = (result[k + 1].CompareTo("1") == 0 ? true : false);
                    t = t && t2;
                }
                if (result[k].CompareTo("||") == 0)
                {                 
                    t2 = (result[k + 1].CompareTo("1") == 0 ? true : false);
                    t = t  || t2;
                }
                k++;
            }
            return true;
        }
        #endregion
    }
}
