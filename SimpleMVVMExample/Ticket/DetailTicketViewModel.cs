namespace SimpleMVVMExample.Ticket
{
    public class DetailTicketViewModel : ObservableObject, IPageViewModel
    {
        private TicketModel _ticket;

        public TicketModel Ticket { get; set; }

        public string Name => "Ticket";
    }
}
