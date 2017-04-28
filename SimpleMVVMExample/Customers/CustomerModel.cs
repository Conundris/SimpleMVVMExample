using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FormValidationExample.Infrastructure;
using MvvmValidation;

// ReSharper disable InconsistentNaming

namespace SimpleMVVMExample
{
    public class CustomerModel : ValidatableViewModelBase
    {
        #region Fields

        private int _customerId;
        private string _surname;
        private string _forename;
        private string _email;
        private string _company;
        private string _street;
        private string _phone;
        private string _town;
        private string _county;
        private string _country;
        private DateTime _dateOfBirth;
        private char _active;
        private bool? isValid;
        private string validationErrorsString;

        #endregion // Fields
        // CStr
        public CustomerModel()
        {
            ConfigureValidationRules();
            Validator.ResultChanged += OnValidationResultChanged;
            DATDATEOFBIRTH = DateTime.Now;
        }


        #region Properties

        public int INTCUSTOMERID
        {
            get { return _customerId; }
            set
            {
                if (value == _customerId) return;
                _customerId = value;
                RaisePropertyChanged(nameof(INTCUSTOMERID));
                Validator.Validate(nameof(INTCUSTOMERID));
            }
        }

        public string STRFORENAME
        {
            get { return _forename; }
            set
            {
                if (value == _forename) return;
                _forename = value;
                RaisePropertyChanged(nameof(STRFORENAME));
                Validator.Validate(nameof(STRFORENAME));
            }
        }
        public string STRSURNAME
        {
            get { return _surname; }
            set
            {
                if (value == _surname) return;
                _surname = value;
                RaisePropertyChanged(nameof(STRSURNAME));
                Validator.Validate(nameof(STRSURNAME));
            }
        }

        public string STREMAIL
        {
            get { return _email; }
            set
            {
                if (value == _email) return;
                _email = value;
                RaisePropertyChanged(nameof(STREMAIL));
                Validator.Validate(nameof(STREMAIL));
            }
        }

        public string STRCOMPANY
        {
            get { return _company; }
            set
            {
                if (value == _company) return;
                _company = value;
                RaisePropertyChanged(nameof(STRCOMPANY));
                Validator.Validate(nameof(STRCOMPANY));
            }
        }

        public string STRSTREET
        {
            get { return _street; }
            set
            {
                if (value == _street) return;
                _street = value;
                RaisePropertyChanged(nameof(STRSTREET));
                Validator.Validate(nameof(STRSTREET));
            }
        }

        public string STRPHONE
        {
            get { return _phone; }
            set
            {
                if (value == _phone) return;
                _phone = value;
                RaisePropertyChanged(nameof(STRPHONE));
                Validator.Validate(nameof(STRPHONE));
            }
        }

        public DateTime DATDATEOFBIRTH
        {
            get { return _dateOfBirth; }
            set
            {
                if (value == _dateOfBirth) return;
                _dateOfBirth = value;
                RaisePropertyChanged(nameof(DATDATEOFBIRTH));
                Validator.Validate(nameof(DATDATEOFBIRTH));
            }
        }

        public string STRTOWN
        {
            get { return _town; }
            set
            {
                if (value == _town) return;
                _town = value;
                RaisePropertyChanged(nameof(STRTOWN));
                Validator.Validate(nameof(STRTOWN));
            }
        }

        public string STRCOUNTY
        {
            get { return _county; }
            set
            {
                if (value == _county) return;
                _county = value;
                RaisePropertyChanged(nameof(STRCOUNTY));
                Validator.Validate(nameof(STRCOUNTY));
            }
        }

        public string STRCOUNTRY
        {
            get { return _country; }
            set
            {
                if (value == _country) return;
                _country = value;
                RaisePropertyChanged(nameof(STRCOUNTRY));
                Validator.Validate(nameof(STRCOUNTRY));
            }
        }

        public char BLNACTIVE
        {
            get { return _active; }
            set
            {
                if (value == _active) return;
                _active = value;
                RaisePropertyChanged(nameof(BLNACTIVE));
                Validator.Validate(nameof(BLNACTIVE));
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

        #region Methods

        // Configure all Validation Rules for the Model
        private void ConfigureValidationRules()
        {
            Validator.AddRequiredRule(() => STRFORENAME, "Forename is required");
            Validator.AddRequiredRule(() => STRSURNAME, "Surname is required");
            Validator.AddRequiredRule(() => STRPHONE, "Phonenumber is required");
            Validator.AddRequiredRule(() => DATDATEOFBIRTH, "Date of birth is required");


            Validator.AddRule(nameof(DATDATEOFBIRTH),
                () => RuleResult.Assert(IsDateValid(DATDATEOFBIRTH), "The chosen date is in the future. Please choose a date of birth in the past."));

            Validator.AddRule(nameof(STRPHONE),
                () =>
                {
                    const string regexPattern =
                        @"^\d+$";
                    return RuleResult.Assert(Regex.IsMatch(STRPHONE, regexPattern),
                        "Phone Number can only contain Numbers.");
                });
        }

        private static bool IsDateValid(DateTime datdateofbirth)
        {
            return datdateofbirth <= DateTime.Now;
        }

        private async void Validate()
        {
            await ValidateAsync();
        }

        public async Task ValidateAsync()
        {
            var result = await Validator.ValidateAllAsync();

            UpdateValidationSummary(result);
        }
        // If Validation Result changes
        private void OnValidationResultChanged(object sender, ValidationResultChangedEventArgs e)
        {
            if (!IsValid.GetValueOrDefault(true))
            {
                var validationResult = Validator.GetResult();

                UpdateValidationSummary(validationResult);
            }
        }
        private void UpdateValidationSummary(ValidationResult validationResult)
        {
            IsValid = validationResult.IsValid;
            ValidationErrorsString = validationResult.ToString();
        }

        #endregion

        // Overriden for Usage in Ticket View
        public override string ToString()
        {
            return $"{_surname} {_forename}";
        }
    }
}
