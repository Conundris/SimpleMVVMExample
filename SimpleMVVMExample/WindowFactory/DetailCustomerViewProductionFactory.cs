using System.Threading.Tasks;
using AsyncShowDialog;
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
