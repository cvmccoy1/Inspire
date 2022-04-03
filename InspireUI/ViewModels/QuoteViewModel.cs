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
        private readonly IQuoteService _quoteService;
        private QuoteData _quoteData;

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

            // Initially the Author TextBlock is hidden...visible only when mouse is hovering over the Quote TextBlock
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
            _quoteData = await Task.Run(() => _quoteService.GetQouteData());
            UpdateQuoteAndAuthorUi();
        }


        private void UpdateQuoteAndAuthorUi()
        {
            if (_quoteData != null && !string.IsNullOrWhiteSpace(_quoteData.Quote))
            {
                Quote = _quoteData.Quote.TrimEnd('\n');
                Author = $"-- {_quoteData.Author}";
            }
            else
            {
                Quote = "Unable to receive a quote";
                Author = string.Empty;
            }
        }
    }
}
