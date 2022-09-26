using System;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop
{
    internal class DataAccess
    {
        //สร้างtblBook
        public static void Createtblbooks()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))

            {
                db.Open();

                string tblbookcmd = "CREATE TABLE IF NOT " + "EXISTS Bookstbl (ISBN NVARCHAR(2048) PRIMARY KEY, " +
                    "Title NVARCHAR(2048) NULL, Description NVARCHAR(2048) NULL, Price INTEGER NULL)";
                SqliteCommand Creattblbooks = new SqliteCommand(tblbookcmd, db);
                Creattblbooks.ExecuteReader();
            }
        }


        public static void Createtblcustomers()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {

                db.Open();

                string tblCustomers = "CREATE TABLE IF NOT " + "EXISTS Customerstbl (Customer_id NVARCHAR(2048) PRIMARY KEY, " +
                    "Customer_name NVARCHAR(2048) NULL, Address NVARCHAR(2048) NULL, Email NVARCHAR(2048) NULL)";
                SqliteCommand Createtblcustomers = new SqliteCommand(tblCustomers, db);
                Createtblcustomers.ExecuteReader();
            }



        }


        public static void Createtransactions()
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();

                string tblTransactions = "CREATE TABLE IF NOT " + "EXISTS Transactionstbl (ISBN NVARCHAR(2048) null, " +
                    "Customer_id NVARCHAR(2048) NULL, Quatity INTEGER NULL, Total_Price INTEGER NULL)";
                SqliteCommand Createtbltransactions = new SqliteCommand(tblTransactions, db);
                Createtbltransactions.ExecuteReader();

            }




        }






    }

}
