using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using SimpleMVVMExample.Ticket;

namespace SimpleMVVMExample
{
    public class TicketViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _ticketId;
        private ObservableCollection<TicketModel> _tickets = new ObservableCollection<TicketModel>();
        private TicketModel _selectedItem;
        private ICommand _populateTicketsCommand;
        private ICommand _saveTicketCommand;
        private ICommand _openDetailTicketCommand;
        private ICommand _closeTicketCommand;

        #endregion

        public TicketViewModel()
        {
            Tickets = new ObservableCollection<TicketModel>();
        }

        #region Properties/Commands

        public TicketModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (value == _selectedItem || value == null) return;
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
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
                    OnPropertyChanged("TicketId");
                }
            }
        }

        public ICommand PopulateTicketsCommand => _populateTicketsCommand ?? (_populateTicketsCommand = new RelayCommand(param => InitializeCurrentTickets()));

        public ICommand OpenDetailTicketCommand => _openDetailTicketCommand ?? (_openDetailTicketCommand = new RelayCommand(param => ShowWindow(), param => (SelectedItem != null)));

        public ICommand SaveTicketCommand => _saveTicketCommand ?? (_saveTicketCommand = new RelayCommand(param => SaveTicket()));

        public ICommand CloseTicketCommand => _closeTicketCommand ?? (_closeTicketCommand = new RelayCommand(param => DeleteTicket()));

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

        private void DeleteTicket()
        {
            MessageBox.Show("Successfully deleted Ticket.");
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

        private void ShowWindow()
        {
            // Just as an exammple, here I just show a MessageBox
            Debug.WriteLine(SelectedItem);
            var detailForm = new DetailTicketView(SelectedItem);
            detailForm.ShowDialog();
        }
        #endregion
    }
}
