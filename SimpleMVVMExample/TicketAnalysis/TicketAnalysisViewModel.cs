using System;
using System.Windows.Input;

namespace SimpleMVVMExample.TicketAnalysis
{
    public class TicketAnalysisViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private ICommand _runTicketAnalysisCommand;
        private DateTime _datFrom;
        private DateTime _datTo;

        #endregion

        public string Name => "TicketAnalysis";

    }
}
