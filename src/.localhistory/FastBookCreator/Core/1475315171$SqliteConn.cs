﻿using System.Data.SQLite;
using System.IO;
using System.Web; 

namespace FastBookCreator.Core
{
   
    public class SqliteConn
    {
        private const string PackFileName = "pack.sqlite";
        private const string CommonFileName = "common.sqlite";
        public static SQLiteConnection GetPackDb()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{PackFileName}";
            if (System.IO.File.Exists(file)) return new SQLiteConnection($"Data Source={file};");
            var aseetFilePath = $"{HttpContext.Current.Server.MapPath("~")}PACK.db";
            File.Copy(aseetFilePath, file);
            //SQLiteConnection.CreateFile(file);
            return new SQLiteConnection($"Data Source={file};");
        }

        public static SQLiteConnection GetCommonDb()
        {
            var file = $"{HttpContext.Current.Server.MapPath("~")}{CommonFileName}";
            if (System.IO.File.Exists(file)) return new SQLiteConnection($"Data Source={file};");
            var aseetFilePath = $"{HttpContext.Current.Server.MapPath("~")}COMMON.db";
            File.Copy(aseetFilePath, file);
            //SQLiteConnection.CreateFile(file);
            return new SQLiteConnection($"Data Source={file};");
        }
    }
}