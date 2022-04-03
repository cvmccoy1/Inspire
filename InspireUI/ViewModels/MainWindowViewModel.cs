using InspireData;
using PropertyChanged;
using System;
using System.Diagnostics;
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
        private readonly IImageService _imageService;

        /// <summary>
        /// Property bound to the Background's Image
        /// </summary>
        public BitmapImage BackgroundImage { get; set; }

        /// <summary>
        /// Command bound to the New Image Button's Click event
        /// </summary>
        [DoNotNotify]
        public RelayCommand NewImageButtonCommand { get; set; }

        /// <summary>
        /// Command Bound to the Background Image changed Event
        /// </summary>
        [DoNotNotify]
        public RelayCommand ImageChangedCommand { get; set; }

        public MainWindowViewModel(IImageService imageService)
        {
            _imageService = imageService;

            //Establish binding to the New Image button
            NewImageButtonCommand = new RelayCommand(_ => NewImageButtonClick(nameof(NewImageButtonCommand)));
            ImageChangedCommand = new RelayCommand(_ => ImageChanged(nameof(ImageChangedCommand)));

            UpdateBackgroundImageUiAsync();
        }

        /// <summary>
        /// Method called when the New Image button is clicked.
        /// </summary>
        private void NewImageButtonClick(string sender)
        {
            UpdateBackgroundImageUiAsync();
        }

        /// <summary>
        /// Method called when the Background Image Change event is fired.
        /// </summary>
        /// <param name="sender"></param>
        private void ImageChanged(string sender)
        {
            SetMouseCursor(null);
        }

        private async void UpdateBackgroundImageUiAsync()
        {
            // This operation seems to take a while, so let the user know via the mouse icon
            SetMouseCursor(Cursors.Wait);
            try
            {
                ImageData imageData = await Task.Run(() =>_imageService.GetImageData());

                Uri uriSource = new Uri(imageData.Url, UriKind.Absolute);
                BackgroundImage = new BitmapImage(uriSource);
            }
            catch (Exception exp)
            {
                SetMouseCursor(null);
                Debug.Fail($"Unable to retrieve the background image: {exp.Message}");
            }
        }
    }
}
