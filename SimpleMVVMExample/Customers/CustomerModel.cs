using System;
// ReSharper disable InconsistentNaming

namespace SimpleMVVMExample
{
    public class CustomerModel : ObservableObject
    {
        #region Fields

        private int _customerId;
        private string _surname;
        private string _forename;
        private string _company;
        private string _street;
        private string _phone;
        private string _town;
        private string _county;
        private string _country;
        private DateTime _dateOfBirth;
        private bool _active;

        #endregion // Fields

        #region Properties

        public int INTCUSTOMERID
        {
            get { return _customerId; }
            set
            {
                if (value == _customerId) return;
                _customerId = value;
                OnPropertyChanged("INTCUSTOMERID");
            }
        }

        public string STRFORENAME
        {
            get { return _forename; }
            set
            {
                if (value == _forename) return;
                _forename = value;
                OnPropertyChanged("STRFORENAME");
            }
        }
        public string STRSURNAME
        {
            get { return _surname; }
            set
            {
                if (value == _surname) return;
                _surname = value;
                OnPropertyChanged("STRSURNAME");
            }
        }

        public string STRCOMPANY
        {
            get { return _company; }
            set
            {
                if (value == _company) return;
                _company = value;
                OnPropertyChanged("STRCOMPANY");
            }
        }

        public string STRSTREET
        {
            get { return _street; }
            set
            {
                if (value == _street) return;
                _street = value;
                OnPropertyChanged("STRSTREET");
            }
        }

        public string STRPHONE
        {
            get { return _phone; }
            set
            {
                if (value == _phone) return;
                _phone = value;
                OnPropertyChanged("STRPHONE");
            }
        }

        public DateTime DATDATEOFBIRTH
        {
            get { return _dateOfBirth; }
            set
            {
                if (value == _dateOfBirth) return;
                _dateOfBirth = value;
                OnPropertyChanged("DATDATEOFBIRTH");
            }
        }

        public string STRTOWN
        {
            get { return _town; }
            set
            {
                if (value == _town) return;
                _town = value;
                OnPropertyChanged("STRTOWN");
            }
        }

        public string STRCOUNTY
        {
            get { return _county; }
            set
            {
                if (value == _county) return;
                _county = value;
                OnPropertyChanged("STRCOUNTY");
            }
        }

        public string STRCOUNTRY
        {
            get { return _country; }
            set
            {
                if (value == _country) return;
                _country = value;
                OnPropertyChanged("STRCOUNTRY");
            }
        }

        public bool BLNACTIVE
        {
            get { return _active; }
            set
            {
                if (value == _active) return;
                _active = value;
                OnPropertyChanged("BLNACTIVE");
            }
        }


        #endregion // Properties
    }
}
