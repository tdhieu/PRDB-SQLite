using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbSchema
    {
        #region Các thuộc tính

        private string schemaname;
        public string SchemaName { get { return schemaname; } set { schemaname = value; } }

        // Tap cac quan he tuong ung voi luoc do quan he
        private List<ProbRelation> relations;
        public List<ProbRelation> Relations { get { return relations; } set { relations = value; } }

        // Tập các thuộc tính
        private List<ProbAttribute> attributes;
        public List<ProbAttribute> Attributes { get { return attributes; } set { attributes = value; } }

        // Bảng thiết kế
        private DataTable txtschema;
        public DataTable txtSchema { get { return txtschema; } set { txtschema = value; } }

        private bool edited;
        public bool Edited { get { return edited; } set { edited = value; } }

        private bool newcreated;
        public bool NewCreated { get { return newcreated; } set { newcreated = value; } }

        #endregion

        #region Các phương thức

        public ProbSchema()
        {
            this.attributes = new List<ProbAttribute>();
            this.txtschema = new DataTable();
            this.relations = new List<ProbRelation>();
        }

        public ProbSchema(string schemaname)
        {
            this.schemaname = CutHeading(schemaname, "sch_");
            this.attributes = new List<ProbAttribute>();
            this.txtschema = new DataTable();
            this.relations = new List<ProbRelation>();
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

        public void LoadAttributes()
        {           
            ProbAttribute Attr;
            for (int i = 0; i < txtschema.Rows.Count; i++)
            {
                Attr = new ProbAttribute();
                Attr.PrimaryKey = (txtschema.Rows[i][0].ToString().Equals("true") ? true : false);
                Attr.Name = txtschema.Rows[i][1].ToString();
                Attr.Type.TypeName = txtschema.Rows[i][2].ToString();
                Attr.Type._DataType = Attr.Type.GetDataType();
                Attr.Type.ValueType = txtschema.Rows[i][3].ToString();
                Attr.Description = (txtschema.Rows[i][4] == null ? "" : txtschema.Rows[i][4].ToString());
                this.Attributes.Add(Attr);
            }
        }

        #endregion
    }
}
