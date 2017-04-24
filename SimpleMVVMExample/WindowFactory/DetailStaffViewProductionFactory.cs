using System.Threading.Tasks;
using AsyncShowDialog;
using SimpleMVVMExample.Staff;

namespace SimpleMVVMExample.WindowFactory
{
    class DetailStaffViewProductionFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public async Task<bool?> CreateNewWindow(object selectedItem)
        {
            var window = new DetailStaffView
            {
                DataContext = new DetailStaffViewModel((StaffModel) selectedItem)
            };

            return await window.ShowDialogAsync();
        }

        #endregion
    }
}
