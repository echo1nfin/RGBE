using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using RGBkeygen;

namespace rgbEncryptor
{
    internal static class Program
    {
        private static bool CheckFile(string path)
        {
            try
            {
                Image.Load<Rgba32>(path);
            }
            catch (System.IO.FileNotFoundException)
            {
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {

            while (true)
            {
                Console.WriteLine("1. keygen");
                Console.WriteLine("2. Quit");
                Console.Write("@:\\ ");

                string key = Console.ReadLine()?.ToLower() ?? "";

                switch (key)
                {
                    case "1":
                        Keygen.GenerateImageKey().Save("k.png");
                        break;
                    case "2":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}