using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PRDB_SQLite.PresentationLayer;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbRelation: ProbSchema
    {
        #region Các thuộc tính

        private ProbSchema schema;
        public ProbSchema Schema { get { return schema; } set { schema = value; } }

        private string relationname;
        public string RelationName { get { return relationname; } set { relationname = value; } }

        // Primary Key
        private List<string> prkey;
        public List<string> PrKey { get { return prkey; } set { prkey = value; } }

        private List<ProbTuple> data;
        public List<ProbTuple> Data { get { return data; } set { data = value; } }

        private bool edited;
        public bool Edited { get { return edited; } set { edited = value; } }

        private bool newcreated;
        public bool NewCreated { get { return newcreated; } set { newcreated = value; } }

        #endregion

        #region Các phương thức

        public ProbRelation() // Constructor
        {
            this.Schema = new ProbSchema();
            this.Attributes = new List<ProbAttribute>();
            this.data = new List<ProbTuple>();
            this.prkey = new List<string>();
        }

        public ProbRelation(string relationname)  // Constructor
        {
            this.relationname = CutHeading(relationname,"rel_");
            this.Schema = new ProbSchema();
            this.Attributes = new List<ProbAttribute>();
            this.data = new List<ProbTuple>();
            this.prkey = new List<string>();
        }

        public string CutHeading(string S, string P)
        {
            for (int i = 0; i < S.Length; i++)
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
