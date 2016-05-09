using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbView
    {
        #region Các thuộc tính

        private string viewname;
        public string ViewName { get { return viewname; } set { viewname = value; } }

        private ProbSchema schema;
        public ProbSchema Schema { get { return schema; } set { schema = value; } }

        private ProbRelation relation;
        public ProbRelation Relation { get { return relation; } set { relation = value; } }

        private string query;
        public string Query { get { return query; } set { query = value; } }

        private bool edited;
        public bool Edited { get { return edited; } set { edited = value; } }

        private bool newcreated;
        public bool NewCreated { get { return newcreated; } set { newcreated = value; } }

        #endregion

        #region Các phương thức

        public ProbView()
        {
            schema = new ProbSchema();
            relation = new ProbRelation();
        }

        public ProbView(string name)
        {
            viewname = CutHeading(name, "sch_");
            schema = new ProbSchema(name);
            relation = new ProbRelation(name);
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
