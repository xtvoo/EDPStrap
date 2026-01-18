using Bloxstrap.UI.ViewModels.Settings;

namespace Bloxstrap.UI.Elements.Settings.Pages
{
    /// <summary>
    /// Interaction logic for OptimizerPage.xaml
    /// </summary>
    public partial class OptimizerPage
    {
        public OptimizerPage()
        {
            DataContext = new OptimizerViewModel();
            InitializeComponent();
        }
    }
}
