using System.ComponentModel;
using System.Threading;
using System.Windows.Input;

namespace Inspire.ViewModels
{
    /// <summary>
    /// A Base View Model that fires Property Changed events automagically as needed 
    /// (via fody-see https://www.shenchauhan.com/blog/2018/6/18/fody-inotifypropertychanged-the-easy-way).
    /// </summary>
    //[AddINotifyPropertyChangedInterface]
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly SynchronizationContext _syncContext;

        public BaseViewModel()
        {
            // we assume this ctor is called from the UI thread!
            _syncContext = SynchronizationContext.Current;
        }

        /// <summary>
        /// The event that is fired when any child property changes it value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => 
        {
            // A body for debugging
        };

        protected void SetMouseCursor(Cursor cursor)
        {
            _syncContext.Post(o => Mouse.OverrideCursor = cursor, null);
        }
    }
}
