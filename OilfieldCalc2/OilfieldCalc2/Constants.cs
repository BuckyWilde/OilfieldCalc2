using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OilfieldCalc2
{
    public static class Constants
    {
        public const string DatabaseFilename = "ofc.db3";

        public const SQLite.SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Xamarin.Essentials.FileSystem.AppDataDirectory;
                return Path.Combine(basePath, DatabaseFilename);
            }
        }
    }
}
