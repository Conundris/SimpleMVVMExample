using System.Threading.Tasks;
using FormValidationExample.Infrastructure;
using MvvmValidation;

namespace SimpleMVVMExample.Staff
{
    public class StaffModel : ValidatableViewModelBase
    {
        #region Fields

        private int _intstaffid;
        private string _strforename;
        private string _strsurname;
        private string _strusername;
        private string _strpassword;
        private string _stremail;
        private bool _blnactive;
        private bool? isValid;
        private string validationErrorsString;  

        #endregion // Fields

        public StaffModel()
        {
            ConfigureValidationRules();
            Validator.ResultChanged += OnValidationResultChanged;
        }

        #region Properties

        public int INTSTAFFID
        {
            get { return _intstaffid; }
            set
            {
                if (value != _intstaffid)
                {
                    _intstaffid = value;
                    OnPropertyChanged("INTSTAFFID");
                }
            }
        }

        public string STRFORENAME
        {
            get { return _strforename; }
            set
            {
                if (value != _strforename)
                {
                    _strforename = value;
                    OnPropertyChanged("STRFORENAME");
                }
            }
        }

        public string STRSURNAME
        {
            get { return _strsurname; }
            set
            {
                if (value != _strsurname)
                {
                    _strsurname = value;
                    OnPropertyChanged("STRSURNAME");
                }
            }
        }

        public string STRUSERNAME
        {
            get { return _strusername; }
            set
            {
                if (value != _strusername)
                {
                    _strusername = value;
                    OnPropertyChanged("STRUSERNAME");
                }
            }
        }

        public string STRPASSWORD
        {
            get { return _strpassword; }
            set
            {
                if (value != _strpassword)
                {
                    _strpassword = value;
                    OnPropertyChanged("STRPASSWORD");
                }
            }
        }

        public string STREMAIL
        {
            get { return _stremail; }
            set
            {
                if (value != _stremail)
                {
                    _stremail = value;
                    OnPropertyChanged("STREMAIL");
                }
            }
        }

        public bool BLNACTIVE
        {
            get { return _blnactive; }
            set
            {
                if (value != _blnactive)
                {
                    _blnactive = value;
                    OnPropertyChanged("BLNACTIVE");
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
            Validator.AddRequiredRule(() => STRFORENAME, "Forename is required");
            Validator.AddRequiredRule(() => STRSURNAME, "Surname is required");
            Validator.AddRequiredRule(() => STREMAIL, "Phonenumber is required");
            Validator.AddRequiredRule(() => STRPASSWORD, "Date of birth is required");
            Validator.AddRequiredRule(() => STRUSERNAME, "Date of birth is required");
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
