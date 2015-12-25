using Brownian_Motion.ViewModel;

namespace Brownian_Motion
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
