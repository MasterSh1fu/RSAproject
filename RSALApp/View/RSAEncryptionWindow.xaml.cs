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
using RSALApp.NavigationServices;
using RSALApp.ViewModel;

namespace RSALApp.View
{
    /// <summary>
    /// Логика взаимодействия для RSAEncryptionWindow.xaml
    /// </summary>
    public partial class RSAEncryptionWindow : Window
    {
        public RSAEncryptionWindow()
        {
            InitializeComponent();
            Navigation_Service.WindowShow += ShowWindow;
        }

        private void ShowWindow(string windowName, string sender)
        {
            if (sender == "RSAEncryptionWindowViewModel")
            {
                
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
