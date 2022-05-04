using BackOffice.ViewModal;

namespace BackOffice
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = new ShellViewModel();
        }
    }
}