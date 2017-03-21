using SimpleMVVMExample.Staff;

namespace SimpleMVVMExample.WindowFactory
{
    class DetailStaffViewProductionFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public void CreateNewWindow(object selectedItem)
        {
            var window = new DetailStaffView
            {
                DataContext = new DetailStaffViewModel((StaffModel) selectedItem)
            };
            window.Show();
        }

        #endregion
    }
}
