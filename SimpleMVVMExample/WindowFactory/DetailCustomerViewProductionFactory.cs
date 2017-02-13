using SimpleMVVMExample.Customers;

namespace SimpleMVVMExample.WindowFactory
{
    class DetailCustomerViewProductionFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public void CreateNewWindow(object selectedItem)
        {
            var window = new DetailCustomerView
            {
                DataContext = new DetailCustomerViewModel((CustomerModel) selectedItem)
            };
            window.Show();
        }

        #endregion
    }
}
