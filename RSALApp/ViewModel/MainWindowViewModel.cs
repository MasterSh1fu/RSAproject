using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSALApp.NavigationServices;
using RSALApp.Commands;


namespace RSALApp.ViewModel
{
    class MainWindowViewModel : BaseModel
    {
        private RelayCommand openWindowCommmand;
        private RelayCommand openWindowLSBCommmand;
        private RelayCommand aboutProgrammCommand;
        private RelayCommand openWindowLSBMethod;
        private RelayCommand openWindowLSBMethodForAudio;
        public RelayCommand OpenWindowCommmand
        {
            get
            {
                return openWindowCommmand ??
                  (openWindowCommmand = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("RSAEncryptionWindow", "MainWindowViewModel");
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
                      Navigation_Service.ShowWindow("LSBForImageWindow", "MainWindowViewModel");
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

        public RelayCommand OpenWindowLSBMethod
        {
            get
            {
                return openWindowLSBMethod ??
                  (openWindowLSBMethod = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("LSBMethodWindow", "MainWindowViewModel");
                  }));
            }
        }

        public RelayCommand OpenWindowLSBMethodForAudio
        {
            get
            {
                return openWindowLSBMethodForAudio ??
                  (openWindowLSBMethodForAudio = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("LSBAudioWav", "MainWindowViewModel");
                  }));
            }
        }
    }
}
