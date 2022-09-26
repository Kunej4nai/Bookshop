using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace Bookshop
{
    internal class Delete
    {
        public static void DeleteBooks(string tbl, string ISBN)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand deletecmd = new SqliteCommand();
                deletecmd.Connection = db;
                deletecmd.CommandText = "DELETE FROM " + tbl + " WHERE ISBN=@ISBN";
                deletecmd.Parameters.AddWithValue("@ISBN",ISBN);
                deletecmd.ExecuteReader();
            }
        }
    
    
        public static void DeleteCustomers(string tbl, string Customer_id)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand deletecmd = new SqliteCommand();
                deletecmd.Connection = db;
                deletecmd.CommandText = "DELETE FROM " + tbl + " WHERE Customer_id";
                deletecmd.Parameters.AddWithValue("@Customer_id",Customer_id);
                deletecmd.ExecuteReader();
            }
    
        }
    
    }
}
