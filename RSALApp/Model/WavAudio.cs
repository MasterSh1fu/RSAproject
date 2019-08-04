using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSALApp.Model
{
    class WavAudio
    {
        public static char[] massive16bytes = new char[16] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
        public static string[] massive2bytes = new string[16] { "0000", "0001", "0010", "0011", "0100", "0101", "0110", "0111", "1000", "1001", "1010", "1011", "1100", "1101", "1110", "1111" };

        public static char[] GetLenghtMessage(string lenght)
        {
            while (lenght.Length < 12)
            {
                lenght = "0" + lenght;
            }
            char[] result = ConvertMessageToArrayCharBits(lenght);
            return result;
        }

        public static byte[] HideInfoInAudioFile(char[] bytes, string filepath)
        {
            byte[] bytesFile = File.ReadAllBytes(filepath);

            byte[] format = new byte[4] { bytesFile[8], bytesFile[9], bytesFile[10], bytesFile[11] };
            string formatStr = ASCIIEncoding.ASCII.GetString(format);

            if (formatStr != "WAVE")
            {

            }

            int lenghtMessage = bytes.Length;

            int dataPositionIndex = 0;
            string dataTag = "";
            byte[] bufferBytes = new byte[4];
            while (dataTag != "data" && dataPositionIndex < bytesFile.Length - 10)
            {
                bufferBytes[0] = bytesFile[dataPositionIndex];
                bufferBytes[1] = bytesFile[dataPositionIndex + 1];
                bufferBytes[2] = bytesFile[dataPositionIndex + 2];
                bufferBytes[3] = bytesFile[dataPositionIndex + 3];

                dataTag = ASCIIEncoding.ASCII.GetString(bufferBytes);
                dataPositionIndex++;
                Console.WriteLine(dataPositionIndex);
            }
            dataPositionIndex += 4;
            char[] lenghtofMessage = GetLenghtMessage(lenghtMessage.ToString());//длинна сообщения это 12 значное число что в моей системе 48 bit
            int j = 0;
            for (int i = dataPositionIndex; j < 48; i++)
            {
                bytesFile[i] = (byte)((bytesFile[i] & 254) | byte.Parse(lenghtofMessage[j].ToString()));
                j++;
            }
            dataPositionIndex += 48;
            j = 0;
            for (int i = dataPositionIndex; (i < dataPositionIndex + lenghtMessage) && (i < bytesFile.Length); i++)
            {
                bytesFile[i] = (byte)((bytesFile[i] & 254) | byte.Parse(bytes[j].ToString()));
                j++;
            }



            return bytesFile;
        }

        public static string ExtractMessageFromAudio(string filePath)
        {
            byte[] bytesFile = File.ReadAllBytes(filePath);

            int dataPositionIndex = 0;
            string dataTag = "";
            byte[] bufferBytes = new byte[4];
            while (dataTag != "data" && dataPositionIndex < bytesFile.Length - 10)
            {
                bufferBytes[0] = bytesFile[dataPositionIndex];
                bufferBytes[1] = bytesFile[dataPositionIndex + 1];
                bufferBytes[2] = bytesFile[dataPositionIndex + 2];
                bufferBytes[3] = bytesFile[dataPositionIndex + 3];
                dataTag = ASCIIEncoding.ASCII.GetString(bufferBytes);
                dataPositionIndex++;
                Console.WriteLine(dataPositionIndex);
            }
            dataPositionIndex += 4;
            int j = 0;
            string bufferLenght = "";
            byte bufferByte;
            for (int i = dataPositionIndex; j < 48; i++)
            {
                bufferByte = (byte)(bytesFile[i] & 1);
                bufferLenght += bufferByte + "";
                j++;
            }
            j = 0;
            int lenghtMessage = int.Parse(ConcertToXexSystemFromBinary(bufferLenght));
            dataPositionIndex += 48;
            string bufferResult = "";
            for (int i = dataPositionIndex; j < lenghtMessage; i++)
            {
                bufferByte = (byte)(bytesFile[i] & 1);
                bufferResult += bufferByte + "";
                j++;
            }

            return bufferResult;
        }

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
            //message = ASCIIEncoding.ASCII.GetString(bytesBuffer);
            message = Encoding.GetEncoding(1251).GetString(bytesBuffer);

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
