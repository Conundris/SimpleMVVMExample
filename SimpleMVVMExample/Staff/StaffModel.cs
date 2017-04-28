using System.Text.RegularExpressions;
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
        private string _stremail;
        private char _blnactive;
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
                    Validator.Validate(nameof(INTSTAFFID));
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
                    Validator.Validate(nameof(STRFORENAME));
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
                    Validator.Validate(nameof(STRSURNAME));
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
                    Validator.Validate(nameof(STREMAIL));
                }
            }
        }

        public char BLNACTIVE
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

        #region Methods
        private void ConfigureValidationRules()
        {
            Validator.AddRequiredRule(() => STRFORENAME, "Forename is required");
            Validator.AddRequiredRule(() => STRSURNAME, "Surname is required");
            Validator.AddRequiredRule(() => STREMAIL, "Email is required");

            Validator.AddRule(nameof(STREMAIL),
                () =>
                {
                    const string regexPattern =
                        @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                    return RuleResult.Assert(Regex.IsMatch(STREMAIL, regexPattern, RegexOptions.IgnoreCase),
                        "Email must by a valid email address");
                });
        }

        public async void Validate()
        {
            await ValidateAsync();
        }

        public async Task ValidateAsync()
        {
            var result = await Validator.ValidateAllAsync();

            UpdateValidationSummary(result);
        }
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

        public override string ToString()
        {
            return $"{_strsurname} {_strforename}";
        }


        #endregion

    }
}
