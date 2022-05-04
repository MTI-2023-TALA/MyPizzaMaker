using BackOffice.ViewModal;

namespace BackOffice
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = new ShellViewModel();
        }
    }
}