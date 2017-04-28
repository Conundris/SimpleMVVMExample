using System.Windows;

namespace SimpleMVVMExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var app = new ApplicationView();
            var context = new ApplicationViewModel();
            app.DataContext = context;
            app.Show();
        }
    }
}
