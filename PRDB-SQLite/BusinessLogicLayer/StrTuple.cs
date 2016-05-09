using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class StrTuple
    {
        #region Các thuộc tính
        private List<string> cells;
        public List<string> Cells { get { return cells; } set { cells = value; } }
        #endregion

        #region Các phương thức
        public StrTuple()
        {
            this.cells = new List<string>();
        }
        #endregion
    }
}
