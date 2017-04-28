using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Security.Cryptography.Pkcs;
using System.Windows;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Helper_Classes;
using SimpleMVVMExample.Staff;

namespace SimpleMVVMExample.Ticket
{
    /// <summary>
    /// Interaction logic for DetailTicketView.xaml
    /// </summary>
    public partial class DetailTicketView : Window, ICloseable
    {
        private TicketModel _ticketModel;
        public ObservableCollection<TicketNoteModel> TicketNotes;
        public ObservableCollection<CustomerModel> Customers;
        public ObservableCollection<StaffModel> Staff;

        public DetailTicketView()
        {
            TicketNotes = new ObservableCollection<TicketNoteModel>();
            Customers = new ObservableCollection<CustomerModel>();
            Staff = new ObservableCollection<StaffModel>();
            InitializeComponent();
            fillStatus();
        }

        private void fillStatus()
        {
            cmbStatus.Items.Add("Created");
            cmbStatus.Items.Add("In Progress");
            cmbStatus.Items.Add("Not Assigned");
        }


        private void TicketNote_Click(object sender, RoutedEventArgs e)
        {
            new TicketNoteView(Convert.ToInt32(txtTicketID.Text)).ShowDialog();

            RefreshNotes();
        }

        private void RefreshNotes()
        {
            TicketNotes.Clear();

            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM TBLTICKETNOTE WHERE INTTICKETID = " + txtTicketID.Text;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TicketNotes.Add(new TicketNoteModel {INTTICKETNOTEID = reader.GetInt32(0), INTTICKETID = reader.GetInt32(1), STRNOTE = reader.GetString(2)});
                }
            }

            lbTicketNotes.ItemsSource = TicketNotes;
            lbTicketNotes.DisplayMemberPath = "STRNOTE";
        }

        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            new PaymentView(Convert.ToInt32(txtTicketID.Text)).ShowDialog();

            RefreshPayments();
        }

        private void RefreshPayments()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT DECAMOUNT FROM TBLTICKET WHERE INTTICKETID = " + txtTicketID.Text;

                DbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    txtPaymentReceived.Text = reader.GetDecimal(0).ToString();
                }
            }
        }

        private void btnPrintTicket_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Printing Ticket.");
        }

        private void DetailTicketWindow_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshNotes();
            RefreshPayments();
        }
    }
}
