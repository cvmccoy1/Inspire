using InspireData;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Inspire.ViewModels
{
    /// <summary>
    /// The Main Window's View Model
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        public BitmapImage BackgroundImage { get; set; }

        [DoNotNotify]
        public RelayCommand NewImageButtonCommand { get; set; }

        public MainWindowViewModel()
        {
            NewImageButtonCommand = new RelayCommand(o => NewImageButtonClick(nameof(NewImageButtonCommand)));
            UpdateBackgroundImageUi();
        }

        private void NewImageButtonClick(object sender)
        {
            UpdateBackgroundImageUi();
        }

        private async void UpdateBackgroundImageUi()
        {
            // This operation seems to take a while, so let the user know via the mouse icon
            SetMouseCursor(Cursors.Wait);
            try
            {
                ImageData imageData = await ImageService.GetImageData();
                Uri uriSource = new Uri(imageData.Url, UriKind.Absolute);
                BackgroundImage = new BitmapImage(uriSource);
            }
            catch (Exception exp)
            {
                Debug.Fail($"Unable to retieve the background image: {exp.Message}");
            }
            finally
            {
                SetMouseCursor(null);
            }
        }
    }
}
