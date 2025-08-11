using Microsoft.Data.Sqlite;
using System;

namespace LawOfficeApp
{
    public static class Database
    {
        public static void Initialize(string dbPath)
        {
            var needCreate = !System.IO.File.Exists(dbPath);
            using var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            if (needCreate)
            {
                using var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                CREATE TABLE IF NOT EXISTS Clients (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT,
                    Phone TEXT,
                    Email TEXT,
                    Address TEXT,
                    Note TEXT,
                    AvatarPath TEXT,
                    CreatedAt TEXT
                );
                CREATE TABLE IF NOT EXISTS Cases (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Code TEXT,
                    Title TEXT,
                    ClientId INTEGER,
                    Court TEXT,
                    Status TEXT,
                    NextDate TEXT,
                    Note TEXT,
                    CreatedAt TEXT
                );
                CREATE TABLE IF NOT EXISTS Checks (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Code TEXT,
                    OwnerName TEXT,
                    Amount INTEGER,
                    DueDate TEXT,
                    RelatedCaseId INTEGER,
                    Note TEXT,
                    CreatedAt TEXT
                );
                CREATE TABLE IF NOT EXISTS Reminders (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT,
                    WhenDate TEXT,
                    Note TEXT,
                    RelatedCaseId INTEGER,
                    CreatedAt TEXT
                );
                CREATE TABLE IF NOT EXISTS Meta (Key TEXT PRIMARY KEY, Value TEXT);
                ";
                cmd.ExecuteNonQuery();
                // insert meta version
                using var cmd2 = conn.CreateCommand();
                cmd2.CommandText = "INSERT OR REPLACE INTO Meta (Key, Value) VALUES ('db_version', '1');";
                cmd2.ExecuteNonQuery();
            }
            conn.Close();
        }
    }
}
