using System; 
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using FormValidationExample.Infrastructure;
using MvvmValidation;
using Oracle.ManagedDataAccess.Client;
using SimpleMVVMExample.DB;
// ReSharper disable ExplicitCallerInfoArgument

namespace SimpleMVVMExample.Customers
{
    class DetailCustomerViewModel : ValidatableViewModelBase
    {
        private CustomerModel selectedCustomer;
        private bool? isValid;
        private string validationErrorsString;

        public DetailCustomerViewModel(CustomerModel selectedCustomer)
        {
            this.selectedCustomer = selectedCustomer;
            CreateCustomerCommand = new GalaSoft.MvvmLight.CommandWpf.RelayCommand(SaveAndCloseCustomer);

            ConfigureValidationRules();
            Validator.ResultChanged += OnValidationResultChanged;
        }

        public ICommand CreateCustomerCommand { get; set; }

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

        private void SaveAndCloseCustomer()
        {
            // Insert (Create new Customer)
            if (selectedCustomer.INTCUSTOMERID == 0)
            {
                using (var cmd = DC.GetOpenConnection().CreateCommand())
                {
                    cmd.CommandText =
                        "INSERT INTO tblCustomer (strSurname, strForename, strCompany, strPhone, DATDATEOFBIRTH, strStreet, strTown, strCountry, strCounty)" +
                        " VALUES (:strSurname, :strForename, :strCompany, :strPhone, :DATDATEOFBIRTH, :strStreet, :strTown, :strCountry, :strCounty)";

                    cmd.Parameters.Add(new OracleParameter("strSurname", selectedCustomer.STRSURNAME));
                    cmd.Parameters.Add(new OracleParameter("strForename", selectedCustomer.STRFORENAME));
                    cmd.Parameters.Add(new OracleParameter("strCompany", selectedCustomer.STRCOMPANY));
                    cmd.Parameters.Add(new OracleParameter("strPhone", selectedCustomer.STRPHONE));
                    cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date,
                        selectedCustomer.DATDATEOFBIRTH,
                        ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("strStreet", selectedCustomer.STRSTREET));
                    cmd.Parameters.Add(new OracleParameter("strTown", selectedCustomer.STRTOWN));
                    cmd.Parameters.Add(new OracleParameter("strCountry", selectedCustomer.STRCOUNTRY));
                    cmd.Parameters.Add(new OracleParameter("strCounty", selectedCustomer.STRCOUNTY));

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
            else // Update Customer
            {
                using (var cmd = DC.GetOpenConnection().CreateCommand())
                {
                    cmd.CommandText =
                        "UPDATE tblCustomer" +
                        "SET strSurname = :strSurname" +
                        "SET strForename = :strForename" +
                        "SET strCompany = :strCompany" +
                        "SET strPhone = :strPhone" +
                        "SET DATDATEOFBIRTH = :DATDATEOFBIRTH" +
                        "SET strStreet = :strStreet" +
                        "SET strTown = :strTown" +
                        "SET strCountry = :strCountry" +
                        "SET strCounty = :strCounty" +
                        "WHERE intCustomerID = :intCustomerID";

                    cmd.Parameters.Add(new OracleParameter("intCustomerID", selectedCustomer.INTCUSTOMERID));
                    cmd.Parameters.Add(new OracleParameter("strSurname", selectedCustomer.STRSURNAME));
                    cmd.Parameters.Add(new OracleParameter("strForename", selectedCustomer.STRFORENAME));
                    cmd.Parameters.Add(new OracleParameter("strCompany", selectedCustomer.STRCOMPANY));
                    cmd.Parameters.Add(new OracleParameter("strPhone", selectedCustomer.STRPHONE));
                    cmd.Parameters.Add(new OracleParameter("DATDATEOFBIRTH", OracleDbType.Date,
                        selectedCustomer.DATDATEOFBIRTH,
                        ParameterDirection.Input));
                    cmd.Parameters.Add(new OracleParameter("strStreet", selectedCustomer.STRSTREET));
                    cmd.Parameters.Add(new OracleParameter("strTown", selectedCustomer.STRTOWN));
                    cmd.Parameters.Add(new OracleParameter("strCountry", selectedCustomer.STRCOUNTRY));
                    cmd.Parameters.Add(new OracleParameter("strCounty", selectedCustomer.STRCOUNTY));

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (OracleException ex)
                    {
                        Console.WriteLine(ex.Message);
                        throw;
                    }
                }
            }
        }

        private void ConfigureValidationRules()
        {
            Validator.AddRequiredRule(() => selectedCustomer.STRFORENAME, "Forename is required");

            Validator.AddRequiredRule(() => selectedCustomer.STRSURNAME, "Surname is required");

            Validator.AddRule(nameof(selectedCustomer.STREMAIL),
                () =>
                {
                    const string regexPattern =
                        @"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$";
                    return RuleResult.Assert(Regex.IsMatch(selectedCustomer.STREMAIL, regexPattern),
                        "Email must by a valid email address");
                });


            /*Validator.AddAsyncRule(nameof(selectedCustomer.),
                async () =>
                {
                    var isAvailable = await UserRegistrationService.IsUserNameAvailable(UserName).ToTask();

                    return RuleResult.Assert(isAvailable,
                        string.Format("User Name {0} is taken. Please choose a different one.", UserName));
                });*/


            //Validator.AddChildValidatable(() => InterestSelectorViewModel);
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
        private void UpdateValidationSummary(ValidationResult validationResult)
        {
            IsValid = validationResult.IsValid;
            ValidationErrorsString = validationResult.ToString();
        }
    }
}
