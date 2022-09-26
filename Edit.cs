using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    internal class Edit
    {
        public static void EditBooks(string tbl, string ISBN, string Title, string Description, string Price)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand editcmd = new SqliteCommand();
                editcmd.Connection = db;
                editcmd.CommandText = "UPDATE " + tbl + " SET Title=@Title, Description=@Description, Price=@Price WHERE ISBN=@ISBN";
                editcmd.Parameters.AddWithValue("@ISBN",ISBN);
                editcmd.Parameters.AddWithValue("@Title",Title);
                editcmd.Parameters.AddWithValue("@Description",Description);
                editcmd.Parameters.AddWithValue("@Price",Price);
                editcmd.ExecuteReader();
                db.Close();
                

            }
  
        }
   
    
        public static void EditCustomers(string tbl, string Customer_id, string Customer_name, string Address, string Email)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand editcmd = new SqliteCommand();
                editcmd.Connection = db;
                editcmd.CommandText = "UPDATE " + tbl + " SET Customer_name=@Customer_name, Address=@Address, Email=@Email Where Customer_id=@Customer_id";
                editcmd.Parameters.AddWithValue("@Customer_id",Customer_id);
                editcmd.Parameters.AddWithValue("@Customer_name",Customer_name);
                editcmd.Parameters.AddWithValue("@Address",Address);
                editcmd.Parameters.AddWithValue("@Email",Email);
                editcmd.ExecuteReader();
                db.Close();
            }


        }
    
    
    
    
    }
}
