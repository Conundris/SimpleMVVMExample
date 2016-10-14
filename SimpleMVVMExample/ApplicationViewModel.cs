using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using SimpleMVVMExample.Customers;

namespace SimpleMVVMExample
{
    public class ApplicationViewModel : ObservableObject
    {
        #region Fields

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion

        public ApplicationViewModel()
        {
            // Add available pages
            PageViewModels.Add(new HomeViewModel());
            PageViewModels.Add(new TicketViewModel());
            PageViewModels.Add(new CustomerViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand ?? (_changePageCommand = new RelayCommand(
                           p => ChangeViewModel((IPageViewModel) p),
                           p => p is IPageViewModel));
            }
        }

        public List<IPageViewModel> PageViewModels => _pageViewModels ?? (_pageViewModels = new List<IPageViewModel>());

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel == value) return;
                _currentPageViewModel = value;
                OnPropertyChanged("CurrentPageViewModel");
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}
