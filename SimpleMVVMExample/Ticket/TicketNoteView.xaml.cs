using System;
using System.Data;
using System.Windows;
using SimpleMVVMExample.DB;

namespace SimpleMVVMExample.Ticket
{
    /// <summary>
    /// Interaction logic for TicketNoteView.xaml
    /// </summary>
    public partial class TicketNoteView : Window
    {
        private int ticketID;

        public TicketNoteView(int ticketID)
        {
            InitializeComponent();
            this.ticketID = ticketID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text != "")
            {
                using (var cmd = DC.GetOpenConnection().CreateCommand())
                {
                    if (cmd.Connection.State != ConnectionState.Open) return;

                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "INSERT INTO TBLTICKETNOTE (INTTICKETID, STRNOTE) VALUES (" + ticketID + ", '" + DateTime.Now.ToString("yyyy MMMM dd") + ": " + txtDescription.Text + "')";

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ticket note has been succesfully created.");
                }
            }
            Close();
        }
    }
}
