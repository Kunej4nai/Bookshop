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

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        static int check;
        
        public Menu()
        {
            InitializeComponent();
        }

        private void btnBooks_Click(object sender, RoutedEventArgs e)
        {
            Books bookspage = new Books(); 
            this.Close();
            bookspage.Show();
            
            
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e)
        {
            Customers customerspage = new Customers(); 
            this.Close();
            customerspage.Show(); 
        }

        private void btnTransactions_Click(object sender, RoutedEventArgs e)
        {
            Transactions transactions = new Transactions(); 
            this.Close();
            transactions.Show();
        }

    }
}