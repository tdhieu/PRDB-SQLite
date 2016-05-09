using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbTriple   // Giá trị bộ ba xác suất
    {
        #region Các thuộc tính

        // Tập các giá trị
        private List<object> values;
        public List<object> Value { get { return values; } set { values = value; } }

        // Tập xác suất cận dưới
        private List<double> minprob;
        public List<double> MinProb { get { return minprob; } set { minprob = value; } }

        // Tập xác suất cận trên
        private List<double> maxprob;
        public List<double> MaxProb { get { return maxprob; } set { maxprob = value; } }

        // Hàm phân phối xác suất
        private bool distribution;
        public bool Distribution { get { return distribution; } set { distribution = value; } }

        #endregion

        #region Các phương thức

        // Tạo bộ ba xác suất từ chuỗi text
        public ProbTriple()
        {
            this.values = new List<object>();
            this.minprob = new List<double>();
            this.maxprob = new List<double>();
        }

        public ProbTriple(string V)
        {
            this.values = new List<object>();
            this.minprob = new List<double>();
            this.maxprob = new List<double>();

            int j, i = V.IndexOf("}")-1;

            string strValues = V.Substring(1, i);
            string[] value = strValues.Split(',');

            for (j = 0; j < value.Length; j++)
            {
                value[j] = value[j].Trim();
                values.Add(value[j]);
            }

            i = i + 2;
            j = V.Length - 1;

            string strProb = V.Substring(i, j-i+1);

             // Xác suất phân bố riêng cho từng giá trị
            if (strProb[0] == '{' && strProb[strProb.Length-1] == '}')
            {
                strProb = strProb.Replace("{", "");  // thay thế { bằng kí tự rỗng
                strProb = strProb.Replace("[", "");  // thay thế [ bằng kí tự rỗng
                strProb = strProb.Replace("]", "");  // thay thế ] bằng kí tự rỗng
                strProb = strProb.Replace("}", "");  // thay thế } bằng kí tự rỗng

                string[] Prob = strProb.Split(',');
                for (j = 0; j < Prob.Length - 1; j+=2)
                {
                    Prob[j] = Prob[j].Trim();
                    Prob[j + 1] = Prob[j + 1].Trim();
                    minprob.Add(Convert.ToDouble(Prob[j]));
                    maxprob.Add(Convert.ToDouble(Prob[j + 1]));
                }
                distribution = false;
            }

            // Xác suất phân bố đều
            else
            {
                strProb = strProb.Replace("[", "");  // thay thế [ bằng kí tự rỗng
                strProb = strProb.Replace("]", "");  // thay thế ] bằng kí tự rỗng
                strProb = strProb.Replace(" ", "");  // thay thế space bằng kí tự rỗng
                strProb = strProb.Replace("u", "");  // thay thế u bằng kí tự rỗng
                string[] Prob = strProb.Split(',');
                int n = values.Count;
                for (j = 0; j < n; j++)
                {
                    minprob.Add(Prob[0] == "" ? 1.0 / n : Convert.ToDouble(Prob[0]) / n);
                    maxprob.Add(Prob[1] == "" ? 1.0 / n : Convert.ToDouble(Prob[1]) / n);
                }
                distribution = true;
            }
        }

        public string GetStrValue()
        {
            string strValue = "{";
            for (int i = 0; i < values.Count-1; i++)
                strValue += values[i].ToString() + ", ";
            strValue += values[values.Count - 1] + "}";
            int n = values.Count;
            if (distribution || n == 1)
            {                
                double min = Convert.ToDouble(minprob[0]) * n;
                double max = Convert.ToDouble(maxprob[0]) * n;
                strValue += "[" + (min != 1 ? min.ToString() : "") + "u, " + (max != 1 ? max.ToString() : "") + "u]";
            }
            else 
            {
                double min, max;
                if (n > 1)
                {
                    strValue += "{";
                    for (int i = 0; i < n - 1; i++)
                    {
                        min = Convert.ToDouble(minprob[i]);
                        max = Convert.ToDouble(maxprob[i]);
                        strValue += "[" + min.ToString() + ", " + max.ToString() + "], ";
                    }
                    min = Convert.ToDouble(minprob[n - 1]);
                    max = Convert.ToDouble(maxprob[n - 1]);
                    strValue += "[" + min.ToString() + ", " + max.ToString() + "]}";
                }
            }
            return strValue;
        }

        #endregion
    }
}
