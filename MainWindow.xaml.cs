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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataAccess.Createtblbooks();
            DataAccess.Createtblcustomers();
            DataAccess.Createtransactions();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "1")
            {
                if (txtEmail.Text == "1")
                {
                    if (txtPassword.Text == "1")
                    {
                        MessageBox.Show("Already login !");
                        MessageBox.Show("Welcome to the Menupage");
                        Menu menupage = new Menu();
                        menupage.Show();
                    }

                    else
                    {
                        MessageBox.Show("Your Password is not correct Please try again");
                    }

                }

              else 
              {  
                  MessageBox.Show("Your Email is not correct Please try again"); 
              }
            }
               
            else
            {
                MessageBox.Show("Your Username is not correct Please try again");
            }
                   
        
        }

    }
}
