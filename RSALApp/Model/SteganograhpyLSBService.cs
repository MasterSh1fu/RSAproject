using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;


namespace RSALApp.Model
{
    public static class SteganograhpyLSBService
    {
        public static string loadedTrueImagePath;
        public static Image loadedTrueImage;
        public static int width;
        public static int height;
        public static int canSave;

        public static char[] massive16bytes = new char[16] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static string[] massive2bytes = new string[16] { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };



        public static string GetMessegeByBytesString(string bytes)
        {
            string message = "";
            string byffer = "";
            byte[] bytesBuffer = new byte[bytes.Length / 3];
            int index = 0;
            int j = 0;
            while (index <= bytes.Length - 3)
            {
                byffer = bytes.Substring(index, 3);
                bytesBuffer[j] = byte.Parse(byffer);
                index += 3;
                j++;
            }
            message = Encoding.GetEncoding(1251).GetString(bytesBuffer);
            //message = ASCIIEncoding.ASCII.GetString(bytesBuffer);

            return message;
        }
        public static string GetStringBytesMessage(string message)
        {
            string result = "";
            string buffer = "";
            byte[] bytes = Encoding.GetEncoding(1251).GetBytes(message);
            //byte[] bytes = ASCIIEncoding.ASCII.GetBytes(message);
            for (int i = 0; i < bytes.Length; i++)
            {
                buffer = bytes[i] + "";
                while (buffer.Length < 3)
                {
                    buffer = "0" + buffer;
                }
                result += buffer;
            }
            return result;
        }

        public static void LoadImag(string pathImg)
        {
            loadedTrueImagePath = pathImg;
            loadedTrueImage = Image.FromFile(pathImg);
            Bitmap loadedTrueBitmap = new Bitmap(loadedTrueImage);
            width = loadedTrueImage.Width;
            height = loadedTrueImage.Height;
            canSave = (width * height * 3) / 8 / 1024;
        }

        public static char[] GetStartLenghtBits(int lenght)
        {
            string lenghtStr = lenght.ToString();
            if (lenghtStr.Length < 9)
            {
                while (lenghtStr.Length < 9)
                {
                    lenghtStr = "0" + lenghtStr;
                }

            }
            char[] result = ConvertMessageToArrayCharBits(lenghtStr);
            return result;
        }


        public static char[] ConvertMessageToArrayCharBits(string message)
        {

            string buffer = "";
            char[] bytes16x = message.ToCharArray();
            for (int i = 0; i < bytes16x.Length; i++)
            {
                for (int j = 0; j < massive16bytes.Length; j++)
                {
                    if (bytes16x[i] == massive16bytes[j])
                    {
                        buffer += massive2bytes[j];
                        break;
                    }
                }
            }
            char[] result = buffer.ToCharArray();
            return result;
        }
        
        public static Image HideInfoToImage(char[] message, int flag, int lenght)
        {
            Bitmap afterEncrypt = new Bitmap(loadedTrueImage);
            Bitmap loadedTrueBitmap = new Bitmap(loadedTrueImage);
            byte bufferR;
            byte bufferG;
            byte bufferB;
            Color bufferCon;
            int t = 0;
            char[] bitsLenght = GetStartLenghtBits(lenght);
            int l = 0;
            byte w = 254;
            for (int i = 0; i < 12; i++)
            {
                bufferR = loadedTrueBitmap.GetPixel(0, i).R;
                bufferG = loadedTrueBitmap.GetPixel(0, i).G;
                bufferB = loadedTrueBitmap.GetPixel(0, i).B;

                bufferR = (byte)((bufferR & w) | byte.Parse(bitsLenght[l] + ""));
                bufferG = (byte)((bufferG & w) | byte.Parse(bitsLenght[l + 1] + ""));
                bufferB = (byte)((bufferB & w) | byte.Parse(bitsLenght[l + 2] + ""));

                bufferCon = Color.FromArgb(bufferR, bufferG, bufferB);

                afterEncrypt.SetPixel(0, i, bufferCon);
                l += 3;

            }



            w = 254;
            int ost = lenght % 3;
            int indexJ = 0;
            int indexI = 0;
            for (int i = 0; i < width && (t < (message.Length - (3 * flag))); i++)
            {
                for (int j = 12; (j < height) && (t < (message.Length - (3 * flag))); j++)
                {
                    bufferR = loadedTrueBitmap.GetPixel(i, j).R;
                    bufferG = loadedTrueBitmap.GetPixel(i, j).G;
                    bufferB = loadedTrueBitmap.GetPixel(i, j).B;

                    bufferR = (byte)((bufferR & w) | byte.Parse(message[t] + ""));
                    bufferG = (byte)((bufferG & w) | byte.Parse(message[t + 1] + ""));
                    bufferB = (byte)((bufferB & w) | byte.Parse(message[t + 2] + ""));

                    bufferCon = Color.FromArgb(bufferR, bufferG, bufferB);

                    afterEncrypt.SetPixel(i, j, bufferCon);
                    t += 3 * flag;
                    indexJ = j;
                    indexI = i;
                }
            }
            if (ost == 1)
            {
                bufferR = loadedTrueBitmap.GetPixel(indexI, indexJ).R;
                bufferG = loadedTrueBitmap.GetPixel(indexI, indexJ).G;
                bufferB = loadedTrueBitmap.GetPixel(indexI, indexJ).B;
                bufferR = (byte)((bufferR & w) | byte.Parse(message[message.Length - 1] + ""));
                bufferCon = Color.FromArgb(bufferR, bufferG, bufferB);
                afterEncrypt.SetPixel(indexI, indexJ, bufferCon);

            }
            if (ost == 2)
            {
                bufferR = loadedTrueBitmap.GetPixel(indexI, indexJ).R;
                bufferG = loadedTrueBitmap.GetPixel(indexI, indexJ).G;
                bufferB = loadedTrueBitmap.GetPixel(indexI, indexJ).B;
                bufferR = (byte)((bufferR & w) | byte.Parse(message[message.Length - 2] + ""));
                bufferG = (byte)((bufferG & w) | byte.Parse(message[message.Length - 1] + ""));
                bufferCon = Color.FromArgb(bufferR, bufferG, bufferB);
                afterEncrypt.SetPixel(indexI, indexJ, bufferCon);
            }




            Image result = (Image)afterEncrypt;

            return result;

        }


        public static string GetInfoFromImg(string path, int flag)
        {
            string result = "";
            string buffer = "";
            Image codeImg = Image.FromFile(path);
            Bitmap loadedTrueBitmap = new Bitmap(codeImg);
            byte bufferR;
            byte bufferG;
            byte bufferB;
            byte bufferNum = 1;
            for (int i = 0; i < 12; i++)
            {
                bufferR = loadedTrueBitmap.GetPixel(0, i).R;
                bufferG = loadedTrueBitmap.GetPixel(0, i).G;
                bufferB = loadedTrueBitmap.GetPixel(0, i).B;

                bufferR = (byte)(bufferR & bufferNum);
                bufferG = (byte)(bufferG & bufferNum);
                bufferB = (byte)(bufferB & bufferNum);

                buffer += bufferR.ToString();
                buffer += bufferG.ToString();
                buffer += bufferB.ToString();
            }
            int lenght = int.Parse(ConcertToXexSystemFromBinary(buffer));
            lenght = (lenght * 4);
            Console.WriteLine(lenght);

            string bufferResul = "";
            int index = 0;

            bufferNum = 1;
            int ost = lenght % 3;
            int indexJ = 0;
            int indexI = 0;
            for (int i = 0; i < width && index < lenght - (3 * flag) - ost; i++)
            {
                for (int j = 12; j < height && index < lenght - (3 * flag) - ost; j++)
                {
                    bufferR = loadedTrueBitmap.GetPixel(i, j).R;
                    bufferG = loadedTrueBitmap.GetPixel(i, j).G;
                    bufferB = loadedTrueBitmap.GetPixel(i, j).B;

                    bufferR = (byte)(bufferR & bufferNum);
                    bufferG = (byte)(bufferG & bufferNum);
                    bufferB = (byte)(bufferB & bufferNum);

                    bufferResul += bufferR.ToString();
                    bufferResul += bufferG.ToString();
                    bufferResul += bufferB.ToString();

                    index += (3 * flag);
                    indexJ = j;
                    indexI = i;
                }
            }
            if (ost == 1)
            {
                bufferR = loadedTrueBitmap.GetPixel(indexI, indexJ).R;
                bufferR = (byte)(bufferR & bufferNum);
                bufferResul += bufferR.ToString();

            }
            if (ost == 2)
            {
                bufferR = loadedTrueBitmap.GetPixel(indexI, indexJ).R;
                bufferG = loadedTrueBitmap.GetPixel(indexI, indexJ).G;
                bufferR = (byte)(bufferR & bufferNum);
                bufferG = (byte)(bufferG & bufferNum);
                bufferResul += bufferR.ToString();
                bufferResul += bufferG.ToString();
            }


            string bufferResult = ConcertToXexSystemFromBinary(bufferResul);
            result = GetMessegeByBytesString(bufferResult);
            return result;
        }

        public static string ConcertToXexSystemFromBinary(string str)
        {
    
            
            string result = "";
            string buffer = "";

            for (int i = 0; i <= str.Length - 4; i += 4)
            {
                buffer = str.Substring(i, 4);
                for (int j = 0; j < 16; j++)
                {
                    if (buffer == massive2bytes[j])
                    {
                        result += massive16bytes[j];

                        break;
                    }
                }
            }
            return result;
        }

    }
}
