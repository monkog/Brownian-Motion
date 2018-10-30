using BrownianMotion.ViewModel;

namespace BrownianMotion
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