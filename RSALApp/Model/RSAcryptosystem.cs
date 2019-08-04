using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Security.Cryptography;

namespace RSALApp.Model
{
    static class RSAcryptosystem
    {

        public static BigInteger GenerationPrimeNumber(int numB)
        {
            Random rnd = new Random();
            byte[] bytes = new byte[numB + 1];
            rnd.NextBytes(bytes);
            byte num128 = 128;
            bytes[bytes.Length - 2] = (byte)(bytes[bytes.Length - 2] | num128);
            bytes[bytes.Length - 1] = 0;
            BigInteger primeNumber = new BigInteger(bytes);
            while (true)
            {
                if (MillerRabinTest(primeNumber, 100))
                {
                    break;
                }
                else
                {
                    primeNumber++;
                }
            }
            return primeNumber;
        }

        public static BigInteger GenerationN(BigInteger p, BigInteger q)
        {
            return BigInteger.Multiply(p, q);
        }

        public static BigInteger EncryptionRSA(BigInteger e, BigInteger n, BigInteger message)
        {
            BigInteger resul = BigInteger.ModPow(message, e, n);
            return resul;
        }

        public static BigInteger DecryptionRSA(BigInteger d, BigInteger n, BigInteger code)
        {
            BigInteger resul = BigInteger.ModPow(code, d, n);
            return resul;
        }

        public static BigInteger EulerFuncion(BigInteger p, BigInteger q)
        {
            return BigInteger.Multiply(p - 1, q - 1);
        }

        public static BigInteger GenerationD(BigInteger fe, int numB)
        {
            BigInteger d = GenerationPrimeNumber(numB);
            while (d > fe)
            {
                d = GenerationPrimeNumber(numB);
            }
            return d;
        }

        public static BigInteger GenerationE(BigInteger d, BigInteger fe)
        {
            BigInteger e = ReverseElement(d, fe);
            return e;
        }

        public static bool MillerRabinTest(BigInteger n, int k)
        {
            // если n == 2 или n == 3 - эти числа простые, возвращаем true
            if (n == 2 || n == 3)
                return true;

            // если n < 2 или n четное - возвращаем false
            if (n < 2 || n % 2 == 0)
                return false;

            // представим n − 1 в виде (2^s)·t, где t нечётно, это можно сделать последовательным делением n - 1 на 2
            BigInteger t = n - 1;

            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            // повторить k раз
            for (int i = 0; i < k; i++)
            {

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                byte[] _a = new byte[n.ToByteArray().LongLength];

                BigInteger a;

                do
                {
                    rng.GetBytes(_a);
                    a = new BigInteger(_a);
                }
                while (a < 2 || a >= n - 2);

                // x ← a^t mod n, вычислим с помощью возведения в степень по модулю
                BigInteger x = BigInteger.ModPow(a, t, n);

                // если x == 1 или x == n − 1, то перейти на следующую итерацию цикла
                if (x == 1 || x == n - 1)
                    continue;

                // повторить s − 1 раз
                for (int r = 1; r < s; r++)
                {
                    // x ← x^2 mod n
                    x = BigInteger.ModPow(x, 2, n);

                    // если x == 1, то вернуть "составное"
                    if (x == 1)
                        return false;

                    // если x == n − 1, то перейти на следующую итерацию внешнего цикла
                    if (x == n - 1)
                        break;
                }

                if (x != n - 1)
                    return false;
            }

            // вернуть "вероятно простое"
            return true;
        }

        public static BigInteger ReverseElement(BigInteger a, BigInteger m)
        {

            BigInteger x, y;
            BigInteger g = RAE(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();

            return (x % m + m) % m;
        }

        public static BigInteger RAE(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            if (a == 0)
            {
                x = 0;
                y = 1;
                return b;
            }
            BigInteger x1, y1;
            BigInteger d = RAE(b % a, a, out x1, out y1);
            x = y1 - (b / a) * x1;
            y = x1;
            return d;
        }
        public static string СonversionTo16(BigInteger num)
        {
            string result = num.ToString("X");
            return result;
        }

        public static BigInteger ConversionToBigInteger(string num)
        {
            BigInteger result = BigInteger.Parse(num, System.Globalization.NumberStyles.AllowHexSpecifier);
            return result;
        }

        public static string ConversionPublicKeyToString(BigInteger e, BigInteger n)
        {
            string eStr = e.ToString("X");
            string nStr = n.ToString("X");
            string result = eStr + "00000000" + nStr;
            return result;
        }
        public static string ConversionPrivateKeyToString(BigInteger d, BigInteger n)
        {
            string dStr = d.ToString("X");
            string nStr = n.ToString("X");
            string result = dStr + "00000000" + nStr;
            return result;
        }
        public static void GetNEFromString(string key, out BigInteger e, out BigInteger n)
        {
            int index = 0;
            string eStr = "";
            string nStr = "";
            string substring = "00000000";
            index = key.IndexOf(substring);
            nStr = key.Substring(index + 8, (key.Length - (index + 8)));
            eStr = key.Substring(0, index);
            e = BigInteger.Parse(eStr, System.Globalization.NumberStyles.AllowHexSpecifier);
            n = BigInteger.Parse(nStr, System.Globalization.NumberStyles.AllowHexSpecifier);
        }
        public static void GetDEFromString(string key, out BigInteger d, out BigInteger n)
        {
            int index = 0;
            string dStr = "";
            string nStr = "";
            string substring = "00000000";
            index = key.IndexOf(substring);
            nStr = key.Substring(index + 8, (key.Length - (index + 8)));
            dStr = key.Substring(0, index);
            d = BigInteger.Parse(dStr, System.Globalization.NumberStyles.AllowHexSpecifier);
            n = BigInteger.Parse(nStr, System.Globalization.NumberStyles.AllowHexSpecifier);
        }

        public static string TestKey(BigInteger d, BigInteger e, BigInteger n)
        {
            BigInteger code = EncryptionRSA(e, n, 1);
            BigInteger encode = DecryptionRSA(d, n, code);
            if (encode == 1)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }
        public static byte[] GetASCII(string str)
        {
            byte[] AscIIBytes = Encoding.GetEncoding(1251).GetBytes(str);
            //byte[] AscIIBytes = Encoding.ASCII.GetBytes(str);
            return AscIIBytes;
        }
        public static string GetStringFromASCII(byte[] AscIIBytes)
        {
            string str = Encoding.GetEncoding(1251).GetString(AscIIBytes);
            return str;
        }

        public static string EncryptionMessageRSA(byte[] message, int lenghtKey, BigInteger e, BigInteger n)
        {
            string result = "";
            byte[] buffer = new byte[lenghtKey];
            string bufferStr = "";
            byte[] message2;
            BigInteger bufferBint;

            if (message.Length % lenghtKey != 0)
            {
                message2 = new byte[message.Length + (lenghtKey - (message.Length % lenghtKey))];
                for (int i = 0; i < message.Length; i++)
                {
                    message2[i] = message[i];
                }
                for (int i = message.Length; i < message2.Length; i++)
                {
                    message2[i] = 0;
                }
            }
            else
            {
                message2 = new byte[message.Length];
                for (int i = 0; i < message.Length; i++)
                {
                    message2[i] = message[i];
                }
            }
            for (int i = 0; i < message2.Length; i += (lenghtKey))
            {
                for (int j = 0; j < lenghtKey; j++)
                {
                    buffer[j] = message2[i + j];
                }
                bufferBint = GetBigIntFromBytes(buffer);
                bufferStr = СonversionTo16(EncryptionRSA(e, n, bufferBint));
                while (bufferStr.Length < (lenghtKey + 1) * 2)
                {
                    bufferStr = "0" + bufferStr;
                }
                while (bufferStr.Length > (lenghtKey + 1) * 2)
                {
                    bufferStr = bufferStr.Substring(1, bufferStr.Length - 1);
                }
                result += bufferStr;
            }



            return result;
        }

        public static BigInteger GetBigIntFromBytes(byte[] bytes)
        {
            byte[] bytesBuffer = new byte[bytes.Length + 1];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytesBuffer[i] = bytes[i];
            }

            bytesBuffer[bytesBuffer.Length - 1] = 0;
            BigInteger number = new BigInteger(bytesBuffer);
            Console.WriteLine(number);
            return number;
        }

        public static string DecryptionMessageRSA(string code, int keyLenght, BigInteger d, BigInteger n)
        {
            string result = "";
            string buffer;
            byte[] bufferBytes;
            BigInteger bufferInt;
            for (int i = 0; i < (code.Length / keyLenght); i++)
            {
                buffer ="0" + code.Substring(i * keyLenght, keyLenght);
                bufferInt = ConversionToBigInteger(buffer);
                bufferInt = DecryptionRSA(d, n, bufferInt);
                bufferBytes = bufferInt.ToByteArray();
                buffer = GetStringFromASCII(bufferBytes);
                result += buffer;
            }

            return result;
        }
    }
}
