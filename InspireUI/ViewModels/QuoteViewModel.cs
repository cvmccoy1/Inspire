using InspireData;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace Inspire.ViewModels
{
    public class QuoteViewModel : BaseViewModel
    {
        private IQuoteService _quoteService; 

        /// <summary>
        /// Contains a list of available quotes to display
        /// </summary>
        private List<IQuoteData> _quoteDataList;

        /// <summary>
        /// Contains the index into the list of quotes of the one currently in use
        /// </summary>
        private int _currentQuoteIndex = 0;


        /// <summary>
        /// Property Bound to the Quote TextBlock Text
        /// </summary>
        public string Quote { get; set; }

        /// <summary>
        /// Property Bound to the Author TextBlock Text
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Property Bound to the Author TextBlock Visibility
        /// </summary>
        public Visibility AuthorVisibility { get; set; }

        /// <summary>
        /// Command Bound the New Quote Button Click Event
        /// </summary>
        [DoNotNotify]
        public RelayCommand NewQuoteButtonCommand { get; set; }

        /// <summary>
        /// Command Bound to the Quote Mouse Enter Event
        /// </summary>
        [DoNotNotify]
        public RelayCommand QuoteMouseEnterCommand { get; set; }

        /// <summary>
        /// Command Bound to the Quote Mouse Exit Event
        /// </summary
        [DoNotNotify]
        public RelayCommand QuoteMouseLeaveCommand { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public QuoteViewModel(IQuoteService quoteService)
        {
            _quoteService = quoteService;

            // Establish binding to the New Quote button
            NewQuoteButtonCommand = new RelayCommand(o => NewQuoteButtonClick(nameof(NewQuoteButtonCommand)));

            // Establish binding to the Mouse Enter and Leave events for the Quote TextBlock
            QuoteMouseEnterCommand = new RelayCommand(o => QuoteMouseEnter(nameof(QuoteMouseEnterCommand)));
            QuoteMouseLeaveCommand = new RelayCommand(o => QuoteMouseLeave(nameof(QuoteMouseLeaveCommand)));

            // Initially the Author TextBlock is hidden...visible only when mouse is hoving over the Quote TextBlock
            AuthorVisibility = Visibility.Hidden;

            UpdateQuoteUi();
        }

        private void NewQuoteButtonClick(object sender)
        {
            UpdateQuoteUi();
        }

        private void QuoteMouseEnter(object sender)
        {
            AuthorVisibility = Visibility.Visible;
        }

        private void QuoteMouseLeave(object sender)
        {
            AuthorVisibility = Visibility.Hidden;
        }

        /// <summary>
        /// Updates the Quote Information from the Quotes Service
        /// </summary>
        /// <remarks> The Quotes Service returns a list of 10 quotes.  Once all of the quotes have
        /// been displayed, a request for 10 new quotes will be done.</remarks>
        private async void UpdateQuoteUi()
        {
            await GetNextAvailableQuote();
            UpdateQuoteAndAuthorUi();
        }

        /// <summary>
        /// A list of quotes (about 10) is kept until all the quotes in the list have been displayed.
        /// Once all of the quotes have been used (or at start up), the list is refreshed with a new 
        /// list of quotes, retrieved from the <see cref="QuoteService"/>.
        /// </summary>
        private async Task GetNextAvailableQuote()
        {
            int quoteCount = _quoteDataList != null ? _quoteDataList.Count : 0;
            if (++_currentQuoteIndex >= quoteCount)
            {
                try
                {
                    _quoteDataList = await _quoteService.GetQouteData();
                }
                catch (Exception exp)
                {
                    Debug.Fail($"Unable to retieve the quote data list: {exp.Message}");
                }
                finally
                {
                    _currentQuoteIndex = 0;
                }
            }
        }

        private void UpdateQuoteAndAuthorUi()
        {
            if (_quoteDataList != null && _quoteDataList.Count > 0)
            {
                Quote = _quoteDataList[_currentQuoteIndex].Quote.TrimEnd('\n');
                Author = $"-- {_quoteDataList[_currentQuoteIndex].Author}";
            }
            else
            {
                Quote = "Unable to receive a quote";
                Author = string.Empty;
            }
        }
    }
}
