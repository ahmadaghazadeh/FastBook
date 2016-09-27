using System.Data.SQLite;
using System.Web; 

namespace FastBookCreator.Core
{
   
    public class SqliteConn
    {
        const string FileName = "Test.DB.sqlite";
       
        public static SQLiteConnection GetConnection()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{FileName}";
            if (System.IO.File.Exists(file)) return new SQLiteConnection($"Data Source={file};");
            SQLiteConnection.CreateFile(file);
            return new SQLiteConnection($"Data Source={file};");
        }
    }
}