using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_SQLite.PresentationLayer;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class Table
    {
        #region Các thuộc tính

        //Tên bảng
        private string tbname;            
        public string TBName { get { return tbname; } set { tbname = value; } }

        // Tập các thuộc tính
        private List<Attribute_> attributes;
        public List<Attribute_> Attributes { get { return attributes; } set { attributes = value; } }

        // Tên khóa chính
        private List<string> prkey;
        public List<string> PrKey { get { return prkey; } set { prkey = value; } }

        // Bảng thiết kế
        private List<StrTuple> design;
        public List<StrTuple> Design { get { return design; } set { design = value; } }

        // Bảng dữ liệu
        private List<ProbTuple> data;
        public List<ProbTuple> Data { get { return data; } set { data = value; } }

        #endregion

        #region Các phương thức

        public Table(string name)
        {
            this.tbname = name;
            this.attributes = new List<Attribute_>();
            this.data = new List<ProbTuple>();
            this.design = new List<StrTuple>();
            this.prkey = new List<string>();
        }  // Phương thức khởi tạo

        #endregion
    }
}
