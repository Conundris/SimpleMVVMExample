namespace SimpleMVVMExample
{
    public class TicketModel : ObservableObject
    {
        #region Fields

        private int _intticketid;
        private string _strtickettitle;
        private int _intassignedto;
        private int _intrequestedby;
        private decimal _strticketdescription;

        #endregion // Fields

        #region Properties

        public int INTTICKETID
        {
            get { return _intticketid; }
            set
            {
                if (value == _intticketid) return;
                _intticketid = value;
                OnPropertyChanged("INTTICKETID");
            }
        }

        public string STRTITLE
        {
            get { return _strtickettitle; }
            set
            {
                if (value != _strtickettitle)
                {
                    _strtickettitle = value;
                    OnPropertyChanged("STRTITLE");
                }
            }
        }

        public int INTASSIGNEDTO
        {
            get { return _intassignedto; }
            set
            {
                if (value != _intassignedto)
                {
                    _intassignedto = value;
                    OnPropertyChanged("INTASSIGNEDTO");
                }
            }
        }

        public int INTREQUESTEDBY
        {
            get { return _intrequestedby; }
            set
            {
                if (value != _intrequestedby)
                {
                    _intrequestedby = value;
                    OnPropertyChanged("INTREQUESTEDBY");
                }
            }
        }

        public decimal STRTICKETDESCRIPTION
        {
            get { return _strticketdescription; }
            set
            {
                if (value != _strticketdescription)
                {
                    _strticketdescription = value;
                    OnPropertyChanged("STRTICKETDESCRIPTION");
                }
            }
        }

        #endregion // Properties
    }
}
