using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class View
    {
        #region Các thuộc tính

        private string viewname;
        public string ViewName { get { return viewname; } set { viewname = value; } }

        private Schema schema;
        public Schema Schema { get { return schema; } set { schema = value; } }

        private Relation relation;
        public Relation Relation { get { return relation; } set { relation = value; } }

        private string query;
        public string Query { get { return query; } set { query = value; } }
        #endregion

        #region Các phương thức

        public View(string name)
        {
            viewname = CutHeading(name, "sch_");
            schema = new Schema(name);
            relation = new Relation(name);
        }

        public string CutHeading(string S, string P)
        {
            for (int i = 0; i < P.Length; i++)
                if (S.Substring(0, i + 1).Equals(P))
                {
                    S = S.Remove(0, i + 1);
                    break;
                }
            return S;
        }

        #endregion
    }
}
