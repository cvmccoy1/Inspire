using Inspire.ViewModels;
using InspireData;
using System.Windows.Controls;

namespace Inspire.Views
{
    /// <summary>
    /// Interaction logic for WeatherView.xaml
    /// </summary>
    public partial class WeatherView : UserControl
    {
        public WeatherView()
        {
            InitializeComponent();
            DataContext = new WeatherViewModel(new WeatherService());
        }
    }
}
