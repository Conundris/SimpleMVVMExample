using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace SimpleMVVMExample
{
    public class TicketViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _ticketId;
        private ObservableCollection<TicketModel> _tickets = new ObservableCollection<TicketModel>();
        private ICommand _populateTicketsCommand;
        private ICommand _saveTicketCommand;

        #endregion

        public TicketViewModel()
        {
            Tickets = new ObservableCollection<TicketModel>();
        }

        #region Properties/Commands

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

        public string Name => "Products";

        public int TicketId
        {
            get { return _ticketId; }
            set
            {
                if (value != _ticketId)
                {
                    _ticketId = value;
                    OnPropertyChanged("TicketId");
                }
            }
        }

        public ICommand PopulateTicketsCommand
        {
            get
            {
                return _populateTicketsCommand ?? (_populateTicketsCommand = new RelayCommand(
                           param => InitializeCurrentTickets()
                       ));
            }
        }

        public ICommand SaveTicketCommand
        {
            get
            {
                if (_saveTicketCommand == null)
                {
                    _saveTicketCommand = new RelayCommand(
                        param => SaveTicket()
                    );
                }
                return _saveTicketCommand;
            }
        }

        #endregion

        #region Methods

        private void GetTicket()
        {
            // Usually you'd get your Product from your datastore,
            // but for now we'll just return a new object
            var p = new TicketModel
            {
                TicketId = TicketId,
                TicketTitle = "Test Product",
                TicketDescription = 10
            };
        }

        private void SaveTicket()
        {
            // You would implement your Product save here
        }

        private void InitializeCurrentTickets()
        {
            for (var i = 0; i < 5; i++)
            {
                Tickets.Add(InitializeTicket(i));
            }
        }

        private TicketModel InitializeTicket(int i)
        {
            var theObject = new TicketModel
            {
                TicketId = i,
                TicketTitle = "The object " + i
            };
            return theObject;
        }

        private void ShowWindow(int i)
        {
            // Just as an exammple, here I just show a MessageBox
            MessageBox.Show("You clicked on object " + i + "!!!");
        }
        #endregion
    }
}
