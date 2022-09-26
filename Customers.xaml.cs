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
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Customers : Window
    {
        public Customers()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Add.AddCustomers(txtCustomer_id.Text, txtCustomer_name.Text, txtAddress.Text, txtEmail.Text);
            txtCustomer_id.Clear();
            txtCustomer_name.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit.EditCustomers("Customerstbl", txtCustomer_id.Text, txtCustomer_name.Text, txtAddress.Text, txtEmail.Text);
            txtCustomer_id.Clear();
            txtCustomer_name.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete.DeleteCustomers("Customerstbl", txtCustomer_id.Text);
            txtCustomer_id.Clear();
            txtCustomer_name.Clear();
            txtAddress.Clear();
            txtEmail.Clear();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string Customer_id = txtSearch.Text;
            List<Itemc> listdata = new List<Itemc>();
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand searchcmd = new SqliteCommand();
                searchcmd.Connection = db;
                searchcmd.CommandText =
                    ("SELECT * FROM Customerstbl WHERE Customer_id=@Customer_id");
                searchcmd.Parameters.AddWithValue("@Customer_id",Customer_id);
                SqliteDataReader query = searchcmd.ExecuteReader();

                while (query.Read())
                {
                    listdata.Add(new Itemc { Customer_id = query.GetString(0), Customer_name = query.GetString(1), Address = query.GetString(2), Email = query.GetString(3) });
                }

                try
                {
                    txtCustomer_id.Text = listdata[0].Customer_id;
                    txtCustomer_name.Text = listdata[0].Customer_name;
                    txtAddress.Text = listdata[0].Address;
                    txtEmail.Text = listdata[0].Email;

                }
                catch (Exception ex)
                {

                    ex.Message.ToString();
                    MessageBox.Show("EROR NO DATA");
                }
                
                db.Close();

            }

            dtgCustomers.ItemsSource = listdata;
            txtSearch.Clear();
            
        }
    
   

       
        
        class Itemc
        {
            public string Customer_id { get; set; }
            public string Customer_name { get; set; }
            public string Address { get; set; }
            public string Email { get; set; }
      
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
    }
}
