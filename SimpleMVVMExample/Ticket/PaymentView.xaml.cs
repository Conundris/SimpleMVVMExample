using System.Data;
using System.Windows;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;

namespace SimpleMVVMExample.Ticket
{
    /// <summary>
    /// Interaction logic for PaymentView.xaml
    /// </summary>
    public partial class PaymentView : Window
    {
        private int ticketID;

        public PaymentView(int ticketID)
        {
            InitializeComponent();
            this.ticketID = ticketID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtAmount.Text != "")
            {
                using (var cmd = DC.GetOpenConnection().CreateCommand())
                {
                    if (cmd.Connection.State != ConnectionState.Open) return;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "UPDATE TBLTICKET SET DECAMOUNT = " + txtAmount.Text + " WHERE INTTICKETID = " + ticketID;

                    cmd.ExecuteNonQuery();
                }
            }

            this.Close();
        }
    }
}
