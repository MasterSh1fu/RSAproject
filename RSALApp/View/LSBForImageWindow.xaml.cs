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
using System.Windows.Shapes;
using RSALApp.ViewModel;
using RSALApp.NavigationServices;

namespace RSALApp.View
{
    /// <summary>
    /// Логика взаимодействия для LSBForImageWindow.xaml
    /// </summary>
    public partial class LSBForImageWindow : Window
    {
        public LSBForImageWindow()
        {
            InitializeComponent();
            DataContext = new LSBForImageWindowViewModel();
            Navigation_Service.WindowShow += ShowWindow;
        }
        private void ShowWindow(string windowName, string sender)
        {
            if (sender == "LSBForImageWindowViewModel")
            {

                if (windowName == "RSAEncryptionWindow")
                {
                    RSAEncryptionWindow win = new RSAEncryptionWindow();
                    win.Top = this.Top;
                    win.Left = this.Left;
                    win.Width = this.Width;
                    win.Height = this.Height;
                    win.WindowState = this.WindowState;
                    win.Show();
                    this.Close();
                    Navigation_Service.WindowShow -= ShowWindow;
                }
                if (windowName == "MainWindow")
                {
                    MainWindow win = new MainWindow();
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
            }
        }
    }
}
