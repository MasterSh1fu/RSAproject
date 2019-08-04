using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSALApp.Commands;
using RSALApp.NavigationServices;

namespace RSALApp.ViewModel
{
    class LSBMethodWindowViewModel
    {
        private RelayCommand openWindowCommmand;
        private RelayCommand openWindowLSBCommmand;
        private RelayCommand aboutProgrammCommand;
        private RelayCommand openWindowMainCommmand;
        private RelayCommand openAudioWindow;
        public RelayCommand OpenAudioWindow
        {
            get
            {
                return openAudioWindow ??
                  (openAudioWindow = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("LSBAudioWav", "LSBMethodWindowViewModel");
                  }));
            }
        }
        public RelayCommand OpenWindowCommmand
        {
            get
            {
                return openWindowCommmand ??
                  (openWindowCommmand = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("RSAEncryptionWindow", "LSBMethodWindowViewModel");
                  }));
            }
        }
        public RelayCommand OpenWindowLSBCommmand
        {
            get
            {
                return openWindowLSBCommmand ??
                  (openWindowLSBCommmand = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("LSBForImageWindow", "LSBMethodWindowViewModel");
                  }));
            }
        }
        public RelayCommand AboutProgrammCommand
        {
            get
            {
                return aboutProgrammCommand ??
                  (aboutProgrammCommand = new RelayCommand(obj =>
                  {
                      string message = "Данное программное средство предназначено для шифрования и расшифрования текстовой информации с возможностью последующего осаждения шифртекста в картинку методом LSB. Программа является результатом курсового проекта по предмету Защита информации студента 3 курса факультета ИТ Романчика Алексей Валерьевича.";
                      Navigation_Service.ShowMessage(message);
                  }));
            }
        }
        public RelayCommand OpenMainWindowCommmand
        {
            get
            {
                return openWindowMainCommmand ??
                  (openWindowMainCommmand = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("MainWindow", "LSBMethodWindowViewModel");
                  }));
            }
        }


    }
}
