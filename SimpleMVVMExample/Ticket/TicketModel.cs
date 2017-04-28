using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FormValidationExample.Infrastructure;
using MvvmValidation;
using ValidationResult = MvvmValidation.ValidationResult;

namespace SimpleMVVMExample
{
    public class TicketModel : ValidatableViewModelBase
    {
        #region Fields

        private int _intticketid;
        private string _strtickettitle;
        private int _intassignedto;
        private int _intrequestby;
        private string _strassignedto;
        private string _strrequestby;
        private string _strdescription;
        private string _strstatus;
        private bool? isValid;
        private string validationErrorsString;
        private char _blnfinished;

        #endregion // Fields

        public TicketModel()
        {
           ConfigureValidationRules();
           Validator.ResultChanged += OnValidationResultChanged;
           Validate();
        }

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
                    Validator.Validate(nameof(STRTITLE));
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

        public int INTREQUESTBY
        {
            get { return _intrequestby; }
            set
            {
                if (value != _intrequestby)
                {
                    _intrequestby = value;
                    OnPropertyChanged("INTREQUESTBY");
                }
            }
        }

        public string STRREQUESTBY
        {
            get { return _strrequestby; }
            set
            {
                if (value != _strrequestby)
                {
                    _strrequestby = value;
                    OnPropertyChanged("STRREQUESTBY");
                }
            }
        }

        public string STRASSIGNEDTO
        {
            get { return _strassignedto; }
            set
            {
                if (value != _strassignedto)
                {
                    _strassignedto = value;
                    OnPropertyChanged("STRASSIGNEDTO");
                }
            }
        }

        public string STRDESCRIPTION
        {
            get { return _strdescription; }
            set
            {
                if (value != _strdescription)
                {
                    _strdescription = value;
                    OnPropertyChanged("STRDESCRIPTION");
                    Validator.Validate(nameof(STRDESCRIPTION));
                }
            }
        }

        public string STRSTATUS
        {
            get { return _strstatus; }
            set
            {
                if (value != _strstatus)
                {
                    _strstatus = value;
                    OnPropertyChanged("STRSTATUS");
                }
            }
        }

        public char BLNFINISHED
        {
            get { return _blnfinished; }
            set
            {
                if (value != _blnfinished)
                {
                    _blnfinished = value;
                    OnPropertyChanged("BLNFINISHED");
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

        private void ConfigureValidationRules()
        {
            Validator.AddRequiredRule(() => STRTITLE, "Title is required");
            Validator.AddRequiredRule(() => STRDESCRIPTION, "Description is required");
            Validator.AddRequiredRule(() => INTREQUESTBY, "Email is required");
        }
    }
}

