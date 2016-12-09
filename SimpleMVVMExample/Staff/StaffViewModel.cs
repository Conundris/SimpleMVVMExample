using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SimpleMVVMExample.Staff
{
    class StaffViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _staffId;
        private ObservableCollection<StaffModel> _staffList = new ObservableCollection<StaffModel>();
        private StaffModel _selectedItem;
        private ICommand _populateStaffCommand;
        private ICommand _saveStaffCommand;
        private ICommand _openDetailStaffCommand;
        private ICommand _deactivateStaffCommand;

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

        public string Name => "Staff";

        public int StaffId
        {
            get { return _staffId; }
            set
            {
                if (value == _staffId) return;
                _staffId = value;
                OnPropertyChanged("StaffId");
            }
        }

        public ICommand PopulateStaffCommand
        {
            get
            {
                return _populateStaffCommand ?? (_populateStaffCommand = new RelayCommand(
                           param => InitializeCurrentStaff()
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

        public ICommand DeactivateStaffCommand
        {
            get
            {
                return _deactivateStaffCommand ?? (_deactivateStaffCommand = new RelayCommand(
                           param => DeactivateStaff()
                       ));
            }
        }

        #endregion

        #region Methods

        private void SaveStaff()
        {
            MessageBox.Show("Successfully Saved Staff.");
        }

        private void DeactivateStaff()
        {
            MessageBox.Show("Successfully deactivated Staff.");
        }

        private void InitializeCurrentStaff()
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
                StaffID = StaffId,
                Forename = "Test" + i ,
                Surname = "Person" + i,
                Email = "test@test.ch",
                Username = "TestUser" + i,
                Password = "1234" + i,
                Active = true
            };
            return theObject;
        }

        private void ShowWindow()
        {
            // Just as an exammple, here I just show a MessageBox
            Debug.WriteLine(SelectedItem);
            var detailForm = new DetailStaffView(SelectedItem);
            detailForm.ShowDialog();
        }
        #endregion
    }
}
