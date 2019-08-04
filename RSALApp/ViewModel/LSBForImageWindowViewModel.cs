using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSALApp.Commands;
using RSALApp.NavigationServices;
using System.Drawing;
using System.IO;
using RSALApp.Model;
using System.Windows.Media.Imaging;

namespace RSALApp.ViewModel
{
    class LSBForImageWindowViewModel : BaseModel
    {
        private int size = 0;
        private int height = 0;
        private int width = 0;
        private int canSave = 0;
        private string imgPath = "Images/downloads.png";
        private string hideImgPath = "Images/downloads.png";
        private string message = "";
        private string txtPath = "";
        private int txtSize = 0;
        private Image resultImg;
        private string messageResult = "";
 
        private RelayCommand openFileImg;
        private RelayCommand openTxtFileCommand;
        private RelayCommand hideInfoToImgCommand;
        private RelayCommand saveResultCommand;
        private RelayCommand openHideImg;
        private RelayCommand extractInformFromImage;
        private RelayCommand saveResultTxtCommand;


        public string MessageResult
        {
            get { return messageResult; }
            set
            {
                messageResult = value;
                OnPropertyChanged("MessageResult");
            }
        }

        public string TxtPath
        {
            get { return txtPath; }
            set
            {
                txtPath = value;
                OnPropertyChanged("TxtPath");
            }
        }
        public int TxtSize
        {
            get { return txtSize; }
            set
            {
                txtSize = value;
                OnPropertyChanged("TxtSize");
            }
        }
        public string ImgPath
        {
            get { return imgPath; }
            set
            {
                imgPath = value;
                OnPropertyChanged("ImgPath");
            }
        }
        public string HideImgPath
        {
            get { return hideImgPath; }
            set
            {
                hideImgPath = value;
                OnPropertyChanged("HideImgPath");
            }
        }
        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                OnPropertyChanged("Size");
            }
        }
        public int Height
        {
            get { return height; }
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }
        public int Width
        {
            get { return width; }
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }
        public int CanSave
        {
            get { return canSave; }
            set
            {
                canSave = value;
                OnPropertyChanged("CanSave");
            }
        }


        public RelayCommand OpenFileImg
        {
            get
            {
                return openFileImg ??
                  (openFileImg = new RelayCommand(obj =>
                  {
                      if (Navigation_Service.OpenFileDialog("Image files(*.bmp)|*.bmp"))
                      {
                          ImgPath = Navigation_Service.FilePath;
                          SteganograhpyLSBService.LoadImag(ImgPath);
                          FileInfo imginf = new FileInfo(ImgPath);
                          Size = (int)(imginf.Length / 1024);
                          Width = SteganograhpyLSBService.width;
                          Height = SteganograhpyLSBService.height;
                          CanSave = SteganograhpyLSBService.canSave;
                      }

                  }));
            }
        }
        public RelayCommand OpenTxtFileCommand
        {
            get
            {
                return openTxtFileCommand ??
                  (openTxtFileCommand = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.OpenFileDialog("Text files(*.txt)|*.txt");
                      if (flag == true)
                      {
                          using (StreamReader sr = new StreamReader(Navigation_Service.FilePath, System.Text.Encoding.Default))
                          {
                              message = sr.ReadToEnd();
                          }
                          message = message + "0";
                          message = SteganograhpyLSBService.GetStringBytesMessage(message);
                          TxtPath = Navigation_Service.FilePath;
                          FileInfo imginf = new FileInfo(Navigation_Service.FilePath);
                          TxtSize = (int)(imginf.Length / 1024);
                      }

                  }));
            }
        }

        public RelayCommand HideInfoToImgCommand
        {
            get
            {
                return hideInfoToImgCommand ??
                  (hideInfoToImgCommand = new RelayCommand(obj =>
                  {
                      if ((canSave > txtSize)&& txtPath != "" && imgPath != "Images/downloads.png")
                      {
                          try
                          {
                              resultImg = SteganograhpyLSBService.HideInfoToImage(SteganograhpyLSBService.ConvertMessageToArrayCharBits(message), 1, message.Length);
                              Navigation_Service.ShowMessage("Осаждение информации в картинку прошло успешно!");
                          }
                          catch (Exception exp)
                          {
                              Navigation_Service.ShowMessage(exp.Message);
                          }
                      }

                  }));
            }
        }

        public RelayCommand SaveResultCommand
        {
            get
            {
                return saveResultCommand ??
                  (saveResultCommand = new RelayCommand(obj =>
                  {
                      bool flag= Navigation_Service.SaveFileDialog("Image files(*.bmp)|*.bmp");
                      if (flag && resultImg != null)
                      {
                          try
                          {
                              resultImg.Save(Navigation_Service.FilePath);
                          }
                          catch (Exception exp)
                          {
                              Navigation_Service.ShowMessage(exp.Message);
                          }
                      }

                  }));
            }
        }

        public RelayCommand OpenHideImg
        {
            get
            {
                return openHideImg ??
                  (openHideImg = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.OpenFileDialog("Image files(*.bmp)|*.bmp");
                      if (flag)
                      {
                          HideImgPath = Navigation_Service.FilePath;
                          SteganograhpyLSBService.LoadImag(Navigation_Service.FilePath);
                      }

                  }));
            }
        }
        public RelayCommand ExtractInformFromImage
        {
            get
            {
                return extractInformFromImage ??
                  (extractInformFromImage = new RelayCommand(obj =>
                  {
                      try
                      {
                          MessageResult = SteganograhpyLSBService.GetInfoFromImg(SteganograhpyLSBService.loadedTrueImagePath, 1);
                          Navigation_Service.ShowMessage("Извлечение информации прошло успешно!");
                      }
                      catch (Exception exp)
                      {
                          Navigation_Service.ShowMessage(exp.Message);
                      }
                  }));
            }
        }

        public RelayCommand SaveResultTxtCommand
        {
            get
            {
                return saveResultTxtCommand ??
                  (saveResultTxtCommand = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.SaveFileDialog("Text files(*.txt)|*.txt");
                      if (flag)
                      {
                          using (StreamWriter sw = new StreamWriter(Navigation_Service.FilePath, false, System.Text.Encoding.Default))
                          {
                              sw.Write(MessageResult);
                          }
                      }


                  }));
            }
        }

        /*Команды для открытия новых окон*/

        private RelayCommand openWindowCommmand;
        private RelayCommand openWindowRSACommmandFromLSB;
        private RelayCommand aboutProgrammCommandFromLSB;
        private RelayCommand openWindowLSBMethodForAudio;
        public RelayCommand OpenMainWindowCommmand
        {
            get
            {
                return openWindowCommmand ??
                  (openWindowCommmand = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("MainWindow", "LSBForImageWindowViewModel");
                  }));
            }
        }
        public RelayCommand OpenWindowRSACommmandFromLSB
        {
            get
            {
                return openWindowRSACommmandFromLSB ??
                  (openWindowRSACommmandFromLSB = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("RSAEncryptionWindow", "LSBForImageWindowViewModel");
                  }));
            }
        }
        public RelayCommand AboutProgrammCommandFromLSB
        {
            get
            {
                return aboutProgrammCommandFromLSB ??
                  (aboutProgrammCommandFromLSB = new RelayCommand(obj =>
                  {
                      string message = "Данное программное средство предназначено для шифрования и расшифрования текстовой информации с возможностью последующего осаждения шифртекста в картинку методом LSB. Программа является результатом курсового проекта по предмету Защита информации студента 3 курса факультета ИТ Романчика Алексей Валерьевича.";
                      Navigation_Service.ShowMessage(message);
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
                      Navigation_Service.ShowWindow("LSBAudioWav", "LSBForImageWindowViewModel");
                  }));
            }
        }
    }
}
