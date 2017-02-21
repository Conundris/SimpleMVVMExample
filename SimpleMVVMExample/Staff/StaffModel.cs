using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FormValidationExample.Infrastructure;
using MvvmValidation;

namespace SimpleMVVMExample.Staff
{
    public class StaffModel : ValidatableViewModelBase
    {
        #region Fields

        private int _staffID;
        private string _forename;
        private string _surname;
        private string _username;
        private string _password;
        private string _email;
        private bool _active;
        private bool? isValid;
        private string validationErrorsString;  

        #endregion // Fields

        public StaffModel()
        {
            ConfigureValidationRules();
            Validator.ResultChanged += OnValidationResultChanged;
        }

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


        public string ValidationErrorsString
        {
            get { return validationErrorsString; }
            private set
            {
                validationErrorsString = value;
                RaisePropertyChanged(nameof(ValidationErrorsString));
            }
        }

        public bool? IsValid
        {
            get { return isValid; }
            private set
            {
                isValid = value;
                RaisePropertyChanged(nameof(IsValid));
            }
        }

        #endregion // Properties

        private void ConfigureValidationRules()
        {
            Validator.AddRequiredRule(() => Forename, "Forename is required");
            Validator.AddRequiredRule(() => Surname, "Surname is required");
            Validator.AddRequiredRule(() => Email, "Phonenumber is required");
            Validator.AddRequiredRule(() => Password, "Date of birth is required");
            Validator.AddRequiredRule(() => Username, "Date of birth is required");
        }

        private async void Validate()
        {
            await ValidateAsync();
        }

        private async Task ValidateAsync()
        {
            var result = await Validator.ValidateAllAsync();

            UpdateValidationSummary(result);
        }
        private void OnValidationResultChanged(object sender, ValidationResultChangedEventArgs e)
        {
            if (IsValid.GetValueOrDefault(true)) return;
            var validationResult = Validator.GetResult();

            UpdateValidationSummary(validationResult);
        }
        private void UpdateValidationSummary(MvvmValidation.ValidationResult validationResult)
        {
            IsValid = validationResult.IsValid;
            ValidationErrorsString = validationResult.ToString();
        }
    }
}
