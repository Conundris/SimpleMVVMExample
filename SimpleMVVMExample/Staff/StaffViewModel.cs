using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SimpleMVVMExample.Staff
{
    class StaffViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _ticketId;
        private ObservableCollection<StaffModel> _staffList = new ObservableCollection<StaffModel>();
        private StaffModel _selectedItem;
        private ICommand _populateStaffCommand;
        private ICommand _saveStaffCommand;
        private ICommand _openDetailStaffCommand;
        private ICommand _deleteStaffCommand;

        #endregion

        public StaffViewModel()
        {
            StaffList = new ObservableCollection<StaffModel>();
        }

        #region Properties/Commands

        public StaffModel SelectedItem
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

        public ObservableCollection<StaffModel> StaffList
        {
            get { return _staffList ?? (_staffList = new ObservableCollection<StaffModel>()); }
            set
            {
                if (value != null)
                {
                    _staffList = value;
                    OnPropertyChanged("StaffList");
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

        public ICommand PopulateStaffCommand
        {
            get
            {
                return _populateStaffCommand ?? (_populateStaffCommand = new RelayCommand(
                           param => InitializeCurrentTickets()
                       ));
            }
        }

        public ICommand OpenDetailStaffCommand
        {
            get
            {
                if (_openDetailStaffCommand == null)
                {
                    _openDetailStaffCommand = new RelayCommand(
                        param => ShowWindow(),
                        param => (SelectedItem != null)
                        );
                }
                return _openDetailStaffCommand;
            }
        }

        public ICommand SaveStaffCommand
        {
            get
            {
                if (_saveStaffCommand == null)
                {
                    _saveStaffCommand = new RelayCommand(
                        param => SaveStaff()
                    );
                }
                return _saveStaffCommand;
            }
        }

        public ICommand DeleteStaffCommand
        {
            get
            {
                if (_deleteStaffCommand == null)
                {
                    _deleteStaffCommand = new RelayCommand(
                        param => DeleteTicket()
                    );
                }
                return _deleteStaffCommand;
            }
        }

        #endregion

        #region Methods

        private void GetStaff()
        {
            // Usually you'd get your Product from the database,
            // but for now we'll just return a new object
            var p = new TicketModel
            {
                TicketId = TicketId,
                TicketTitle = "Test Product",
                TicketDescription = 10
            };
        }

        private void SaveStaff()
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
                StaffList.Add(InitializeStaff(i));
            }
        }

        private StaffModel InitializeStaff(int i)
        {
            var theObject = new StaffModel
            {
                
            };
            return theObject;
        }

        private void ShowWindow()
        {
            // Just as an exammple, here I just show a MessageBox
            Debug.WriteLine(SelectedItem);
            //var detailForm = new DetailTicketView(SelectedItem);
            //detailForm.ShowDialog();
        }
        #endregion
    }
}
