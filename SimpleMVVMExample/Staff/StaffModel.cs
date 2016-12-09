namespace SimpleMVVMExample.Staff
{
    public class StaffModel : ObservableObject
    {
        #region Fields

        private int _staffID;
        private string _forename;
        private string _surname;
        private string _username;
        private string _password;
        private string _email;
        private bool _active;

        #endregion // Fields

        #region Properties

        public int StaffID
        {
            get { return _staffID; }
            set
            {
                if (value != _staffID)
                {
                    _staffID = value;
                    OnPropertyChanged("StaffID");
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

        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    _username = value;
                    OnPropertyChanged("Username");
                }
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    _email = value;
                    OnPropertyChanged("Email");
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
