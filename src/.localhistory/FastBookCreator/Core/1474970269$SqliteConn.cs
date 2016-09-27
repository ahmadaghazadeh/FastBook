using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using Dapper;
using SqliteConnection = System.Data.SQLite.SQLiteConnection;
namespace FastBookCreator.Core
{
   
    public class SqliteConn
    {
        const string FileName = "Test.DB.sqlite";
        private static SqliteConnection _connection;
        public static SQLiteConnection GetConnection()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{FileName}";
            if (_connection != null) return _connection;
            if (!System.IO.File.Exists(file))
            {
                SqliteConnection.CreateFile(file);
                _connection = new SqliteConnection($"Data Source={file};");
            }
            else
            {
                _connection = new SqliteConnection($"Data Source={file};");
            }
 
            return _connection;
        }
    }
}