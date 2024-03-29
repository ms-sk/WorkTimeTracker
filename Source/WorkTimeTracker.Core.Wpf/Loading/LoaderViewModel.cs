﻿using System;
using System.Windows.Input;
using WorkTimeTracker.Core.Wpf.ViewModels;

namespace WorkTimeTracker.Core.Wpf.Loading
{
    public sealed class LoaderViewModel : ViewModel
    {
        LoaderDisposable? _disposable;

        public Cursor Cursor
        {
            get => GetValue<Cursor>() ?? Cursors.Arrow;
            set => SetValue(value);
        }

        public IDisposable Load()
        {
            CreateDisposable();

            Cursor = Cursors.Wait;
            return _disposable ?? throw new InvalidOperationException();
        }

        void SetCursor(object? sender, Cursor cursor)
        {
            Cursor = cursor;
        }

        void CreateDisposable()
        {
            if (_disposable != null)
            {
                _disposable.Disposed -= SetCursor;
            }

            _disposable = new LoaderDisposable();
            _disposable.Disposed += SetCursor;
        }
    }
}