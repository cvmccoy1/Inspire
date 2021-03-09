using InspireData;
using PropertyChanged;
using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Inspire.ViewModels
{
    /// <summary>
    /// The Main Window's View Model
    /// </summary>
    public class MainWindowViewModel : BaseViewModel
    {
        private IImageService _imageService;

        /// <summary>
        /// Property bound to the Background's Image
        /// </summary>
        public BitmapImage BackgroundImage { get; set; }

        /// <summary>
        /// Command bound to the New Image Button's Click event
        /// </summary>
        [DoNotNotify]
        public RelayCommand NewImageButtonCommand { get; set; }

        public MainWindowViewModel(IImageService imageService)
        {
            _imageService = imageService;

            //Establish bindling to the New Image button
            NewImageButtonCommand = new RelayCommand(o => NewImageButtonClick(nameof(NewImageButtonCommand)));

            UpdateBackgroundImageUi();
        }

        /// <summary>
        /// Method called when the New Image button is clicked.
        /// </summary>
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
                IImageData imageData = await _imageService.GetImageData();
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
