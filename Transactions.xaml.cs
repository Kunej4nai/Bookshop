using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Bookshop
{
    /// <summary>
    /// Interaction logic for Transactions.xaml
    /// </summary>
    public partial class Transactions : Window
    {
        Menu MenuItem = new Menu();
        List<ItemInsertToTrans> listdataToTrans = new List<ItemInsertToTrans>();
        List<Itemt> listdata1 = new List<Itemt>();
        int price = 0; int totalprice = 0; int qty = 0; int Totalamount = 0;
        int checkcustomer = 0; int cal = 0;
        int orderstat = 0;
        string idbill = "", booktransc = "";
        public Transactions()
        {
            InitializeComponent();
            txtCheck_customerid.Focus();
            DataAccess.Createtblbooks();
            DataAccess.Createtblcustomers();
            DataAccess.Createtransactions();
            CheckOnOffControl();
        }

        private void btnCheck_customerid_Click(object sender, RoutedEventArgs e)
        {
            btnRemoveall_Click(sender, e); 
            string Customer_id = txtCheck_customerid.Text;
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand searchcmd = new SqliteCommand();
                searchcmd.Connection = db;
                searchcmd.CommandText =
                    ("SELECT * FROM Customerstbl WHERE Customer_id=@Customer_id");
                searchcmd.Parameters.AddWithValue("@Customer_id", Customer_id);
                SqliteDataReader query = searchcmd.ExecuteReader();

                try
                {
                    while (query.Read())
                    {
                        //listdata.Add(new Itemt { Customer_id = query.GetString(0), Customer_name = query.GetString(1)});
                        //txtCustomer_id.Text = listdata[0].Customer_id;
                        //txtCustomer_name.Text = listdata[0].Customer_name;
                        txtCustomer_id.Text = query.GetString(0);
                        txtCustomer_name.Text = query.GetString(1);
                    }

                    checkcustomer = 1;
                    CheckOnOffControl();
                    txtSearch_book.Focus();

                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    MessageBox.Show("NO DATA");
                }
                db.Close();
            }

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string ISBN = txtSearch_book.Text;
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();

                SqliteCommand searchcmd = new SqliteCommand();
                searchcmd.Connection = db;
                searchcmd.CommandText =
                ("SELECT * FROM Bookstbl WHERE ISBN=@ISBN");
                searchcmd.Parameters.AddWithValue("@ISBN", ISBN);
                SqliteDataReader query = searchcmd.ExecuteReader();

                try
                {
                    while (query.Read())
                    {
                        txtISBN.Text = query.GetString(0);
                        txtTitle.Text = query.GetString(1);
                        txtPrice.Text = query.GetString(3);
                    }
                    txtInsert_quatity.Focus();
                    cal = cal + (int.Parse(txtPrice.Text));
                    txtTotal_Price.Text = cal.ToString();
                    txtSearch_book.Clear();
                }
                catch (Exception ex)
                {

                    ex.Message.ToString();
                    MessageBox.Show("NO DATA");
                }
                db.Close();
            }



        }
     
        private void btnCalculater_Click(object sender, RoutedEventArgs e)
        {
            if (txtISBN.Text != "" && txtTitle.Text != "" && txtInsert_quatity.Text != "" && txtPrice.Text != "" && txtTotal_Price.Text != "")
            {
                qty = int.Parse(txtInsert_quatity.Text);
                totalprice = int.Parse(txtInsert_quatity.Text) * int.Parse(txtPrice.Text);
                txtTotal_Price.Text = totalprice.ToString();
                txtQuatity.Text = qty.ToString();
                txtInsert_quatity.Clear();

            }
            else
            {
                MessageBox.Show("Please Enter Quatity");
            }

        }

        private void btnAddListOrder_Click(object sender, RoutedEventArgs e)
        {
            if (txtISBN.Text != "" && txtTitle.Text != "" && txtQuatity.Text != "" && txtPrice.Text != "" && txtTotal_Price.Text != "")

            {
                //assign ListTextBox OBJ Name lbxShow
                lbxShow.Items.Add
                    (
                    "IDbook:" + txtISBN.Text + "    IDcustomer:" + txtCustomer_id.Text + "    Title:" + txtTitle.Text +
                    "    Price:" + txtPrice.Text + "    QTY:" + txtQuatity.Text
                    + "    TotalPrice:" + txtTotal_Price.Text
                    );
                //assign List<Itemt> OBJ Name list1data1
                listdata1.Add(new Itemt
                {
                    ISBN = txtISBN.Text,
                    Title = txtTitle.Text,
                    Quatity = int.Parse(txtQuatity.Text)
                ,
                    Price = int.Parse(txtPrice.Text),
                    Total_price = int.Parse(txtTotal_Price.Text)
                ,
                    Customer_id = txtCustomer_id.Text
                });
                //Recoard string to string_booktransc for billShow
                booktransc = booktransc+" ISBN :" + txtISBN.Text + "\n Title :" + txtTitle.Text + "\n Price =" + txtPrice.Text +
                    "\n Qty =" + txtQuatity.Text+ "\n TotalPrice =" + txtTotal_Price.Text + "\n______________________________\n";

                int totalall = 0;
                int cn = listdata1.Count;
                dynamic dy = listdata1;
                for (int i = 0; i < cn; i++)
                {
                    totalall = totalall + (listdata1[i].Total_price);
                }
                txtTotal_amount.Text = totalall.ToString();
                txtTotal_amount.Background = Brushes.Violet;
                txtTotal_amount.Foreground = Brushes.White;
                Totalamount = totalall;
                

                txtISBN.Clear();
                txtTitle.Clear();
                txtQuatity.Clear();
                txtPrice.Clear();
                txtTotal_Price.Clear();

                CheckOnOffControl();
                MessageBox.Show("Already add to the craft !");
                txtSearch_book.Focus();
            }
            else if (txtISBN.Text == "" || txtTitle.Text == "" || txtQuatity.Text == "" || txtPrice.Text == "" || txtTotal_Price.Text == "")
            {
                MessageBox.Show("No data!, Please insert the data");
            }
        }

        private void btnBuy_Click(object sender, RoutedEventArgs e)
        {
              MessageBoxResult result =
                MessageBox.Show("Please check orders before. are you sure to add orders to the bill?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                dtgTransactions.ItemsSource = listdata1;

                int cn = dtgTransactions.Items.Count;//for if
                dynamic dy = dtgTransactions.Items;//for if
                if (result == MessageBoxResult.Yes)
                {
                    for (int i = 0; i < cn; i++)
                    {
                        AddTransaction(dy[i].ISBN, dy[i].Customer_id, dy[i].Quatity.ToString(), dy[i].Total_price.ToString());
                    }
                    int cn2 = listdata1.Count;
                    if (cn2 > 0)
                    {
                        listdataToTrans.Add(new ItemInsertToTrans
                        { IdBill = DateTime.Now.ToString(), BookTransc = booktransc + "Total Amount = " + Totalamount + " Baht" + "\n______________________________\n" });
                        dtgTransactions.ItemsSource = listdataToTrans;
                    }
                    MessageBox.Show("Buy compleat!");
                    txtCheck_customerid.Clear();
                    txtCustomer_id.Clear();
                    txtCustomer_name.Clear();
                    txtCheck_customerid.Clear();
                    txtTotal_amount.Clear();
                    txtCheck_customerid.IsEnabled = true;
                    btnCheck_customerid.IsEnabled = true;

                    listdata1.RemoveAll(listdata1.Remove);
                    lbxShow.Items.Clear();
                    lbxShow.Items.Refresh();

                    txtTotal_amount.Background = Brushes.White;
                    txtTotal_amount.Foreground = Brushes.Black;
                    checkcustomer = 0;

                    CheckOnOffControl();
                }

                else if (result == MessageBoxResult.No)
                { MessageBox.Show("Your order is cancel");
                    btnRemoveall_Click(sender, e);
                    listdataToTrans.RemoveAll(listdataToTrans.Remove);
                    booktransc = "";
                    idbill = "";

                    txtTotal_amount.Clear();
                    Totalamount = 0;

                }
            
        }

        

        private void CheckOnOffControl()
        {
            if (checkcustomer == 0)
            {
                txtSearch_book.IsEnabled = false;
                txtInsert_quatity.IsEnabled = false;
                btnSearch_book.IsEnabled = false;
                btnCalculater.IsEnabled = false;
                btnAddListOrder.IsEnabled = false;
                btnBuy.IsEnabled = false;
                
            }
            if (checkcustomer == 1)
            {
                txtCheck_customerid.IsEnabled = false;
                btnCheck_customerid.IsEnabled = false;

                txtSearch_book.IsEnabled = true;
                txtInsert_quatity.IsEnabled = true;
                txtInsert_quatity.IsEnabled = true;
                btnSearch_book.IsEnabled = true;
                btnCalculater.IsEnabled = true;
                btnAddListOrder.IsEnabled = true;

                if (lbxShow.Items.Count < 0)
                {
                    btnBuy.IsEnabled = false;
                }
                if (lbxShow.Items.Count > 0)
                {
                    btnBuy.IsEnabled = true;
                }
            }
        }

        private void AddTransaction(string ISBN, string Customer_id, string Quatity, string Total_Price)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand insertcmd = new SqliteCommand();
                insertcmd.Connection = db;
                insertcmd.CommandText = "INSERT INTO Transactionstbl VALUES(@ISBN, @Customer_id, @Quatity, @Total_Price);";
                insertcmd.Parameters.AddWithValue("@ISBN", ISBN);
                insertcmd.Parameters.AddWithValue("@Customer_id", Customer_id);
                insertcmd.Parameters.AddWithValue("@Quatity", Quatity);
                insertcmd.Parameters.AddWithValue("@Total_Price", Total_Price);
                insertcmd.ExecuteReader();
                db.Close();
            }
        }

        private void btnSearch_Transection_Click(object sender, RoutedEventArgs e)
        {
            string cmd = "SELECT * FROM Transactionstbl";
            List<Itemt> listdata = new List<Itemt>();
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand searchcmd = new SqliteCommand(cmd, db);
                SqliteDataReader query = searchcmd.ExecuteReader();
                try
                {
                    while (query.Read())
                    {
                        listdata.Add(new Itemt
                        {
                            ISBN = query.GetString(0),
                            Customer_id = query.GetString(1),
                            Quatity = query.GetInt32(2),
                            Total_price = query.GetInt32(3)
                        });
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    MessageBox.Show("EROR");
                }
                db.Close();
            }
            dtgTransactions.ItemsSource = listdata;
        }

        public static void DeleteTran(string ISBN)
        {
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();
                SqliteCommand deletecmd = new SqliteCommand();
                deletecmd.Connection = db;
                deletecmd.CommandText = "DELETE FROM Transactionstbl WHERE ISBN=@ISBN";
                deletecmd.Parameters.AddWithValue("@ISBN", ISBN);
                deletecmd.ExecuteReader();
                db.Close();
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DeleteTran(txtISBNfrombill.Text);
            txtISBNfrombill.Clear();
        }

        private void dtgTransactions_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            dynamic dy = dtgTransactions.SelectedItem;
            MessageBox.Show(dy.ISBN);
        }

        private void btnRemoveall_Click(object sender, RoutedEventArgs e)
        {
            listdata1.RemoveAll(listdata1.Remove);
            dtgTransactions.ItemsSource = listdata1;

            dtgTransactions.Items.Refresh();
            txtTotal_amount.Clear();
            lbxShow.Items.Clear();
            lbxShow.Items.Refresh();
         
        }
      

        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu();
            MessageBoxResult rst = MessageBox.Show("Exits no saving Data","you want Exits y or n", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (rst==MessageBoxResult.OK)
            {
                menu.Show(); this.Close();
            }
            
        }
        class ItemInsertToTrans
        {
            public string IdBill { get; set; }
            public string BookTransc { get; set; }
        }

        private void btnSearch_Click_1(object sender, RoutedEventArgs e)
        {
            string ISBN = txtSearchBill.Text;
            List<Itemt> listdata = new List<Itemt>();
            using (SqliteConnection db = new SqliteConnection($"Filename=Table.db"))
            {
                db.Open();

                SqliteCommand searchcmd = new SqliteCommand();
                searchcmd.Connection = db;
                searchcmd.CommandText =
                ("SELECT * FROM Transactionstbl Where ISBN=@ISBN");
                searchcmd.Parameters.AddWithValue("@ISBN", ISBN);
                SqliteDataReader query = searchcmd.ExecuteReader();

                try
                {
                    while (query.Read())
                    {
                        listdata.Add(new Itemt
                        {
                            ISBN = query.GetString(0),
                            Customer_id = query.GetString(1),
                            Quatity = query.GetInt32(2),
                            Total_price = query.GetInt32(3)
                        });
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    MessageBox.Show("EROR");
                }
                db.Close(); 
            }
            dtgTransactions.ItemsSource = listdata;
       



           
        }

        class Itemt
        {
            public string ISBN { get; set; }
            public string Title { get; set; }
            public string Customer_id { get; set; }
            public int Price { get; set; }
            public int Quatity { get; set; }
            public int Total_price { get; set; }

        }

    }
}