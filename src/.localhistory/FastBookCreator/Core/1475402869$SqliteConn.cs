﻿using System.Data.SQLite;
using System.IO;
using System.Web; 

namespace FastBookCreator.Core
{
   
    public class SqliteConn
    {
        private const string PackFileName = "PACK.db";
        private const string CommonFileName = "PACK.db";
        public static SQLiteConnection GetPackDb()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{PackFileName}";
            if (System.IO.File.Exists(file)) return new SQLiteConnection($"Data Source={file};");
            var aseetFilePath = $"{HttpContext.Current.Server.MapPath("~")}bin//Asset//{PackFileName}";
            File.Copy(aseetFilePath, file);
            return new SQLiteConnection($"Data Source={file};");
        }

        public static SQLiteConnection GetCommonDb()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{CommonFileName}";
            if (System.IO.File.Exists(file)) return new SQLiteConnection($"Data Source={file};");
            var aseetFilePath = $"{HttpContext.Current.Server.MapPath("~")}bin//Asset//{CommonFileName}";
            File.Copy(aseetFilePath, file);
            return new SQLiteConnection($"Data Source={file};");
        }
    }
}