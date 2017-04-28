using System.Threading.Tasks;
using AsyncShowDialog;
using SimpleMVVMExample.Ticket;

namespace SimpleMVVMExample.WindowFactory
{
    class DetailTicketViewProductionFactory : IWindowFactory
    {
        #region Implementation of INewWindowFactory

        public async Task<bool?> CreateNewWindow(object selectedItem)
        {
            var window = new DetailTicketView
            {
                DataContext = new DetailTicketViewModel((TicketModel) selectedItem)
            };

            return await window.ShowDialogAsync();
        }

        #endregion
    }
}
