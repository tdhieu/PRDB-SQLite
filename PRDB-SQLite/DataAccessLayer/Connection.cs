using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SQLite;

namespace PRDB_SQLite.DataAccessLayer
{
    public class Connection
    {
        #region Các thuộc tính
        public SQLiteConnection connection;
        public SQLiteCommand command;
        public SQLiteDataAdapter dapt;

        private string commandText;
        public string CommandText { get { return commandText; } set { commandText = value; } }

        private CommandType commandType;
        public CommandType CommandType { get { return commandType; } set { commandType = value; } }

        private string errorMessage;
        public string ErrorMessage { get { return errorMessage; } set { errorMessage = value; } }

        private string[] parameterCollection;
        public string[] ParameterCollection { get { return parameterCollection; } set { parameterCollection = value; } }

        private Object[] valueCollection;
        public Object[] ValueCollection { get { return valueCollection; } set { valueCollection = value; } }

        #endregion

        #region Connection

        public Connection()
        {
            connection = new SQLiteConnection();
            command = new SQLiteCommand();
            dapt = new SQLiteDataAdapter();
        }

        public void OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.ConnectionString = Program.ConnectionString;
                    connection.Open();
                }
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
        }

        #endregion

        #region Query
        public DataSet GetDataSet(string QueryString, string TblName)
        {
            DataSet dts = new DataSet();
            OpenConnection();
            try
            {
                command = new SQLiteCommand();
                command.CommandText = QueryString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                if (parameterCollection != null)
                    AddParametter(command);

                dapt = new SQLiteDataAdapter(command);
                dapt.Fill(dts, TblName);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
            CloseConnection();
            return dts;
        }

        public DataTable GetDataTable(string QueryString, string TblName)
        {
            DataTable dtb = new DataTable(TblName);
            OpenConnection();
            try
            {
                command = new SQLiteCommand();
                command.CommandText = QueryString;
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                if (parameterCollection != null)
                    AddParametter(command);

                dapt = new SQLiteDataAdapter(command);
                dapt.Fill(dtb);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
            CloseConnection();
            return dtb;
        }

        public void AddParametter(SQLiteCommand cmd)
        {
            try
            {
                for (int i = 0; i < parameterCollection.Length; i++)
                    cmd.Parameters.AddWithValue(parameterCollection[i], valueCollection[i]);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
        }

        public bool Existed(string TableName)
        {
            DataTable dtb = new DataTable();
            OpenConnection();
            try
            {
                command = new SQLiteCommand();
                command.CommandText = "SELECT * FROM " + TableName;
                command.Connection = connection;
                command.CommandType = CommandType.Text;

                dapt = new SQLiteDataAdapter(command);
                dapt.Fill(dtb);
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
                CloseConnection();
                return false;
            }
            CloseConnection();
            return (dtb.Rows.Count > 0);
        }

        #endregion

        #region Update

        public bool CreateTable(string sql)
        {
            OpenConnection();
            commandText = sql;
            commandType = CommandType.Text;
            int result = ExecuteNonQuery();
            CloseConnection();
            if (result < 0) return false;
            else return true;
        }

        public bool UpdateData(string sql)
        {
            OpenConnection();
            commandText = sql;
            commandType = CommandType.Text;
            int result = ExecuteNonQuery();
            CloseConnection();
            if (result < 0) return false;
            else return true;
        }

        public int ExecuteNonQuery()
        {
            int rows = 0;
            try
            {
                command = new SQLiteCommand();
                command.CommandText = commandText;
                command.Connection = connection;
                command.CommandType = commandType;

                if (parameterCollection != null)
                    AddParametter(command);

                rows = command.ExecuteNonQuery();
            }
            catch (SQLiteException sqliteEx)
            {
                return -1;
                //errorMessage = sqliteEx.Message;
            }
            finally
            {
                command.Dispose();
            }
            return rows; // trả về số mẫu tin thực thi
        }

        public object ExecuteScalar()
        {
            object objValue = null;
            try
            {
                command = new SQLiteCommand();
                command.CommandText = commandText;
                command.Connection = connection;
                command.CommandText = commandText;

                if (parameterCollection != null)
                    AddParametter(command);

                objValue = command.ExecuteScalar();
            }
            catch (SQLiteException sqliteEx)
            {
                errorMessage = sqliteEx.Message;
            }
            finally
            {
                command.Dispose();
            }
            return objValue;
        }

        #endregion
    }
}
