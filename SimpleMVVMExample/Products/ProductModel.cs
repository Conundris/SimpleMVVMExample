namespace SimpleMVVMExample
{
    public class ProductModel : ObservableObject
    {
        #region Fields

        private int _ticketId;
        private string _ticketTitle;
        private decimal _ticketDescription;

        #endregion // Fields

        #region Properties

        public int TicketId
        {
            get { return _ticketId; }
            set
            {
                if (value != _ticketId)
                {
                    _ticketId = value;
                    OnPropertyChanged("TicketId");
                }
            }
        }

        public string TicketTitle
        {
            get { return _ticketTitle; }
            set
            {
                if (value != _ticketTitle)
                {
                    _ticketTitle = value;
                    OnPropertyChanged("TicketTitle");
                }
            }
        }

        public decimal TicketDescription
        {
            get { return _ticketDescription; }
            set
            {
                if (value != _ticketDescription)
                {
                    _ticketDescription = value;
                    OnPropertyChanged("TicketDescription");
                }
            }
        }

        #endregion // Properties
    }
}
