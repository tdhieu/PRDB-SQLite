using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.Data.SQLite;
using System.Windows.Forms;
using PRDB_SQLite.DataAccessLayer;
using PRDB_SQLite.BusinessLogicLayer;
using PRDB_SQLite.PresentationLayer;

namespace PRDB_SQLite.BusinessLogicLayer
{
    public class clsProcess
    {
        #region Các thuộc tính

        private ProbDatabase DB;
        private Thread workerThread;                        // Thread xử lý queries
        private bool connected = false;
        private SQLiteConnection connection = new SQLiteConnection();
        private MethodInvoker task = null;					// next task for the worker thread
        public enum RunState { Idle, Running, Cancelling };
        private RunState runState = RunState.Idle;
        private SQLiteDataAdapter dataAdapter;
        private DataSet dts;
        private bool editmode;
        public bool EditMode { get { return editmode; } set { editmode = value; } }

        #endregion

        #region Các phương thức

        public string GetRootPath(string path)
        {
            string root = "";
            for (int i = 0; i < path.Length; i++)
                if (path[i] == '\\')
                {
                    root = path.Substring(0, i + 1);
                    break;
                }
            return root;
        }

        public ProbDatabase LoadDB(string strPath)
        {
            DB = new ProbDatabase(strPath);
            this.workerThread = new Thread(new ThreadStart(StartWorker));
            workerThread.Name = "DbClient Worker Thread";
            workerThread.Start();
            return DB;
        }

        public bool Connect()
        {
            if (connected) return true;
            RunOnWorker(new MethodInvoker(DoConnect), true);
            return connected;
        }

        public void DoConnect()
        {
            if (connected) return;
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.ConnectionString = Program.ConnectionString;
                    connection.Open();
                    connected = true;
                }
            }
            catch (SQLiteException sqliteEx)
            {
                MessageBox.Show(sqliteEx.Message);
            }
        }

        protected void StartWorker()
        {
            do
            {
                // Chờ luồng chính thức giấc  
                //Thread.CurrentThread.Suspend();
                try { Thread.Sleep(Timeout.Infinite); }
                catch (Exception) { }					// the wakeup call, ie Interrupt() will throw an exception
                // Nếu không làm gì, form thread sẽ được đóng lại
                if (task == null) break;
                // Ngược lại, thực thi công việc được giao
                task();
                task = null;
            } while (true);
        }

        public void RunOnWorker(MethodInvoker method)
        {
            RunOnWorker(method, false);
        }

        public void RunOnWorker(MethodInvoker method, bool synchronous)
        {
            if (task != null) 								// already doing something?
            {
                Thread.Sleep(100);					// give it 100ms to finish...
                if (task != null) return;				// still not finished - cannot run new task
            }
            WaitForWorker();
            task = method;
            workerThread.Interrupt();
            if (synchronous) WaitForWorker();
        }

        public void WaitForWorker()
        {
            while (workerThread.ThreadState != ThreadState.WaitSleepJoin || task != null)
            {
                Thread.Sleep(20);
            }
        }

        protected void StopWorker()
        {
            WaitForWorker();
            // kết thúc luồng
            workerThread.Interrupt();			// ngắt luồng không có task
            workerThread.Join();			    // chờ đến khi kết thúc
        }

        public virtual void Disconnect()
        {
            if (runState == RunState.Running) Cancel();
            if (connected)
                RunOnWorker(new MethodInvoker(connection.Close), true);
        }

        public virtual void Cancel()
        {
            // Dừng một truy vấn đang chạy đồng bộ (chờ cho đến khi truy vấn dừng)
            // Phương thức này được goi khi ta đóng một câu truy vấn đang thực thi.
            if (runState == RunState.Running)
            {
                DoCancel();
                WaitForWorker();
                runState = RunState.Idle;
            }
        }

        protected virtual void DoCancel()
        {
            if (runState == RunState.Running)
            {
                runState = RunState.Cancelling;
                Thread cancelThread = new Thread(new ThreadStart(dataAdapter.SelectCommand.Cancel));
                cancelThread.Name = "DbClient Cancel Thread";
                cancelThread.Start();
                cancelThread.Join();
            }
        }

        public virtual void Dispose()
        {
            if (connected) Disconnect();
            StopWorker();
        }

        public List<string> GetListOfSchema(ProbDatabase DB)
        {
            List<string> List = new List<string>();
            foreach (ProbSchema schema in DB.Schemas)
                List.Add(schema.SchemaName);
            return List;
        }

        public List<string> GetListOfRelation(ProbDatabase DB)
        {
            List<string> List = new List<string>();
            foreach (ProbRelation relation in DB.Relations)
                List.Add(relation.RelationName);
            return List;
        }

        public List<string> GetListOfView(ProbDatabase DB)
        {
            List<string> List = new List<string>();
            foreach (ProbView view in DB.Views)
                List.Add(view.ViewName);
            return List;
        }

        public List<string> GetListOfQuery(ProbDatabase DB)
        {
            List<string> List = new List<string>();
            foreach (ProbQuery query in DB.Queries)
                List.Add(query.QueryName);
            return List;
        }

        public bool NotEmptySchemas(ProbDatabase DB)
        {
            foreach (ProbSchema schema in DB.Schemas)
                if (schema.Relations.Count > 0)
                    return true;
            return false;
        }

        public bool CreateNewDatabase(ProbDatabase DB)// Create new Database and create new system tables 
        {
            SQLiteConnection.CreateFile(DB.DBPath);
            string SQL = "";

            // Record schemas & relations to database
            SQL += "CREATE TABLE SCHEMA ( ";
            SQL += "SchemaName TEXT, ";
            SQL += "RelationName TEXT ";
            SQL += " );";

            // Record views to database
            SQL += "CREATE TABLE VIEW ( ";
            SQL += "SchemaView TEXT, ";
            SQL += "RelationView TEXT, ";
            SQL += "QueryView TEXT ";
            SQL += " );";

            // Record queries to database                
            SQL += "CREATE TABLE QUERY ( ";
            SQL += "QueryName TEXT, ";
            SQL += "QueryText TEXT";
            SQL += " ); ";

            Connection clsConnection = new Connection();
            return clsConnection.CreateTable(SQL);
        }

        public bool LoadDatabase(ProbDatabase DB)
        {
            try
            {
                Connection cls_Connection = new Connection();
                dts = new DataSet();
                dts.Tables.Add(cls_Connection.GetDataTable("SELECT DISTINCT SchemaName from SCHEMA", "dtb_schema"));

                string schname, relname;
                ProbSchema schema;
                ProbRelation relation;

                foreach (DataRow schrow in dts.Tables["dtb_schema"].Rows)
                {
                    // Load Schemas
                    schname = schrow[0].ToString();

                    dts.Tables.Add(cls_Connection.GetDataTable("SELECT * FROM " + schname, schname));
                    DB.DataSet = dts;
                    schema = DB.Add_Schema(schname);

                    dts.Tables.Add(cls_Connection.GetDataTable("SELECT RelationName FROM SCHEMA WHERE SchemaName = '" + schname +"'", "dtb_relation"));

                    foreach (DataRow relrow in dts.Tables["dtb_relation"].Rows)
                        if (!relrow[0].ToString().Equals(""))
                        {
                            // Load Relations
                            relname = relrow[0].ToString();

                            dts.Tables.Add(cls_Connection.GetDataTable("SELECT * FROM " + relname, relname));
                            DB.DataSet = dts;
                            relation = DB.Add_Relation(relname);
                            relation.Schema = schema;
                            relation.Attributes = schema.Attributes;
                            schema.Relations.Add(relation);
                        }
                    dts.Tables.Remove("dtb_relation");
                }

                dts.Tables.Add(cls_Connection.GetDataTable("SELECT * FROM VIEW", "dtb_view"));

                string schview, relview, queryview;
                foreach (DataRow row in dts.Tables["dtb_view"].Rows)
                {
                    // Load views
                    schview = row[0].ToString();
                    relview = row[1].ToString();
                    queryview = row[2].ToString();

                    dts.Tables.Add(cls_Connection.GetDataTable("SELECT * FROM " + schview, schview));
                    dts.Tables.Add(cls_Connection.GetDataTable("SELECT * FROM " + relview, relview));
                    DB.DataSet = dts;
                    DB.Add_View(schview, relview, queryview);
                }

                dts.Tables.Add(cls_Connection.GetDataTable("SELECT * FROM QUERY", "dtb_query"));

                // Load Query
                ProbQuery sql;
                foreach (DataRow row in dts.Tables["dtb_query"].Rows)
                {
                    sql = new ProbQuery(row[1].ToString());
                    sql.QueryName = row[0].ToString();
                    DB.Queries.Add(sql);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return false;
            }
            return true;
        }

        public bool SaveDatabase(ProbDatabase DB)     // Record schemas, relations, queries and views to database
        {
            string SQL = "", DeleteSQL;
            bool temp;
            Connection clsConnection = new Connection();

            try
            {                
                foreach(ProbSchema schema in DB.Schemas)
                    if (schema.NewCreated)
                    {
                        if (schema.Relations.Count > 0)
                        {
                            foreach (ProbRelation relation in schema.Relations)
                                if (relation.NewCreated)
                                {
                                    SQL += "INSERT INTO SCHEMA VALUES (";
                                    SQL += "'" + "sch_" + schema.SchemaName + "'" + ",";
                                    SQL += "'" + "rel_" + relation.RelationName + "'";
                                    SQL += " );";
                                }
                        }
                        else
                        {
                            SQL += "INSERT INTO SCHEMA (SchemaName) VALUES (";
                            SQL += "'" + "sch_" + schema.SchemaName + "'";
                            SQL += ");";
                        }
                    }
                
                // Record schemas and relations to database
                foreach (ProbSchema schema in DB.Schemas)
                {
                    if (schema.NewCreated)
                    {
                        SQL += "CREATE TABLE " + "sch_" + schema.SchemaName + " ( ";
                        SQL += "PrimaryKey TEXT, ";
                        SQL += "AttributeName VARCHAR(50), ";
                        SQL += "AttributeType TEXT, ";
                        SQL += "ValueType TEXT, ";
                        SQL += "Description TEXT";
                        SQL += " ); ";

                        for (int j = 0; j < schema.txtSchema.Rows.Count; j++)
                        {
                            SQL += "INSERT INTO " + "sch_" + schema.SchemaName + " VALUES (";
                            SQL += "'" + schema.txtSchema.Rows[j][0].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][1].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][2].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][3].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][4].ToString() + "'";
                            SQL += " );";
                        }
                        schema.NewCreated = false;
                        schema.Edited = false;
                    }
                    else if (schema.Edited)
                    {
                        // Delete old data on schema table
                        DeleteSQL = "DELETE FROM " + "sch_" + schema.SchemaName + ";";
                        temp = clsConnection.UpdateData(DeleteSQL);

                        for (int j = 0; j < schema.txtSchema.Rows.Count; j++)
                        {
                            SQL += "INSERT INTO " + "sch_" + schema.SchemaName + " VALUES (";
                            SQL += "'" + schema.txtSchema.Rows[j][0].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][1].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][2].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][3].ToString() + "'" + ",";
                            SQL += "'" + schema.txtSchema.Rows[j][4].ToString() + "'";
                            SQL += " );";
                        }
                        schema.Edited = false;
                        schema.NewCreated = false;
                    }

                    foreach (ProbRelation relation in schema.Relations)
                    {
                        if ((relation.NewCreated || relation.Edited) && relation.Attributes.Count > 0)
                        {
                            DeleteSQL = "DROP TABLE " + "rel_" + relation.RelationName + ";";
                            temp = clsConnection.UpdateData(DeleteSQL);

                            SQL += "CREATE TABLE " + "rel_" + relation.RelationName + " ( ";
                            foreach (ProbAttribute attribute in relation.Attributes)
                            {
                                SQL += attribute.Name + " " + "TEXT" + ", ";
                            }
                            SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
                            SQL += " ); ";

                            foreach (ProbTuple tuple in relation.Data)
                            {
                                SQL += "INSERT INTO " + "rel_" + relation.RelationName + " VALUES (";
                                foreach (ProbTriple triple in tuple.Triples)
                                {
                                    SQL += "'" + triple.GetStrValue() + "'" + ",";
                                }
                                SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
                                SQL += " );  ";
                            }

                            relation.NewCreated = false;
                            relation.Edited = false;
                        }
                    }
                }

                // Record views to database
                foreach (ProbView view in DB.Views)                    
                {
                    if (view.NewCreated)
                    {
                        SQL += "INSERT INTO VIEW VALUES (";
                        SQL += "'" + "sch_" + view.ViewName + "'" + ",";
                        SQL += "'" + "rel_" + view.ViewName + "'" + ",";
                        SQL += "'" + view.Query + "'";
                        SQL += " );";

                        SQL += "CREATE TABLE " + "sch_" + view.ViewName + " ( ";
                        SQL += "PrimaryKey TEXT, ";
                        SQL += "AttributeName VARCHAR(50), ";
                        SQL += "AttributeType TEXT, ";
                        SQL += "ValueType TEXT, ";
                        SQL += "Description TEXT";
                        SQL += " ); ";

                        if (view.Schema.Attributes.Count > 0)
                        {
                            foreach (ProbAttribute attribute in view.Schema.Attributes) // Record schema Views
                            {
                                SQL += "INSERT INTO " + "sch_" + view.ViewName + " VALUES (";
                                SQL += "'" + attribute.PrimaryKey.ToString() + "'" + ",";
                                SQL += "'" + attribute.Name + "'" + ",";
                                SQL += "'" + attribute.Type.TypeName + "'" + ",";
                                SQL += "'" + attribute.Type.ValueType + "'" + ",";
                                SQL += "'" + attribute.Description + "'";
                                SQL += " );";
                            }

                            SQL += "CREATE TABLE " + "rel_" + view.ViewName + " ( ";
                            foreach (ProbAttribute attribute in view.Relation.Attributes)
                            {
                                SQL += attribute.Name + " " + "TEXT" + ", ";
                            }
                            SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
                            SQL += " ); ";

                            foreach (ProbTuple tuple in view.Relation.Data)           // Record relation Views
                            {
                                SQL += "INSERT INTO " + "rel_" + view.ViewName + " VALUES (";
                                foreach (ProbTriple triple in tuple.Triples)
                                {
                                    SQL += "'" + triple.GetStrValue() + "'" + ",";
                                }
                                SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
                                SQL += " ); ";
                            }
                        }

                        view.NewCreated = false;
                        view.Edited = false;
                    }
                    else if (view.Edited)
                    {
                        // Drop old schema and relation view tables
                        DeleteSQL = "DELETE FROM " + "sch_" + view.ViewName + ";";
                        temp = clsConnection.UpdateData(DeleteSQL);

                        if (view.Schema.Attributes.Count > 0)
                        {
                            foreach (ProbAttribute attribute in view.Schema.Attributes) // Record schema Views
                            {
                                SQL += "INSERT INTO " + "sch_" + view.ViewName + " VALUES (";
                                SQL += "'" + attribute.PrimaryKey.ToString() + "'" + ",";
                                SQL += "'" + attribute.Name + "'" + ",";
                                SQL += "'" + attribute.Type.TypeName + "'" + ",";
                                SQL += "'" + attribute.Type.ValueType + "'" + ",";
                                SQL += "'" + attribute.Description + "'";
                                SQL += " );";
                            }

                            DeleteSQL = "DROP TABLE " + "rel_" + view.ViewName + ";";
                            temp = clsConnection.UpdateData(DeleteSQL);

                            SQL += "CREATE TABLE " + "rel_" + view.ViewName + " ( ";
                            foreach (ProbAttribute attribute in view.Relation.Attributes)
                            {
                                SQL += attribute.Name + " " + "TEXT" + ", ";
                            }
                            SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
                            SQL += " ); ";

                            foreach (ProbTuple tuple in view.Relation.Data)           // Record relation Views
                            {
                                SQL += "INSERT INTO " + "rel_" + view.ViewName + " VALUES (";
                                foreach (ProbTriple triple in tuple.Triples)
                                {
                                    SQL += "'" + triple.GetStrValue() + "'" + ",";
                                }
                                SQL = SQL.Remove(SQL.LastIndexOf(','), 1);
                                SQL += " ); ";
                            }
                        }

                        view.NewCreated = false;
                        view.Edited = false;
                    }
                }

                // Record queries to database                
                foreach (ProbQuery query in DB.Queries)
                    if (query.NewCreated)
                    {
                        SQL += "INSERT INTO QUERY VALUES (";
                        SQL += "'" + query.QueryName + "'" + ",";
                        SQL += "'" + query.QueryText + "'";
                        SQL += " );";
                        query.NewCreated = false;
                        query.Edited = false;
                    }
                    else if (query.Edited)
                    {
                        SQL += "UPDATE QUERY SET QueryText = " + query.QueryText + " WHERE QueryName = " + query.QueryName + ";";
                        query.Edited = false;
                        query.NewCreated = false;
                    }
            }
            catch
            {
                return false;
            }
            return clsConnection.UpdateData(SQL);
        }

        public bool SaveDatabaseAs(ProbDatabase DB)   // Save database as a different database
        {
            SQLiteConnection.CreateFile(DB.DBPath);
            Connection clsConnection = new Connection();
            bool result;
            string SQL = "";

            // Record schemas to database
            SQL += "CREATE TABLE SCHEMA ( ";
            SQL += "SchemaName TEXT, ";
            SQL += "RelationName TEXT ";
            SQL += " );";

            // Record views to database
            SQL += "CREATE TABLE VIEW ( ";
            SQL += "SchemaView TEXT, ";
            SQL += "RelationView TEXT, ";
            SQL += "QueryView TEXT ";
            SQL += " );";

            // Record queries to database                
            SQL += "CREATE TABLE QUERY ( ";
            SQL += "QueryName TEXT, ";
            SQL += "QueryText TEXT";
            SQL += " ); ";

            return (clsConnection.CreateTable(SQL) && SaveDatabase(DB));
        }

        #endregion
    }
}
