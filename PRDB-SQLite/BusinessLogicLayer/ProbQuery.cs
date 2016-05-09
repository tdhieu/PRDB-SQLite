using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbQuery
    {
        #region Các thuộc tính

        private string queryname;
        public string QueryName { get { return queryname; } set { queryname = value; } }

        private string querytext;
        public string QueryText { get { return querytext; } set { querytext = value; } }

        private string errorMessage;
        public string ErrorMessage { get { return errorMessage; } set { errorMessage = value; } }

        private List<ProbRelation> relations;
        public List<ProbRelation> Relations { get { return relations; } set { relations = value; } }
                                                            
        private List<ProbAttribute> attributes;
        public List<ProbAttribute> Attributes { get { return attributes; } set { attributes = value; } }

        private List<ProbTuple> SatisfiedTuples;        
        private string selectcondition;

        private bool edited;
        public bool Edited { get { return edited; } set { edited = value; } }

        private bool newcreated;
        public bool NewCreated { get { return newcreated; } set { newcreated = value; } }

        #endregion

        #region Các phương thức

        public ProbQuery()
        {
            attributes = new List<ProbAttribute>();
            SatisfiedTuples = new List<ProbTuple>();
            relations = new List<ProbRelation>();
        }

        public ProbQuery(string query)  // Constructor
        {
            querytext = query.Trim();
            querytext = querytext.Replace("\n", " ");
            attributes = new List<ProbAttribute>();
            SatisfiedTuples = new List<ProbTuple>();
            relations = new List<ProbRelation>();
        }
        
        // cut spare space
        public string CutSpareSpace(string S)
        {
            string result = "";
            for (int i = 0; i < S.Length; i++)
                if (S[i] == ' ')
                {
                    if (S[i - 1] != ' ') result += S[i];
                }
                else result += S[i];
            return result;
        }

        // get attributes in query text
        public List<ProbAttribute> GetAttributes(string S)
        {
            int j1, j2;
            List<ProbAttribute> result = new List<ProbAttribute>();
            j1 = S.IndexOf("select ") + 7;                                                   // start postion of attributes
            j2 = S.IndexOf("from ") - 2;                                                     // end postion of attributes
            string attributes = S.Substring(j1, j2 - j1 + 1).Replace(" ", "");
            if (attributes.CompareTo("*") == 0)                                              // get all attributes in the relation
                return this.relations[0].Attributes;
            else                                                                             // get selected attributes
            {
                foreach (ProbAttribute attribute in this.relations[0].Attributes)
                    if (attributes.Contains(attribute.Name))          
                        result.Add(attribute);
            }
            return result;
        }

        // get relations in query text
        public List<ProbRelation> GetRelations(ProbDatabase DB)
        {
            int j1, j2;
            string S = this.querytext;
            List<ProbRelation> result = new List<ProbRelation>();

            j1 = S.IndexOf("from ") + 5;                                                      // start position of relation name
            j2 = (S.Contains("where ") ? S.IndexOf("where ") - 2 : S.Length - 1);             // end position of relation name
            string relName = S.Substring(j1, j2 - j1 + 1).Trim();
            foreach (ProbRelation relation in DB.Relations)
                if (relation.RelationName.CompareTo(relName) == 0)
                    result.Add(relation);
            return result;
        }

        // Lấy điều kiện chọn trong truy vấn
        public string GetCondition(string S)
        {
            int i = S.IndexOf("where ") + 6;
            return S.Substring(i).Trim();
        }

        // Kiểm tra lỗi cú pháp
        public bool CheckSyntax()
        {
            string S = querytext;
            if (S.Contains("select ") && S.Contains("from "))
            {
                if (S.IndexOf("select ") != S.LastIndexOf("select ")) return false;
                if (S.IndexOf("from ") != S.LastIndexOf("from ")) return false;
                if (S.Contains("where ") && S.IndexOf("where ") != S.LastIndexOf("where ")) return false;
                return true;
            }
            return false;
        }

        // Thực thi truy vấn
        public List<ProbTuple> Execute()
        {
            SatisfiedTuples = new List<ProbTuple>();
            if (querytext.Contains(" where "))                                   // the query has select condition
            {
                selectcondition = GetCondition(querytext);                       // the select condition has the form of ((a < b)[L,U] and (a > b)[L,U] or (not(a = b)[L,U]))
                SelectionCondition Condition = new SelectionCondition(this.relations[0], selectcondition);
                foreach (ProbTuple tuple in this.relations[0].Data)
                    if (Condition.Satisfied(tuple))
                        SatisfiedTuples.Add(tuple);
            }
            else SatisfiedTuples = this.relations[0].Data;
            return SelectAttributes(SatisfiedTuples);
        }

        private List<ProbTuple> SelectAttributes(List<ProbTuple> tuples)
        {
            List<int> index = new List<int>();
            string S = this.querytext;
            int j1 = S.IndexOf("select ") + 7;                                    // start position of attributes
            int j2 = S.IndexOf("from ") - 2;                                      // end position of attributes 
            string strAtr = S.Substring(j1, j2 - j1 + 1).Replace(" ", "");        // the string contains attributes' names  
            if (strAtr.CompareTo("*") == 0)                                       // if the attribute is * 
            {
                for (int i = 0; i < this.relations[0].Attributes.Count; i++)
                {
                    this.Attributes.Add(this.relations[0].Attributes[i]);
                    index.Add(i);
                }
            }
            else
            {
                for (int i = 0; i < this.relations[0].Attributes.Count; i++)
                    if (strAtr.Contains(this.relations[0].Attributes[i].Name))
                    {
                        this.Attributes.Add(this.relations[0].Attributes[i]);
                        index.Add(i);
                    }
            }

            List<ProbTuple> result = new List<ProbTuple>();
            ProbTuple newTuple;
            foreach (ProbTuple tuple in tuples)
            {
                newTuple = new ProbTuple();
                foreach (int i in index)
                    newTuple.Triples.Add(tuple.Triples[i]);
                result.Add(newTuple);    
            }

            return result;
        }

        #endregion
    }
}
