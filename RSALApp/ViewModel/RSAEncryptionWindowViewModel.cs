using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSALApp.Model;
using RSALApp.Commands;
using RSALApp.NavigationServices;
using System.Numerics;
using System.IO;

namespace RSALApp.ViewModel
{
    class RSAEncryptionWindowViewModel : BaseModel
    {
        private List<int> lenghtKey =  new List<int>() {32,64,128,256,512,1024,2048,4096};
        private int currentLenghtKey = 32;
        private string strP = "";
        private string strQ = "";
        private BigInteger q;
        private BigInteger p;
        private BigInteger n;
        private BigInteger fn;
        private BigInteger e;
        private BigInteger d;
        private string publicKey;
        private string privateKey;
        private string testKey = "";
        private string message = "";
        private string code = "";
        private string messageCode = "";
        private string enCode = "";

        private RelayCommand generationQCommand;
        private RelayCommand generationPCommand;
        private RelayCommand generationKeyCommand;
        private RelayCommand testKeyCommand;
        private RelayCommand openFileCommand;
        private RelayCommand encodingRSA;
        private RelayCommand saveFileCommand;
        private RelayCommand seveKeysToFile;


        private RelayCommand decodingRSA;
        private RelayCommand openCodeFileCommand;
        private RelayCommand saveResultEncodingFileCommand;

        public List<int> LenghtKey
        {
            get { return lenghtKey; }
            set
            {
                lenghtKey = value;
                OnPropertyChanged("LenghtKey");
            }
        }

        public string MessageCode
        {
            get { return messageCode; }
            set
            {
                messageCode = value;
                OnPropertyChanged("MessageCode");
            }
        }

        public string EnCode
        {
            get { return enCode; }
            set
            {
                enCode = value;
                OnPropertyChanged("EnCode");
            }
        }


        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public string Code
        {
            get { return code; }
            set
            {
                code = value;
                OnPropertyChanged("Code");
            }
        }

        public string TestKey
        {
            get { return testKey; }
            set
            {
                testKey = value;
                OnPropertyChanged("TestKey");
            }
        }

        public string PublicKey
        {
            get { return publicKey; }
            set
            {
                publicKey = value;
                RSAcryptosystem.GetNEFromString(publicKey, out e, out n);
                OnPropertyChanged("PublicKey");
            }
        }
        public string PrivateKey
        {
            get { return privateKey; }
            set
            {
                privateKey = value;
                RSAcryptosystem.GetDEFromString(privateKey, out d, out n);
                OnPropertyChanged("PrivateKey");
            }
        }

        public int CurrentLenghtKey
        {
            get { return currentLenghtKey; }
            set
            {
                currentLenghtKey = value;
                OnPropertyChanged("CurrentLenghtKey");
            }
        }

        public string StrP
        {
            get { return strP; }
            set
            {
                strP = value;
                OnPropertyChanged("StrP");
            }
        }
        public string StrQ
        {
            get { return strQ; }
            set
            {
                strQ = value;
                OnPropertyChanged("StrQ");
            }
        }

        public RelayCommand GenerationQCommand
        {
            get
            {
                return generationQCommand ??
                  (generationQCommand = new RelayCommand(obj =>
                  {
                      
                      q = RSAcryptosystem.GenerationPrimeNumber((currentLenghtKey / 8)/4);
                      StrQ = RSAcryptosystem.СonversionTo16(q);
                  }));
            }
        }
        public RelayCommand GenerationPCommand
        {
            get
            {
                return generationPCommand ??
                  (generationPCommand = new RelayCommand(obj =>
                  {

                      p = RSAcryptosystem.GenerationPrimeNumber((currentLenghtKey / 8) / 4);
                      StrP = RSAcryptosystem.СonversionTo16(p);
                  }));
            }
        }

        public RelayCommand GenerationKeyCommand
        {
            get
            {
                return generationKeyCommand ??
                  (generationKeyCommand = new RelayCommand(obj =>
                  {
                      n = RSAcryptosystem.GenerationN(p, q);
                      fn = RSAcryptosystem.EulerFuncion(p, q);
                      d = RSAcryptosystem.GenerationD(fn, ((currentLenghtKey / 8) / 2) - 1);
                      e = RSAcryptosystem.GenerationE(d,fn);
                      PublicKey = RSAcryptosystem.ConversionPublicKeyToString(e,n);
                      PrivateKey = RSAcryptosystem.ConversionPrivateKeyToString(d,n);

                  }));
            }
        }

        public RelayCommand TestKeyCommand
        {
            get
            {
                return testKeyCommand ??
                  (testKeyCommand = new RelayCommand(obj =>
                  {
                      TestKey = RSAcryptosystem.TestKey(d, e, n);

                  }));
            }
        }

        public RelayCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ??
                  (openFileCommand = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.OpenFileDialog("Text files(*.txt)|*.txt");
                      if (flag == true)
                      {
                          using (StreamReader sr = new StreamReader(Navigation_Service.FilePath, System.Text.Encoding.Default))
                          {
                              Message = sr.ReadToEnd();
                          }
                      }

                  }));
            }
        }

        public RelayCommand EncodingRSA
        {
            get
            {
                return encodingRSA ??
                  (encodingRSA = new RelayCommand(obj =>
                  {
                      byte[] asciibytes = RSAcryptosystem.GetASCII(message);
                      Code = RSAcryptosystem.EncryptionMessageRSA(asciibytes, (currentLenghtKey / 16) - 1, e, n);
                  }));
            }
        }

        public RelayCommand SaveFileCommand
        {
            get
            {
                return saveFileCommand ??
                  (saveFileCommand = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.SaveFileDialog("Text files(*.txt)|*.txt");
                      if (flag == true)
                      {
                          using (StreamWriter sw = new StreamWriter(Navigation_Service.FilePath, false, System.Text.Encoding.Default))
                          {
                              sw.Write(Code);
                          }
                      }

                  }));
            }
        }


        public RelayCommand OpenCodeFileCommand
        {
            get
            {
                return openCodeFileCommand ??
                  (openCodeFileCommand = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.OpenFileDialog("Text files(*.txt)|*.txt");
                      if (flag == true)
                      {
                          using (StreamReader sr = new StreamReader(Navigation_Service.FilePath, System.Text.Encoding.Default))
                          {
                              MessageCode = sr.ReadToEnd();
                          }
                      }

                  }));
            }
        }

        public RelayCommand DecodingRSA
        {
            get
            {
                return decodingRSA ??
                  (decodingRSA = new RelayCommand(obj =>
                  {
                      try
                      {
                          EnCode = RSAcryptosystem.DecryptionMessageRSA(MessageCode, (currentLenghtKey / 8), d, n);
                      }
                      catch (Exception exp)
                      {
                          Navigation_Service.ShowMessage(exp.Message);
                      }
                  }));
            }
        }


        public RelayCommand SaveResultEncodingFileCommand
        {
            get
            {
                return saveResultEncodingFileCommand ??
                  (saveResultEncodingFileCommand = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.SaveFileDialog("Text files(*.txt)|*.txt");
                      if (flag == true)
                      {
                          using (StreamWriter sw = new StreamWriter(Navigation_Service.FilePath, false, System.Text.Encoding.Default))
                          {
                              sw.Write(enCode);
                          }
                      }

                  }));
            }
        }

        public RelayCommand SeveKeysToFile
        {
            get
            {
                return seveKeysToFile ??
                  (seveKeysToFile = new RelayCommand(obj =>
                  {
                      bool flag = Navigation_Service.SaveFileDialog("Text files(*.txt)|*.txt");
                      if (flag == true)
                      {
                          using (StreamWriter sw = new StreamWriter(Navigation_Service.FilePath, false, System.Text.Encoding.Default))
                          {
                              sw.WriteLine("Public Key: " + publicKey);
                              sw.WriteLine("Lenght Public Key: " + currentLenghtKey.ToString());
                              sw.WriteLine("Private Key: " + privateKey);
                              sw.WriteLine("Lenght Private Key: " + currentLenghtKey.ToString());
                          }
                      }

                  }));
            }
        }


        /*Команды меню по открытию окон*/


        private RelayCommand openWindowCommmand;
        private RelayCommand openWindowLSBCommmandFromRsa;
        private RelayCommand aboutProgrammCommandFromRsa;
        private RelayCommand openWindowLSBMethodForAudio;
        public RelayCommand OpenMainWindowCommmand
        {
            get
            {
                return openWindowCommmand ??
                  (openWindowCommmand = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("MainWindow", "RSAEncryptionWindowViewModel");
                  }));
            }
        }
        public RelayCommand OpenWindowLSBCommmandFromRsa
        {
            get
            {
                return openWindowLSBCommmandFromRsa ??
                  (openWindowLSBCommmandFromRsa = new RelayCommand(obj =>
                  {
                      Navigation_Service.ShowWindow("LSBForImageWindow", "RSAEncryptionWindowViewModel");
                  }));
            }
        }
        public RelayCommand AboutProgrammCommandFromRsa
        {
            get
            {
                return aboutProgrammCommandFromRsa ??
                  (aboutProgrammCommandFromRsa = new RelayCommand(obj =>
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
                      Navigation_Service.ShowWindow("LSBAudioWav", "RSAEncryptionWindowViewModel");
                  }));
            }
        }

    }
}
