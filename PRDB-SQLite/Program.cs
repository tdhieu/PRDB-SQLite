using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace PRDB_SQLite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form_Main());
        }
        static public string dbShowName;
        static public string dbName;
        static public string ConnectionString;
        static public List<string> SchemaName = new List<string>();
        static public List<string> RelationName = new List<string>();
        static public List<string> ViewName = new List<string>();
        static public List<string> QueryName = new List<string>();        
    }
}
