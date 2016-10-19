using System;

namespace SimpleMVVMExample
{
    public class CustomerModel : ObservableObject
    {
        #region Fields

        private int _customerId;
        private string _surname;
        private string _forename;
        private string _gender;
        private string _street;
        private string _town;
        private string _state;
        private string _postcode;
        private DateTime _dateOfBirth;

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
        #endregion // Properties
    }
}
