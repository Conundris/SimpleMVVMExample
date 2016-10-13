namespace SimpleMVVMExample
{
    public class TicketModel : ObservableObject
    {
        #region Fields

        private int _ticketId;
        private string _ticketTitle;
        private string _assignedTo;
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

        public string AssignedTo
        {
            get { return _assignedTo; }
            set
            {
                if (value != _assignedTo)
                {
                    _assignedTo = value;
                    OnPropertyChanged("AssignedTo");
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
