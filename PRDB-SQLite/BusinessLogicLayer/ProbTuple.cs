using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbTuple    // Biến bộ giá trị xác suất
    {
        #region Các thuộc tính
        // Tập các giá trị bộ ba xác suất trên một tuple
        private List<ProbTriple> triples;
        public List<ProbTriple> Triples { get { return triples; } set { triples = value; } }
        #endregion

        #region Các phương thức
        public ProbTuple()
        {
            this.triples = new List<ProbTriple>();
        }
        #endregion
    }
}
