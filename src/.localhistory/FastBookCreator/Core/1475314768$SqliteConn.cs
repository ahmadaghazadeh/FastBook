using System.Data.SQLite;
using System.Web; 

namespace FastBookCreator.Core
{
   
    public class SqliteConn
    {
        private const string PackFileName = "pack.sqlite";
        private const string CommonFileName = "pack.sqlite";
        public static SQLiteConnection GetPackDb()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{PackFileName}";
            if (System.IO.File.Exists(file)) return new SQLiteConnection($"Data Source={file};");
            SQLiteConnection.CreateFile(file);
            return new SQLiteConnection($"Data Source={file};");
        }

        public static SQLiteConnection GetCommonDb()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{CommonFileName}";
            if (System.IO.File.Exists(file)) return new SQLiteConnection($"Data Source={file};");
            SQLiteConnection.CreateFile(file);
            return new SQLiteConnection($"Data Source={file};");
        }
    }
}