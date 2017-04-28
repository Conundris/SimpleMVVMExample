using System.Threading.Tasks;
using AsyncShowDialog;
using SimpleMVVMExample.Ticket;

namespace SimpleMVVMExample.WindowFactory
{
    class PaymentViewProductionFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public async Task<bool?> CreateNewWindow(object selectedItem)
        {
            TicketModel item = (TicketModel) selectedItem;

            var window = new PaymentView(item.INTTICKETID)
            {
                DataContext = new DetailTicketViewModel(item)
            };

            return await window.ShowDialogAsync();
        }

        #endregion
    }
}
