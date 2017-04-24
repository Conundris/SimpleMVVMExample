using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using AsyncShowDialog;
using MaterialDesignThemes.Wpf;
using SimpleMVVMExample.Customers;

namespace SimpleMVVMExample.WindowFactory
{
    class DetailCustomerViewProductionFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public async Task<bool?> CreateNewWindow(object selectedItem)
        {
            var window = new DetailCustomerView
            {
                DataContext = new DetailCustomerViewModel((CustomerModel) selectedItem),
            };

            var result = await window.ShowDialogAsync();

            return result;
        }
        #endregion
    }
}
