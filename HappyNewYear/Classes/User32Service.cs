using HappyNewYear.Interfaces;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace HappyNewYear.Classes
{
    internal class User32Service : IUser32Service
    {
        private readonly Window _window;

        public User32Service(Window window) => this._window = window;

        #region Alt + Tab
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr window, int index, int value);

        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr window, int index);

        private void HideFromAltTab(IntPtr Handle)
        {
            User32Service.SetWindowLong(Handle, Constants.GWL_EXSTYLE, User32Service.GetWindowLong(Handle, Constants.GWL_EXSTYLE) | Constants.WS_EX_TOOLWINDOW);
        }

        void IUser32Service.HideFromAltTab() => this.HideFromAltTab(Handle);

        private IntPtr Handle => new WindowInteropHelper(this._window).Handle;
        #endregion

        #region back window
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(int hWnd, int hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        void IUser32Service.ShoveToBackground() => SetWindowPos((int)this.Handle, Constants.HWND_BOTTOM, 0, 0, 0, 0, Constants.SWP_NOMOVE | Constants.SWP_NOSIZE | Constants.SWP_SHOWWINDOW);
        #endregion
    }
}
