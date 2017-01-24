using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SimpleMVVMExample.DB;

namespace SimpleMVVMExample
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            /*DC test = new DC();
            test.test();*/
            using (var ctx = new HelpdeskEntities())
            {
                Console.WriteLine("ConnectionState: " + ctx.Database.Connection.State);

                var rst = from tbl in ctx.TBLCUSTOMER
                          select tbl;

                Console.WriteLine("LINQ Result: ");
                foreach (var result in rst)
                {
                    Console.WriteLine("ID: " + result.INTCUSTOMERID + " Name: " + result.STRNAME);
                }
            }
        }
    }
}
