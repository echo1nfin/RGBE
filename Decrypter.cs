// Decryptor.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System.Drawing;
using System.ComponentModel.DataAnnotations.Schema;

namespace rgbEncryptor
{
    internal static class RgbDecryptor
    {

        public static List<byte> DecryptKey(string stringKey)
        {
            List<byte> key = new List<byte>();
            try
            {
                // Rgba32(0, 0, 0, 0) -> 0, 0, 0, 0)
                string tempStringKey = stringKey.Substring(7);

                // 0, 0, 0, 0) -> 0, 0, 0, 0
                tempStringKey = tempStringKey.Substring(0, tempStringKey.Length - 1);

                // 0, 0, 0, 0 -> 0 0 0 0
                string[] parts = tempStringKey.Replace(", ", " ").Split(' ');

                foreach (string s in parts)
                {
                    key.Add((byte)Convert.ToInt32(s));
                }
            }
            // catch format exceptions if the string is not in the correct format
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid key format.\n {ex.Message}");
                return new List<byte> { 0, 0, 0, 0 };
            }

            return key;
        }

        public static string DecryptMessage(Image<Rgba32> encryptedMessage, List<byte>? byteKey = null)
        {
            List<byte> byteList = new List<byte>();

            encryptedMessage.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < encryptedMessage.Height; y++)
                {
                    var row = accessor.GetRowSpan(y);
                    for (int x = 0; x < encryptedMessage.Width; x++)
                    {
                        byte r = row[x].R;
                        byte g = row[x].G;
                        byte b = row[x].B;
                        byte a = row[x].A;

                        if (byteKey != null)
                        {
                            r = HelpMethods.CircleNumber(r - byteKey[0]);
                            g = HelpMethods.CircleNumber(g - byteKey[1]);
                            b = HelpMethods.CircleNumber(b - byteKey[2]);
                            a = HelpMethods.CircleNumber(a - byteKey[3]);
                        }

                        byteList.Add(r);
                        byteList.Add(g);
                        byteList.Add(b);
                        byteList.Add(a);
                    }
                }
            });

            // delete zero bytes at the end
            while (byteList.Count > 0 && byteList[byteList.Count - 1] == 0)
            {
                byteList.RemoveAt(byteList.Count - 1);
            }

            return System.Text.Encoding.UTF8.GetString(byteList.ToArray());
        }

        public static string GetDecryption(Image<Rgba32> encryptedMessage, Image<Rgba32> keyImage)
        {
            string keyString = DecryptMessage(keyImage);
            List<byte> byteKey = DecryptKey(keyString);
            string message = DecryptMessage(encryptedMessage, byteKey);

            return message;
        }
    }

}