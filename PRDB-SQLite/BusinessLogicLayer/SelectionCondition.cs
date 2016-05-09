using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numeric;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class SelectionCondition
    {
        #region Các thuộc tính
        private string slcondition;
        public string SlCondition { get { return slcondition; } set { slcondition = value; } }

        private ProbTuple tuple;
        public ProbTuple Tuple { get { return tuple; } set { tuple = value; } }

        private string errormessage;
        public string ErrorMessage { get { return errormessage; } set { errormessage = value; } }

        static public string[] Operator = new string[15] { "_<", "_>", "<=", ">=", "_=", "!=", "⊗_in", "⊗_ig", "⊗_me", "⊕_in", "⊕_ig", "⊕_me", "EQUAL(in)", "EQUAL(ig)", "EQUAL(me)" };
        private ProbRelation SelectedTable;
        private List<ProbAttribute> Attributes = new List<ProbAttribute>();
        #endregion

        #region Các phương thức
        public SelectionCondition(ProbRelation table, string S)
        {
            int i = 0;
            while (i < S.Length - 1)
            {
                if (S[i] == '<' && S[i + 1] != '=')
                    S = S.Insert(i++, "_");
                if (S[i] == '>' && S[i + 1] != '=')
                    S = S.Insert(i++, "_");
                if (S[i] == '=' && S[i - 1] != '!' && S[i - 1] != '<' && S[i - 1] != '>')
                    S = S.Insert(i++, "_");
                i++;
            }
            slcondition = S;
            SelectedTable = table;
            Attributes = table.Attributes;
        }

        public bool isCompareOperator(string S)
        {
            for (int i = 0; i < 6; i++)
                if (Operator[i].CompareTo(S) == 0)
                    return true;
            return false;
        }

        public bool isCombineOperator(string S)
        {
            for (int i = 6; i < 15; i++)
                if (Operator[i].CompareTo(S) == 0)
                    return true;
            return false;
        }

        public bool isOperator(string S)
        {
            for (int i = 0; i < 15; i++)
                if (Operator[i].CompareTo(S) == 0)
                    return true;
            return false;
        }

        public string AddSeperateCharacter(string S)
        {
            // Thêm vào các kí tự phân cách
            S = S.Replace("(", "|(|");
            S = S.Replace(")", "|)|");
            S = S.Replace("_<", "|_<|");
            S = S.Replace("_>", "|_>|");
            S = S.Replace("<=", "|<=|");
            S = S.Replace(">=", "|>=|");
            S = S.Replace("_=", "|_=|");
            S = S.Replace("!=", "|!=|");
            S = S.Replace("⊗_in", "|⊗_in|");
            S = S.Replace("⊗_ig", "|⊗_ig|");
            S = S.Replace("⊗_me", "|⊗_me|");
            S = S.Replace("⊕_in", "|⊕_in|");
            S = S.Replace("⊕_ig", "|⊕_ig|");
            S = S.Replace("⊕_me", "|⊕_me|");
            S = S.Replace("EQUAL(in)", "|EQUAL(in)|");
            S = S.Replace("EQUAL(ig)", "|EQUAL(ig)|");
            S = S.Replace("EQUAL(me)", "|EQUAL(me)|");
            while (S[0] == '|') S = S.Remove(0, 1);
            int i;
            for (i = S.Length; i >= 0; i--)
                if (S[i - 1] != '|') break;
            if (i < S.Length) S = S.Remove(i);
            i = 0;
            while (i < S.Length)
            {
                if (S[i] == ' ' && (S[i + 1] == '|' || S[i - 1] == '|'))
                    S = S.Remove(i, 1);
                i++;
            }
            return S;
        }

        public string CutSpareSpace(string S)
        {
            string result = "";
            for (int i = 0; i < S.Length; i++)
                if (S[i] == ' ')
                {
                    if (S[i - 1] != ' ') result += S[i];
                }
                else result += S[i];
            return result;
        }

        public int Priority(string S)
        {
            switch (S)
            {
                case "_<":
                case "_>":
                case "<=":
                case ">=":
                case "_=":
                case "!=":
                case "EQUAL(ig)":
                case "EQUAL(in)":
                case "EQUAL(me)": return 3;
                case "⊗_ig":
                case "⊗_in":
                case "⊗_me":
                case "⊕_in":
                case "⊕_ig":
                case "⊕_me": return 2;
                case "|":
                case "&": return 2;
                case "!": return 3;
                default: return 0;
            }
        }        

        public List<string> SE_PostfixNotation(string S)
        {
            List<string> O = new List<string>();  // Stack
            List<string> V = new List<string>();
            string[] A = AddSeperateCharacter(S).Split('|');

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i].CompareTo("(") == 0) O.Add(A[i]);
                else if (A[i].CompareTo(")") == 0)
                {
                    while (O.Count > 0 && O[O.Count - 1].CompareTo("(") != 0)
                    {
                        V.Add(O[O.Count - 1]);     // V.Add(" " + O[O.Count - 1] + " ");
                        O.RemoveAt(O.Count - 1);
                    }
                    O.RemoveAt(O.Count - 1);      // remove '('
                }
                else if (isOperator(A[i]))        // operator
                {
                    while (O.Count > 0 && Priority(O[O.Count - 1]) >= Priority(A[i]))
                    {
                        V.Add(O[O.Count - 1]);        // V.Add(" " + O[O.Count - 1] + " ");
                        O.RemoveAt(O.Count - 1);
                    }
                    O.Add(A[i]);
                }
                else V.Add(A[i]);           // value
            }
            while (O.Count > 0)      // Sau khi kết thúc, lấy toàn bộ giá trị trong stack đưa vào V
            {
                V.Add(O[O.Count - 1]);
                O.RemoveAt(O.Count - 1);
            }
            return V;
        }

        public List<string> SC_PostfixNotation(string S)
        {
            List<string> O = new List<string>();  // Stack
            List<string> V = new List<string>();

            S = S.Replace("(", " ( ");
            S = S.Replace(")", " ) ");
            S = S.Replace("&", " & ");
            S = S.Replace("|", " | ");
            S = S.Replace("!", " ! ");
            S = CutSpareSpace(S.Trim());

            string[] A = S.Split(' ');

            for (int i = 0; i < A.Length; i++)
            {
                if (A[i].CompareTo("(") == 0) O.Add(A[i]);
                else if (A[i].CompareTo(")") == 0)
                {
                    while (O.Count > 0 && O[O.Count - 1].CompareTo("(") != 0)
                    {
                        V.Add(O[O.Count - 1]);     // V.Add(" " + O[O.Count - 1] + " ");
                        O.RemoveAt(O.Count - 1);
                    }
                    O.RemoveAt(O.Count - 1);      // remove '('
                }
                else if (A[i].CompareTo("&") == 0 || A[i].CompareTo("|") == 0 || A[i].CompareTo("!") == 0)        // operator
                {
                    while (O.Count > 0 && Priority(O[O.Count - 1]) >= Priority(A[i]))
                    {
                        V.Add(O[O.Count - 1]);        // V.Add(" " + O[O.Count - 1] + " ");
                        O.RemoveAt(O.Count - 1);
                    }
                    O.Add(A[i]);
                }
                else V.Add(A[i]);           // value
            }
            while (O.Count > 0)      // Sau khi kết thúc, lấy toàn bộ giá trị trong stack đưa vào V
            {
                V.Add(O[O.Count - 1]);
                O.RemoveAt(O.Count - 1);
            }
            return V;
        }

        public bool isSelectionExpression(string S, int a, int b)
        {
            string S1 = S.Substring(a, b - a + 1).ToLower();
            if (S1.Contains(" and ") || S1.Contains(" or ") || S1.Contains(" not "))
                return false;
            return true;
        }

        public int IndexOf(string S)
        {
            for (int i = 0; i < Attributes.Count; i++)
                if (Attributes[i].Name.CompareTo(S) == 0)
                    return i;
            return -1;
        }

        public bool IntCompare(Int16 a, Int16 b, string Opr)
        {
            switch (Opr)
            {
                case "_<": return (a < b);
                case "_>": return (a > b);
                case "<=": return (a <= b);
                case ">=": return (a >= b);
                case "_=": return (a == b);
                case "!=": return (a != b);
            }
            return false;
        }

        public bool StrCompare(string a, string b, string Opr)
        {
            switch (Opr)
            {
                case "_=": return (a.CompareTo(b) == 0);
                case "!=": return (a.CompareTo(b) != 0);
            }
            return false;
        }

        public bool DblCompare(double a, double b, string Opr)
        {
            switch (Opr)
            {
                case "_<": return (a < b);
                case "_>": return (a > b);
                case "<=": return (a <= b);
                case ">=": return (a >= b);
                case "_=": return (Math.Abs(a - b) < 0.001);
                case "!=": return (Math.Abs(a - b) > 0.001);
            }
            return false;
        }

        public bool BoolCompare(bool a, bool b, string Opr)
        {
            switch (Opr)
            {
                case "_=": return (a == b);
                case "!=": return (a != b);
            }
            return false;
        }

        public bool Compare(object a, string b, string opr, string type)
        {
            switch (type)
            {
                case "Int16":
                case "Int64":
                case "Int32":
                case "Byte":
                case "Decimal":
                case "Currency": return IntCompare(Convert.ToInt16(a), Convert.ToInt16(b), opr);
                case "String":
                case "DateTime":
                case "UserDefined":
                case "Binary": return StrCompare(a.ToString(), b, opr);
                case "Single":
                case "Double": return DblCompare((double)a, Convert.ToDouble(b), opr);
                case "Boolean": return BoolCompare((bool)a, Convert.ToBoolean(b), opr); 
            }
            return false;
        }

        public bool EQUAL(object a, object b, string type)
        {
            switch (type)
            {
                case "Int16":
                case "Int64":
                case "Int32":
                case "Byte":
                case "Decimal":
                case "Currency": return ((int)a == (int)b);
                case "String":
                case "DateTime":
                case "UserDefined":
                case "Binary": return (a.ToString().CompareTo(b.ToString()) == 0);
                case "Single":
                case "Double": return (Math.Abs((double)a - (double)b) < 0.001);
                case "Boolean": return ((bool)a == (bool)b);
            }
            return false;
        }

        public string GetProbInterval(string s1, string s2, string opr)
        {
            double minProb = 0, maxProb = 0;
            int j1, j2, n1, n2;
            string typename;

            try
            {
                if (opr.Contains("EQUAL"))         // Biểu thức so sánh bằng giữa hai thuộc tính trên cùng một bộ
                {
                    j1 = IndexOf(s1);
                    j2 = IndexOf(s2);
                    n1 = tuple.Triples[j1].Value.Count;
                    n2 = tuple.Triples[j2].Value.Count;
                    typename = Attributes[j1].Type._DataType;
                    for (int i1 = 0; i1 < n1; i1++)
                        for (int i2 = 0; i2 < n2; i2++)
                            if (EQUAL(tuple.Triples[j1].Value[i1], tuple.Triples[j2].Value[i2], typename))
                                switch (opr)
                                {
                                    case "EQUAL(in)":   minProb += tuple.Triples[j1].MinProb[i1] * tuple.Triples[j2].MinProb[i2];
                                                        maxProb = Math.Min(1, maxProb + tuple.Triples[j1].MaxProb[i1] * tuple.Triples[j2].MaxProb[i2]);
                                                        break;
                                    case "EQUAL(ig)":   minProb += Math.Min(0, tuple.Triples[j1].MinProb[i1] + tuple.Triples[j2].MinProb[i2] - 1);
                                                        maxProb = Math.Min(1, maxProb + Math.Min(tuple.Triples[j1].MaxProb[i1], tuple.Triples[j2].MaxProb[i2]));
                                                        break;
                                    case "EQUAL(me)":   minProb += 0;
                                                        maxProb = Math.Min(1, maxProb + 0);
                                                        break;
                                }
                }
                else if (isCompareOperator(opr))            // Biểu thức so sánh giữa một thuộc tính với một giá trị
                {
                    j1 = IndexOf(s1);
                    n1 = tuple.Triples[j1].Value.Count;
                    typename = Attributes[j1].Type._DataType;
                    for (int i = 0; i < n1; i++)
                        if (Compare(tuple.Triples[j1].Value[i], s2, opr, typename))
                        {
                            minProb += tuple.Triples[j1].MinProb[i];
                            maxProb += tuple.Triples[j1].MaxProb[i];
                        }
                }
                else                     // Biểu thức kết hợp giữa hai khoảng xác suất
                {
                    double L1, L2, U1, U2;
                    string[] StrProb;

                    s1 = s1.Replace("[", "");  // [L,U]
                    s1 = s1.Replace("]", "");
                    StrProb = s1.Split(',');
                    L1 = Convert.ToDouble(StrProb[0]);
                    U1 = Convert.ToDouble(StrProb[1]);

                    s2 = s2.Replace("[", "");  // [L,U]
                    s2 = s2.Replace("]", "");
                    StrProb = s2.Split(',');
                    L2 = Convert.ToDouble(StrProb[0]);
                    U2 = Convert.ToDouble(StrProb[1]);

                    switch (opr)
                    {
                        case "⊗_ig": minProb = Math.Max(0, L1 + L2 - 1); maxProb = Math.Min(U1, U2); break;
                        case "⊗_in": minProb = L1 * L2; maxProb = U1 * U2; break;
                        case "⊗_me": minProb = 0; maxProb = 0; break;
                        case "⊕_ig": minProb = Math.Max(L1, L2); maxProb = Math.Min(1, U1 + U2); break;
                        case "⊕_in": minProb = L1 + L2 - (L1 * L2); maxProb = U1 + U2 - (U1 * U2); break;
                        case "⊕_me": minProb = Math.Min(1, L1 + L2); maxProb = Math.Min(1, U1 + U2); break;
                    }
                }
            }
            catch 
            {
                errormessage = "Không tìm thấy tên thuộc tính";
            }

            return ("[" + minProb.ToString() + "," + maxProb.ToString() + "]");
        }

        public bool CalculateExpression(List<string> S, double L, double U)
        {
            List<string> stack = new List<string>();
            string value1, value2;
            for (int i = 0; i < S.Count; i++)
                if (!isOperator(S[i])) stack.Add(S[i]);  // Nếu là giá trị thì add vào stack
                else         // Lấy hai giá trị ra khỏi stack và tính toán
                {
                    value2 = stack[stack.Count - 1].Trim();
                    value1 = stack[stack.Count - 2].Trim();
                    stack.RemoveAt(stack.Count - 1);
                    stack.RemoveAt(stack.Count - 1);
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

        public bool CalculateCondition(List<string> S)
        {
            List<string> stack = new List<string>();
            string value1, value2;
            for (int i = 0; i < S.Count; i++)
                if (S[i].CompareTo("!") == 0)
                {
                    value1 = stack[stack.Count - 1].Trim();
                    stack.RemoveAt(stack.Count - 1);
                    stack.Add((value1.CompareTo("1") == 0) ? "0" : "1");
                }
                else if (S[i].CompareTo("&") == 0 || S[i].CompareTo("|") == 0)         // Lấy hai giá trị ra khỏi stack và tính toán
                {
                    value2 = stack[stack.Count - 1].Trim();
                    value1 = stack[stack.Count - 2].Trim();
                    stack.RemoveAt(stack.Count - 1);
                    stack.RemoveAt(stack.Count - 1);
                    bool v1 = (value1.CompareTo("1") == 0) ? true : false;
                    bool v2 = (value2.CompareTo("1") == 0) ? true : false;
                    switch (S[i])
                    {
                        case "&": stack.Add(v1 && v2 ? "1" : "0"); break;
                        case "|": stack.Add(v1 || v2 ? "1" : "0"); break;
                    }
                }
                else stack.Add(S[i]);          // Nếu là giá trị thì add vào stack
            return (stack[0].CompareTo("1") == 0);
        }        

        public bool ExpressionValue(string S)    
        {
            // Get Probabilistic Interval
            int j1 = S.IndexOf('[') + 1, j2 = S.IndexOf(']') - 1;
            string StrInterval = S.Substring(j1, j2 - j1 + 1);
            S = S.Replace("[" + StrInterval + "]", "");
            StrInterval = StrInterval.Replace(" ", "");
            string[] StrProb = StrInterval.Split(',');
            double _MinProb = Convert.ToDouble(StrProb[0]);
            double _MaxProb = Convert.ToDouble(StrProb[1]);

            // Get Selection Expression
            S = S.Trim();
            List<string> rpn = new List<string>();
            rpn = SE_PostfixNotation(S);            // reverse to postfix notation 
            return CalculateExpression(rpn, _MinProb, _MaxProb);
        }

        public bool Satisfied(ProbTuple _tuple)
        {
            tuple = _tuple;
            int j, i = 0;
            string expValue, slCondition;

            slCondition = slcondition;

            while (i < slCondition.Length - 1)
            {
                if (slCondition[i] == '(')
                {
                    j = i + 1;
                    while (j < slCondition.Length)
                    {
                        if (slCondition[j] == ']' && isSelectionExpression(slCondition, i, j))
                        {
                            expValue = ExpressionValue(slCondition.Substring(i, j - i + 1)) ? "1" : "0";
                            slCondition = slCondition.Insert(i, expValue);
                            slCondition = slCondition.Remove(i + 1, j - i + 1);
                            break;
                        }
                        j++;
                    }
                }
                i++;
            }
            slCondition = slCondition.Replace(" ", "").ToLower();
            slCondition = slCondition.Replace("and", "&");
            slCondition = slCondition.Replace("or", "|");
            slCondition = slCondition.Replace("not", "!");
            List<string> rpn = SC_PostfixNotation(slCondition);            // reverse to postfix notation 
            return CalculateCondition(rpn);
        }
        #endregion
    }
}
