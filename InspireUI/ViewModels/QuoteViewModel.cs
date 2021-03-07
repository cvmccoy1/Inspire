using InspireData;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inspire.ViewModels
{
    public class QuoteViewModel : BaseViewModel
    {
        private int _currentQuoteIndex = 0;

        private List<QuoteData> _quoteDataList;

        public string Quote { get; set; }

        public string Author { get; set; }

        public Visibility AuthorVisibility { get; set; }

        [DoNotNotify]
        public RelayCommand NewQuoteButtonCommand { get; set; }

        [DoNotNotify]
        public RelayCommand QuoteMouseEnterCommand { get; set; }

        [DoNotNotify]
        public RelayCommand QuoteMouseLeaveCommand { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public QuoteViewModel()
        {
            NewQuoteButtonCommand = new RelayCommand(o => NewQuoteButtonClick(nameof(NewQuoteButtonCommand)));
            QuoteMouseEnterCommand = new RelayCommand(o => QuoteMouseEnter(nameof(QuoteMouseEnterCommand)));
            QuoteMouseLeaveCommand = new RelayCommand(o => QuoteMouseLeave(nameof(QuoteMouseLeaveCommand)));
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
        /// been used, a new request for 10 new quotes is done.</remarks>
        private async void UpdateQuoteUi()
        {
            int quoteCount = _quoteDataList != null ? _quoteDataList.Count : 0;
            if (++_currentQuoteIndex >= quoteCount)
            {
                try
                {
                    _quoteDataList = await QuoteService.GetQouteData();
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
            if (_quoteDataList != null && _quoteDataList.Count > 0)
            {
                Quote = HtmlToTextConverter.HTMLToText(_quoteDataList[_currentQuoteIndex].Quote).TrimEnd('\n');
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
