using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRDB_SQLite.DataAccessLayer
{
    public class NewTable
    {
        private string tablename;
        public string TableName { get { return tablename; } set { tablename = value; } }

        private string[] attributename;
        public string[] AttributeName { get { return attributename; } set { attributename = value; } }

        private string[] typename;
        public string[] TypeName { get { return typename; } set { typename = value; } }

    }
}
