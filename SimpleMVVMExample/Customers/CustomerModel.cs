using System;

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

        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value != _customerId)
                {
                    _customerId = value;
                    OnPropertyChanged("CustomerId");
                }
            }
        }

        public string Forename
        {
            get { return _forename; }
            set
            {
                if (value != _forename)
                {
                    _forename = value;
                    OnPropertyChanged("Forename");
                }
            }
        }
        public string Surname
        {
            get { return _surname; }
            set
            {
                if (value != _surname)
                {
                    _surname = value;
                    OnPropertyChanged("Surname");
                }
            }
        }

        public string Company
        {
            get { return _company; }
            set
            {
                if (value != _company)
                {
                    _company = value;
                    OnPropertyChanged("Company");
                }
            }
        }

        public string Street
        {
            get { return _street; }
            set
            {
                if (value != _street)
                {
                    _street = value;
                    OnPropertyChanged("Street");
                }
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value != _phone)
                {
                    _phone = value;
                    OnPropertyChanged("Phone");
                }
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                if (value != _dateOfBirth)
                {
                    _dateOfBirth = value;
                    OnPropertyChanged("DateOfBirth");
                }
            }
        }

        public string Town
        {
            get { return _town; }
            set
            {
                if (value != _town)
                {
                    _town = value;
                    OnPropertyChanged("Town");
                }
            }
        }

        public string County
        {
            get { return _county; }
            set
            {
                if (value != _county)
                {
                    _county = value;
                    OnPropertyChanged("County");
                }
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        public bool Active
        {
            get { return _active; }
            set
            {
                if (value != _active)
                {
                    _active = value;
                    OnPropertyChanged("Active");
                }
            }
        }


        #endregion // Properties
    }
}
