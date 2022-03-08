using System;
using System.Windows.Input;

namespace WorkTimeTracker.Core.Wpf.Loading
{
    internal class LoaderDisposable : IDisposable
    {
        public event EventHandler<Cursor>? Disposed;

        public void Dispose()
        {
            Disposed?.Invoke(this, Cursors.Arrow);
        }
    }
}