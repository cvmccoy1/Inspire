using Inspire.ViewModels;
using InspireData;
using System.Windows.Controls;

namespace Inspire.Views
{
    /// <summary>
    /// Interaction logic for QuoteView.xaml
    /// </summary>
    public partial class QuoteView : UserControl
    {
        public QuoteView()
        {
            InitializeComponent();
            DataContext = new QuoteViewModel(new QuoteService());
        }
    }
}
