using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class Attribute_
    {
        #region Các thuộc tính

        // Tên thuộc tính
        private string name;
        public string Name { get { return name; } set { name = value; } }

        // Kiểu dữ liệu của thuộc tính
        private DataType type;
        public DataType Type { get { return type; } set { type = value; } }

        #endregion

        #region Các phương thức
        
        public Attribute_()
        {
            this.type = new DataType();
        }
        
        #endregion
    }
}
