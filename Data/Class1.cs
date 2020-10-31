using System;
using System.IO;
using SQLite;

namespace Data
{
    public class Database
    {
        private const string databaseFile = "wksc_db.db3";
        private const SQLiteOpenFlags openFlags = SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.ProtectionComplete;


        private readonly string dbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), databaseFile);


        private readonly SQLiteAsyncConnection database;



        public Database()
        {
            database = new SQLiteAsyncConnection(dbFilePath, openFlags);
            

        }
    }
}
