using HappyNewYear.Classes;
using HappyNewYear.Interfaces;
using System;
using System.Windows;
using System.Windows.Input;

namespace HappyNewYear
{
    public partial class MainWindow : Window
    {
        private readonly IUser32Service _user32Service;
        private Point _point;

        public MainWindow()
        {
            InitializeComponent();
            this._user32Service = new User32Service(this);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                e.Handled = true;
                this.Close();
            }
            else
            {
                this._point = e.GetPosition(null);
            }
        }

        private void Image_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point cur = e.GetPosition(null);

                this.Left += cur.X - this._point.X;
                this.Top += cur.Y - this._point.Y;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) => this._user32Service.HideFromAltTab();

        private void Window_Activated(object sender, EventArgs e) => this._user32Service.ShoveToBackground();
    }
}
