using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace LawOfficeApp
{
    public record Client(int Id, string Name, string Phone, string Email, string Address, string Note, string AvatarPath, string CreatedAt);

    public static class DataAccess
    {
        public static List<Client> GetClients(string dbPath)
        {
            var list = new List<Client>();
            using var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, Phone, Email, Address, Note, AvatarPath, CreatedAt FROM Clients ORDER BY Id DESC;";
            using var rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                list.Add(new Client(
                    rdr.GetInt32(0),
                    rdr.IsDBNull(1) ? "" : rdr.GetString(1),
                    rdr.IsDBNull(2) ? "" : rdr.GetString(2),
                    rdr.IsDBNull(3) ? "" : rdr.GetString(3),
                    rdr.IsDBNull(4) ? "" : rdr.GetString(4),
                    rdr.IsDBNull(5) ? "" : rdr.GetString(5),
                    rdr.IsDBNull(6) ? "" : rdr.GetString(6),
                    rdr.IsDBNull(7) ? "" : rdr.GetString(7)
                ));
            }
            conn.Close();
            return list;
        }

        public static int AddClient(string dbPath, string name, string phone, string email, string address, string note, string avatarPath)
        {
            using var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Clients (Name, Phone, Email, Address, Note, AvatarPath, CreatedAt) 
                                VALUES ($n,$p,$e,$a,$no,$av,$c); SELECT last_insert_rowid();";
            cmd.Parameters.AddWithValue("$n", name);
            cmd.Parameters.AddWithValue("$p", phone);
            cmd.Parameters.AddWithValue("$e", email);
            cmd.Parameters.AddWithValue("$a", address);
            cmd.Parameters.AddWithValue("$no", note);
            cmd.Parameters.AddWithValue("$av", avatarPath);
            cmd.Parameters.AddWithValue("$c", DateTime.UtcNow.ToString("o"));
            var id = (long)cmd.ExecuteScalar();
            conn.Close();
            return (int)id;
        }

        public static void DeleteClient(string dbPath, int id)
        {
            using var conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM Clients WHERE Id = $id;";
            cmd.Parameters.AddWithValue("$id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
