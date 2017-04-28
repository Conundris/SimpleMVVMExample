using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Utility;
using SimpleMVVMExample.WindowFactory;

namespace SimpleMVVMExample
{
    public class TicketViewModel : ObservableObject, IPageViewModel
    {
        #region Fields


        private ObservableCollection<TicketModel> _tickets = new ObservableCollection<TicketModel>();

        private int _ticketId;
        private readonly IWindowFactory _windowFactory;

        private TicketModel _selectedTicket;
        private ICommand _populateTicketsCommand;
        private ICommand _saveTicketCommand;
        private ICommand _openDetailTicketCommand;
        private ICommand _closeTicketCommand;

        #endregion

        public TicketViewModel() : this(new DetailTicketViewProductionFactory()) { }

        public TicketViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
            Tickets = new ObservableCollection<TicketModel>();
            CreateTicketCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(CreateTicket);
            Get100Tickets();
        }

        #region Properties/Commands

        public TicketModel SelectedTicket
        {
            get
            {
                return _selectedTicket;
            }
            set
            {
                if (value == _selectedTicket || value == null) return;
                _selectedTicket = value;
                OnPropertyChanged("SelectedTicket");
            }
        }

        public ObservableCollection<TicketModel> Tickets
        {
            get { return _tickets ?? (_tickets = new ObservableCollection<TicketModel>()); }
            set
            {
                if (value != null)
                {
                    _tickets = value;
                    OnPropertyChanged("Tickets");
                }
            }
        }

        public string Name => "Tickets";

        public int TicketId
        {
            get { return _ticketId; }
            set
            {
                if (value != _ticketId)
                {
                    _ticketId = value;
                    OnPropertyChanged("INTTICKETID");
                }
            }
        }

        public ICommand CreateTicketCommand { get; set; }

        public ICommand OpenDetailTicketCommand => _openDetailTicketCommand ?? (_openDetailTicketCommand = new RelayCommand(param => ShowWindow(), param => (SelectedTicket != null)));

        public ICommand SaveTicketCommand => _saveTicketCommand ?? (_saveTicketCommand = new RelayCommand(param => SaveTicket()));

        public ICommand CloseTicketCommand => _closeTicketCommand ?? (_closeTicketCommand = new RelayCommand(param => DeleteTicket()));

        #endregion

        #region Methods

        private void SaveTicket()
        {
            // You would implement your Product save here
        }

        private void DeleteTicket()
        {
            MessageBox.Show("Successfully deleted Ticket.");
        }

        private async void CreateTicket()
        {
            // Create and wait for Window to close.
            var complete = await _windowFactory.CreateNewWindow(new TicketModel());
            // Refresh Datagrid with new Data
            GetTickets();
        }

        private void GetTickets()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SPTICKETVIEW";
                cmd.Parameters.Add(new OracleParameter("STRSEARCHSTRING", OracleDbType.Varchar2, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("CURSOR_", OracleDbType.RefCursor, ParameterDirection.Output));

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;

                var dataTable = new DataTable();
                dataTable.Load(dr);

                Tickets = new ObservableCollection<TicketModel>(dataTable.DataTableToList<TicketModel>());
            }
        }

        private void Get100Tickets()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT 
                                        TBLTICKET.*, 
                                        TBLCUSTOMER.STRFORENAME || ' ' || TBLCUSTOMER.STRSURNAME AS STRREQUESTBY, 
                                        TBLSTAFF.STRFORENAME || ' ' || TBLSTAFF.STRSURNAME AS STRASSIGNEDTO 
                                    FROM 
                                        TBLTICKET         
                                        INNER JOIN TBLCUSTOMER
                                           ON TBLCUSTOMER.INTCUSTOMERID = TBLTICKET.INTREQUESTBY
                                        LEFT JOIN TBLSTAFF
                                           ON TBLSTAFF.INTSTAFFID = TBLTICKET.INTASSIGNEDTO 
                                    WHERE 
                                        ROWNUM <= 100 
                                    ORDER BY 
                                        TBLTICKET.INTTICKETID DESC";
                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;
                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);

                Tickets = new ObservableCollection<TicketModel>(dataTable.DataTableToList<TicketModel>());
            }
        }

        private async void ShowWindow()
        {
            // Create and wait for Window to close.
            var complete = await _windowFactory.CreateNewWindow(_selectedTicket);
            // Refresh Datagrid with new Data
            GetTickets();
        }
        #endregion
    }
}
