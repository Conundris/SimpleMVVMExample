﻿using System;
using System.Threading.Tasks;
using System.Windows;

namespace AsyncShowDialog
{
    public static class AsyncWindowUtilites
    {
        public static Task<bool?> ShowDialogAsync(this Window self)
        {
            if (self == null) throw new ArgumentNullException(nameof(self));

            var completion = new TaskCompletionSource<bool?>();
            self.Dispatcher.BeginInvoke(new Action(() => completion.SetResult(self.ShowDialog())));

            return completion.Task;
        }
    }
}