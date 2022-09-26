using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Data.Sqlite;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        public Books()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Add.AddBooks(txtISBN.Text, txtTitle.Text, txtDescription.Text, txtPrice.Text);
            txtISBN.Clear();
            txtTitle.Clear();   
            txtDescription.Clear();
            txtPrice.Clear();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit.EditBooks("Bookstbl", txtISBN.Text, txtTitle.Text, txtDescription.Text, txtPrice.Text);
            txtISBN.Clear();
            txtTitle.Clear();
            txtDescription.Clear();
            txtPrice.Clear();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete.DeleteBooks("Bookstbl", txtISBN.Text);
            txtISBN.Clear();
            txtTitle.Clear();
            txtDescription.Clear();
            txtPrice.Clear();

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
           string ISBN = txtSearch.Text;
            List<Itemb> listdata = new List<Itemb>();
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            { 
                db.Open();
             
                SqliteCommand searchcmd = new SqliteCommand();
                searchcmd.Connection = db;
                searchcmd.CommandText = 
                ("SELECT * FROM Bookstbl Where ISBN=@ISBN");
                searchcmd.Parameters.AddWithValue("@ISBN", ISBN);
                SqliteDataReader query = searchcmd.ExecuteReader();

                while(query.Read())
                {
                    listdata.Add(new Itemb { ISBN = query.GetString(0), Title = query.GetString(1), 
                        Desciption = query.GetString(2), Price = query.GetInt32(3)});
                }

                try
                {
                    txtISBN.Text = listdata[0].ISBN;
                    txtTitle.Text = listdata[0].Title;
                    txtDescription.Text = listdata[0].Desciption;
                    txtPrice.Text = listdata[0].Price.ToString();
                }
                catch (Exception ex)
                {

                    ex.Message.ToString();
                    MessageBox.Show("EROR NO DATA");
                }
               

                db.Close();
             
            }
          
            dtgBooks.ItemsSource = listdata;
            txtSearch.Clear();
       
        }

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            MessageBoxResult rst = MessageBox.Show("Exits no saving Data", "you want Exits y or n", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (rst == MessageBoxResult.OK)
            {
                menu.Show(); this.Close();
            }
        }
 
        class Itemb
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Desciption { get; set; }
            public int Price { get; set;  }
 
        }
    }
}