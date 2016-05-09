using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbAttribute
    {
        #region Các thuộc tính

        private bool primarykey;
        public bool PrimaryKey { get { return primarykey; } set { primarykey = value; } }

        // Tên thuộc tính
        private string name;
        public string Name { get { return name; } set { name = value; } }

        // Kiểu dữ liệu của thuộc tính
        private DataType type;
        public DataType Type { get { return type; } set { type = value; } }

        private string description;
        public string Description { get { return description; } set { description = value; } }

        #endregion

        #region Các phương thức
        
        public ProbAttribute()
        {
            this.type = new DataType();
            this.PrimaryKey = false;
        }
        
        #endregion
    }
}
