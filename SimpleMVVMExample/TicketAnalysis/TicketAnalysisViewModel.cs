using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Input;
using SimpleMVVMExample.DB;
using SimpleMVVMExample.Staff;
using SimpleMVVMExample.Utility;

namespace SimpleMVVMExample.TicketAnalysis
{
    public class TicketAnalysisViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private ICommand _runTicketAnalysisCommand;
        private DateTime _datFrom;
        private DateTime _datTo;

        #endregion

        public string Name => "TicketAnalysis";


        private void Get100Staff()
        {
            using (var cmd = DC.GetOpenConnection().CreateCommand())
            {
                if (cmd.Connection.State != ConnectionState.Open) return;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM TBLSTAFF WHERE ROWNUM <= 100 ORDER BY INTSTAFFID DESC";
                var dr = cmd.ExecuteReader();

                if (!dr.HasRows) return;
                var dataReader = cmd.ExecuteReader();
                var dataTable = new DataTable();
                dataTable.Load(dataReader);
                // = new ObservableCollection<StaffModel>(dataTable.DataTableToList<StaffModel>());
            }
        }
    }
}
