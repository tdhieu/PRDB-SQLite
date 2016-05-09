using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SQLite;
using PRDB_SQLite.DataAccessLayer;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class ProbDatabase
    {
        #region Các thuộc tính

        private bool connected;
        private DataSet dataset;
        public DataSet DataSet { get { return dataset; } set { dataset = value; } }

        // Tên cơ sở dữ liệu
        private string dbname;
        public string DBName { get { return dbname; } set { dbname = value; } }

        private string connectionstring;
        public string ConnectionString { get { return connectionstring; } set { connectionstring = value; } }

        // Đường dẫn đến CSDL
        private string dbpath;
        public string DBPath { get { return dbpath; } set { dbpath = value; } }

        // Lược đồ cơ sở dữ liệu
        private List<ProbSchema> schemas;
        public List<ProbSchema> Schemas { get { return schemas; } set { schemas = value; } }

        // Tập các bảng/quan hệ trong CSDL
        private List<ProbRelation> relations;
        public List<ProbRelation> Relations { get { return relations; } set { relations = value; } }

        private List<ProbView> views;
        public List<ProbView> Views { get { return views; } set { views = value; } }

        // Tập các query trong CSDL
        private List<ProbQuery> queries;
        public List<ProbQuery> Queries { get { return queries; } set { queries = value; } }

        #endregion

        #region Các phương thức

        // Phương thức khởi tạo
        public ProbDatabase(string path)
        {
            // Lấy đường dẫn cho CSDL 
            this.dbpath = path;      
            this.dbname = "";

            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (path[i] == '\\') break;
                else this.dbname = path[i] + dbname;
            }
            // Đặt chuỗi kết nối
            this.connectionstring = "Data Source=" + dbpath + ";Version=3;";
            this.dbname = CutExtension(dbname);
            this.relations = new List<ProbRelation>();
            this.views = new List<ProbView>();
            this.queries = new List<ProbQuery>();
            this.schemas = new List<ProbSchema>();
        }

        public void Rename(string dbName)
        {
            this.dbname = dbName+".pdb";
            int pos = this.dbpath.LastIndexOf('\\');
            this.dbpath = this.dbpath.Remove(pos + 1);
            this.dbpath += dbname;
            this.ConnectionString = "Data Source=" + dbpath + ";Version=3;";
        }

        public ProbSchema Add_Schema(string schname)
        {
            ProbSchema schema = new ProbSchema(schname);
            schema.txtSchema = dataset.Tables[schname];
            schema.LoadAttributes();
            this.schemas.Add(schema);
            return schema;
        }

        public ProbRelation Add_Relation(string relname)
        {
            ProbTuple probTuple;
            ProbTriple probTriple;
            DataTable tbl = dataset.Tables[relname];
            ProbRelation relation = new ProbRelation(relname);

            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                probTuple = new ProbTuple();
                for (int j = 0; j < tbl.Columns.Count; j++)
                {
                    probTriple = new ProbTriple(tbl.Rows[i][j].ToString());
                    probTuple.Triples.Add(probTriple);
                }
                relation.Data.Add(probTuple);
            }
            this.relations.Add(relation);
            return relation;
        }

        public void Add_View(string SchemaView, string RelationView, string QueryView)
        {
            DataTable tbl = dataset.Tables[SchemaView];  // Add schema view
            
            ProbView view = new ProbView(SchemaView);
            view.Query = QueryView;                      // Add query view

            view.Schema.txtSchema = tbl;
            ProbAttribute Attr;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                Attr = new ProbAttribute();
                Attr.PrimaryKey = (tbl.Rows[i][0].ToString().CompareTo("true") == 0 ? true : false);
                Attr.Name = tbl.Rows[i][1].ToString();
                Attr.Type.TypeName = tbl.Rows[i][2].ToString();
                Attr.Type._DataType = Attr.Type.GetDataType();
                view.Schema.Attributes.Add(Attr);
            }

            tbl = dataset.Tables[RelationView];          // Add relation view

            view.Relation.txtSchema = tbl;
            ProbTuple probTuple;
            ProbTriple probTriple;
            for (int i = 0; i < tbl.Rows.Count; i++)
            {
                probTuple = new ProbTuple();
                for (int j = 0; j < tbl.Columns.Count; j++)
                {
                    probTriple = new ProbTriple(tbl.Rows[i][j].ToString());
                    probTuple.Triples.Add(probTriple);
                }
                view.Relation.Data.Add(probTuple);
            }
            view.Relation.Attributes = view.Schema.Attributes;
            views.Add(view);
        }

        public List<string> ListOfSchemaName()
        {
            List<string> List = new List<string>();
            foreach (ProbSchema schema in this.schemas)
                List.Add(schema.SchemaName);
            return List;
        }

        public List<string> ListOfRelationName()
        {
            List<string> List = new List<string>();
            foreach (ProbRelation relation in this.relations)
                List.Add(relation.RelationName);
            return List;
        }

        public List<string> ListOfViewName()
        {
            List<string> List = new List<string>();
            foreach (ProbView view in this.views)
                List.Add(view.ViewName);
            return List;
        }

        public List<string> ListOfQueryName()
        {
            List<string> List = new List<string>();
            foreach (ProbQuery query in this.queries)
                List.Add(query.QueryName);
            return List;
        }

        public void ChangeDir(string path)
        {
            // Thay đổi đường dẫn của CSDL 
            this.dbpath = path;
            this.dbname = "";

            for (int i = path.Length - 1; i >= 0; i--)
            {
                if (path[i] == '\\') break;
                else this.dbname = path[i] + dbname;
            }
            // Đặt chuỗi kết nối
            this.connectionstring = "Data Source=" + dbpath + ";Version=3;";                
        }

        public string CutExtension(string name)
        {
            for (int i=name.Length-1; i>=0; i--)
                if (name[i] == '.')
                {
                    name = name.Remove(i);
                    break;
                }
            return name;
        }

        public bool isNotEdited()
        {
            foreach (ProbSchema schema in schemas)
                if (schema.Edited || schema.NewCreated) return false;

            foreach (ProbRelation relation in relations)
                if (relation.Edited || relation.NewCreated) return false;

            foreach (ProbView view in views)
                if (view.Edited || view.NewCreated) return false;

            foreach (ProbQuery query in queries)
                if (query.Edited || query.NewCreated) return false;

            return true;
        }

        public ProbRelation GetTable(string tbname)
        {
            foreach (ProbRelation table in relations)
                if (table.RelationName.CompareTo(tbname) == 0)
                    return table;
            return null;
        }
        
        public ProbQuery GetQuery(string queryname)
        {
            foreach (ProbQuery query in queries)
                if (query.QueryName.CompareTo(queryname) == 0)
                    return query;
            return null;
        }

        public ProbSchema GetSchema(string SchemaName)
        {
            foreach (ProbSchema schema in this.schemas)
                if (schema.SchemaName.Equals(SchemaName))
                    return schema;
            return null;
        }

        public ProbRelation GetRelation(string RelationName)
        {
            foreach (ProbRelation relation in this.Relations)
                if (relation.RelationName.Equals(RelationName))
                    return relation;
            return null;
        }

        public ProbView GetView(string ViewName)
        {
            foreach (ProbView view in this.views)
                if (view.ViewName.Equals(ViewName))
                    return view;
            return null;
        }

        #endregion
    }
}
