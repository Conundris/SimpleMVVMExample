﻿using System.Collections.ObjectModel;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Utility;

namespace SimpleMVVMExample.Staff
{
    class StaffViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private int _staffId;
        private ObservableCollection<StaffModel> _staffList = new ObservableCollection<StaffModel>();
        private StaffModel _selectedItem;
        private ICommand _saveStaffCommand;
        private ICommand _openDetailStaffCommand;
        private ICommand _deactivateStaffCommand;
        

        #endregion

        public StaffViewModel()
        {
            StaffList = new ObservableCollection<StaffModel>();
            SearchStaffCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(GetStaff);
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
                if (value == null) return;
                _staffList = value;
                OnPropertyChanged("StaffList");
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

        public ICommand SearchStaffCommand { get; set; }

        public ICommand OpenDetailStaffCommand
        {
            get
            {
                return _openDetailStaffCommand ?? (_openDetailStaffCommand = new RelayCommand(
                           param => ShowWindow(),
                           param => (SelectedItem != null)
                       ));
            }
        }

        public ICommand SaveStaffCommand
        {
            get
            {
                return _saveStaffCommand ?? (_saveStaffCommand = new RelayCommand(
                           param => SaveStaff()
                       ));
            }
        }

        public ICommand DeactivateStaffCommand
        {
            get
            {
                return _deactivateStaffCommand ?? (_deactivateStaffCommand = new RelayCommand(
                           param => DeactivateStaff(),
                           param => (SelectedItem != null)
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

        public void GetStaff()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "FRMSTAFFVIEW";
                cmd.Parameters.Add(new OracleParameter("STRSEARCHSTRING", OracleDbType.Varchar2, ParameterDirection.Input));
                cmd.Parameters.Add(new OracleParameter("CURSOR_", OracleDbType.RefCursor, ParameterDirection.Output));

                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;

                var dataTable = new DataTable();
                dataTable.Load(dr);

                StaffList = new ObservableCollection<StaffModel>(dataTable.DataTableToList<StaffModel>());
            }
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
