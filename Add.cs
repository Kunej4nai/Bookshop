using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    internal class Add
    {
        public static void AddBooks (string ISBN, string Title, string Description, string Price)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();

                SqliteCommand addcmd = new SqliteCommand();
                addcmd.Connection = db;
                addcmd.CommandText = "INSERT INTO Bookstbl VALUES(@ISBN, @Title, @Description, @Price);";
                addcmd.Parameters.AddWithValue("@ISBN",ISBN);
                addcmd.Parameters.AddWithValue("@Title", Title);
                addcmd.Parameters.AddWithValue("@Description", Description);
                addcmd.Parameters.AddWithValue("@Price", Price);
                addcmd.ExecuteReader();
                db.Close();

            }

        }
 
  
        public static void AddCustomers(string Customer_id, string Customer_name, string Address, string Email)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();

                SqliteCommand addcmd = new SqliteCommand();
                addcmd.Connection = db;
                addcmd.CommandText = "INSERT INTO Customerstbl VALUES(@Customer_id, @Customer_name, @Address, @Email);";
                addcmd.Parameters.AddWithValue("@Customer_id",Customer_id);
                addcmd.Parameters.AddWithValue("@Customer_name",Customer_name);
                addcmd.Parameters.AddWithValue("@Address",Address);
                addcmd.Parameters.AddWithValue("@Email",Email);
                addcmd.ExecuteReader();
                db.Close();

            }

        }
    

    
    
    
    
    
    
    
    
    
    
    }
}
