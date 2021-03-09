using Inspire.ViewModels;
using InspireData;
using System.Windows.Controls;

namespace Inspire.Views
{
    /// <summary>
    /// Interaction logic for ClockView.xaml
    /// </summary>
    public partial class ClockView : UserControl
    {
        public ClockView()
        {
            InitializeComponent();
            DataContext = new ClockViewModel(new ClockService());
        }
    }
}
