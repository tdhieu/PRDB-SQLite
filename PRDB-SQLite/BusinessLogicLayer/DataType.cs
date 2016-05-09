using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class DataType  // Kiểu dữ liệu của thuộc tính
    {
        #region Các thuộc tính
        // Tên kiểu dữ liệu
        private string typename;
        public string TypeName { get { return typename; } set { typename = value; } }

        // Kiểu dữ liệu
        private string datatype;             
        public string _DataType { get { return datatype; } set { datatype = value; } }

        // Trường giá trị của kiểu
        private string valuetype;        
        public string ValueType { get { return valuetype; } set { valuetype = value; } }
        #endregion

        #region Các phương thức
        // Lấy DataType từ TypeName
        public string GetDataType()
        {
            string[] type = new string[] { "Int16", "Int32", "Int64", "Byte", "String", "Single", "Double", "Boolean", "Decimal", "DateTime", "Binary", "Currency", "UserDefined"};
            string datatype = "";
            for (int i=0; i<type.Length; i++)
                if (typename.CompareTo(type[i]) == 0)
                {
                    datatype = typename;
                    break;
                }
            if (datatype == "") datatype = "UserDefined";
            return datatype;
        }

        #endregion
    }
}
