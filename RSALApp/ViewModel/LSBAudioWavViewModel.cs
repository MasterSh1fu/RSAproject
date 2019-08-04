using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSALApp.Model;
using RSALApp.NavigationServices;
using RSALApp.Commands;
using System.IO;

namespace RSALApp.ViewModel
{
    class LSBAudioWavViewModel : BaseModel
    {
        private string audioPath = "";
        private string hideAaudioPath = "";
        private string textInfoPath = "";
        private string result = "";
        private string message = "";
        private byte[] resultBytes = null;
        private string saveHideInfo = "";
        

        private RelayCommand hideInfoInAudioFile;
        private RelayCommand openAudioFile;
        private RelayCommand saveHideAudioFile;
        private RelayCommand openHideAudioFile;
        private RelayCommand getInfoFromFile;
        private RelayCommand openInfoFile;
        private RelayCommand saveResultInTxt;


 
        public string AudioPath
        {
            get { return audioPath; }
            set
            {
                audioPath = value;
                OnPropertyChanged("AudioPath");
            }
        }

        public string HideAaudioPath
        {
            get { return hideAaudioPath; }
            set
            {
                hideAaudioPath = value;
                OnPropertyChanged("HideAaudioPath");
            }
        }
       
        public string TextInfoPath
        {
            get { return textInfoPath; }
            set
            {
                textInfoPath = value;
                OnPropertyChanged("TextInfoPath");
            }
        }
        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }
       
       public RelayCommand OpenAudioFile
       {
           get
           {
               return openAudioFile ??
                 (openAudioFile = new RelayCommand(obj =>
                 {
                     bool flag = Navigation_Service.OpenFileDialog("Audio files(*.wav)|*.wav");
                     if (flag == true)
                     {
                         AudioPath = Navigation_Service.FilePath;
                     }
                 }));
           }
       }
       public RelayCommand SaveHideAudioFile
       {
           get
           {
               return saveHideAudioFile ??
                 (saveHideAudioFile = new RelayCommand(obj =>
                 {
                     if (resultBytes != null)
                     {
                         bool flag = Navigation_Service.SaveFileDialog("Audio files(*.wav)|*.wav");
                         if (flag == true)
                         {
                             saveHideInfo = Navigation_Service.FilePath;
                             File.WriteAllBytes(saveHideInfo, resultBytes);
                             Navigation_Service.ShowMessage("Файл сохранен!");
                         }
                     }
                     else
                     {
                         Navigation_Service.ShowMessage("Предворительно требуется осадить информацию в audio файл");
                     }
                 }));
           }
        } 
       public RelayCommand OpenHideAudioFile
       {
           get
           {
               return openHideAudioFile ??
                 (openHideAudioFile = new RelayCommand(obj =>
                 {
                     bool flag = Navigation_Service.OpenFileDialog("Audio files(*.wav)|*.wav");
                     if (flag == true)
                     {
                         HideAaudioPath = Navigation_Service.FilePath;
                       

                     }
                 }));
           }
       }
       public RelayCommand GetInfoFromFile
       {
           get
           {
               return getInfoFromFile ??
                 (getInfoFromFile = new RelayCommand(obj =>
                 {
                     try
                     {
                         string bufferResult = WavAudio.ExtractMessageFromAudio(HideAaudioPath);
                         string result1 = WavAudio.ConcertToXexSystemFromBinary(bufferResult);
                         Result = WavAudio.GetMessegeByBytesString(result1);
                     }
                     catch (Exception exp)
                     {
                         Navigation_Service.ShowMessage(exp.Message);
                     }
                 }));
           }
        }
       public RelayCommand OpenInfoFile
       {
           get
           {
               return openInfoFile ??
                 (openInfoFile = new RelayCommand(obj =>
                 {
                     bool flag = Navigation_Service.OpenFileDialog("Text files(*.txt)|*.txt");
                     if (flag == true)
                     {
                         TextInfoPath = Navigation_Service.FilePath;
                         using (StreamReader sr = new StreamReader(Navigation_Service.FilePath, System.Text.Encoding.Default))
                         {
                             message = sr.ReadToEnd();
                         }
                        
                     }
                 }));
           }
       }
       public RelayCommand SaveResultInTxt
       {
           get
           {
               return saveResultInTxt ??
                 (saveResultInTxt = new RelayCommand(obj =>
                 {
                     bool flag = Navigation_Service.SaveFileDialog("Text files(*.txt)|*.txt");
                     if (flag == true)
                     {
                         
                         using (StreamWriter sw = new StreamWriter(Navigation_Service.FilePath, false, System.Text.Encoding.Default))
                         {
                             sw.Write(Result);
                         }

                     }
                 }));
           }
       }

        public RelayCommand HideInfoInAudioFile
        {
            get
            {
                return hideInfoInAudioFile ??
                  (hideInfoInAudioFile = new RelayCommand(obj =>
                  {
                      try
                      {
                          string bytesMessage = WavAudio.GetStringBytesMessage(message);
                          char[] bitArray = WavAudio.ConvertMessageToArrayCharBits(bytesMessage);
                          resultBytes = WavAudio.HideInfoInAudioFile(bitArray, AudioPath);
                          if (resultBytes != null)
                          {
                              Navigation_Service.ShowMessage("Осождение информации в audio файл завершено успешно!");
                          }
                          else
                          {
                              Navigation_Service.ShowMessage("Осадить информацию не удалось!");
                          }
                      }
                      catch (Exception exp)
                      {
                          Navigation_Service.ShowMessage(exp.Message);
                      }
                  }));
            }
        }

        /*Команды для открытия новых окон*/

        private RelayCommand openWindowCommmand;
        private RelayCommand openWindowLSBCommmand;
        private RelayCommand aboutProgrammCommand;
        private RelayCommand openWindowMainCommmand;
      
        
        public RelayCommand OpenWindowCommmand
        {
            get
            {
                return openWindowCommmand ??
                  (openWindowCommmand = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("RSAEncryptionWindow", "LSBAudioWavViewModel");
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
                      Navigation_Service.ShowWindow("LSBForImageWindow", "LSBAudioWavViewModel");
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
                      Navigation_Service.ShowWindow("MainWindow", "LSBAudioWavViewModel");
                  }));
            }
        }
    }
}
