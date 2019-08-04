using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RSALApp.NavigationServices;
using RSALApp.ViewModel;


namespace RSALApp.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            Navigation_Service.WindowShow += ShowWindow;
        }

        private void ShowWindow(string windowName, string sender)
        {
            if (sender == "MainWindowViewModel")
            {
                if (windowName == "RSAEncryptionWindow")
                {
                    RSAEncryptionWindow win = new RSAEncryptionWindow();
                    win.Width = this.Width;
                    win.Height = this.Height;
                    win.Top = this.Top;
                    win.Left = this.Left;
                    win.WindowState = this.WindowState;
                    win.Show();
                    this.Close();
                    Navigation_Service.WindowShow -= ShowWindow;
                }
                if (windowName == "LSBForImageWindow")
                {
                    LSBForImageWindow win = new LSBForImageWindow();
                    win.Top = this.Top;
                    win.Left = this.Left;
                    win.Width = this.Width;
                    win.Height = this.Height;
                    win.WindowState = this.WindowState;
                    win.Show();
                    this.Close();
                    Navigation_Service.WindowShow -= ShowWindow;
                }
                if (windowName == "LSBAudioWav")
                {
                    LSBAudioWav win = new LSBAudioWav();
                    win.Top = this.Top;
                    win.Left = this.Left;
                    win.Width = this.Width;
                    win.Height = this.Height;
                    win.WindowState = this.WindowState;
                    win.Show();
                    this.Close();
                    Navigation_Service.WindowShow -= ShowWindow;
                }
                if (windowName == "LSBMethodWindow")
                {
                    LSBMethodWindow win = new LSBMethodWindow();
                    win.Top = this.Top;
                    win.Left = this.Left;
                    win.Width = this.Width;
                    win.Height = this.Height;
                    win.WindowState = this.WindowState;
                    win.Show();
                    this.Close();
                    Navigation_Service.WindowShow -= ShowWindow;
                }
            }
        }

    }
}
