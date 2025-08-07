// Program.cs

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Numerics;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.Text;
using System.IO;

namespace rgbEncryptor
{
    internal static class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1. Decrypt message");
                Console.WriteLine("2. Encrypt message");
                Console.WriteLine("3. Quit");
                Console.Write("@:\\> ");

                string input = Console.ReadLine()?.ToLower() ?? string.Empty;

                switch (input)
                {
                    case "1":
                    case "decrypter":
                        RunDecryption();
                        break;
                    case "2":
                    case "encrypter":
                        RunEncryption();
                        break;
                    case "3":
                    case "quit":
                    case "exit":
                        return;
                    default:
                        Console.WriteLine("Invalid option");
                        break;
                }
            }
        }

        private static void RunDecryption()
        {
            if (!File.Exists("m.png") || !File.Exists("k.png"))
            {
                Console.WriteLine("Missing 'm.png' or 'k.png'");
                return;
            }

            var messageImage = Image.Load<Rgba32>("m.png");
            var keyImage = Image.Load<Rgba32>("k.png");
            string result = RgbDecryptor.GetDecryption(messageImage, keyImage);

            File.WriteAllText("em.txt", result);
            Console.WriteLine("Decryption complete. Saved to em.txt");
        }

        private static void RunEncryption()
        {
            if (!File.Exists("k.png"))
            {
                Console.WriteLine("Missing 'k.png'");
                return;
            }

            Console.Write("Enter path to message file: ");
            string path = Console.ReadLine() ?? "m.txt";

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string message = File.ReadAllText(path);
            Image<Rgba32> result = RgbEncryptor.GetEncrypt(message);
            result.Save("m.png");

            Console.WriteLine("Encryption complete. Saved to m.png");
        }
    }
    //private static bool CheckFile(string path)
    //{
    //    try
    //    {
    //        Image.Load<Rgba32>(path);
    //    }
    //    catch (System.IO.FileNotFoundException)
    //    {
    //        return false;
    //    }
    //    return true;
    //}

    //static void Main(string[] args)
    //{

    //    while (true)
    //    {
    //        Console.WriteLine("1. Decrypter");
    //        Console.WriteLine("2. Encrypter");
    //        Console.WriteLine("3. Quit");
    //        Console.Write("@:\\> ");

    //        string key = Console.ReadLine()?.ToLower() ?? "";

    //        string path;

    //        switch (key)
    //        {
    //            case "decrypter": case "1":
    //                if (!CheckFile("m.png"))
    //                {
    //                    Console.WriteLine("File m.png found.");
    //                    break;
    //                }
    //                if (!CheckFile("k.png"))
    //                {
    //                    Console.WriteLine("File k.png found.");
    //                    break;
    //                }

    //                Image<Rgba32> messageForDecryption = Image.Load<Rgba32>("m.png");
    //                Image<Rgba32> encryptedKey = Image.Load<Rgba32>("k.png");

    //                string decryptedMessage = DecrypterRGB.GetDecryption(messageForDecryption, encryptedKey);
    //                path = "em.txt";

    //                using (StreamWriter writer = new StreamWriter(path, false, System.Text.Encoding.UTF8))
    //                {
    //                    writer.WriteLine(decryptedMessage);
    //                }
    //                break;
    //            case "encrypter": case "2":
    //                if (!CheckFile("k.png"))
    //                {
    //                    Console.WriteLine("File k.png found.");
    //                    break;
    //                }
    //                string message;
    //                Console.WriteLine("Enter path to file with message: ");
    //                path = Console.ReadLine() ?? "m.txt"; 
    //                try
    //                {
    //                    using (StreamReader reader = new StreamReader(path))
    //                    {
    //                        message = reader.ReadToEnd();
    //                    }
    //                }
    //                catch (FileNotFoundException)
    //                {
    //                    Console.WriteLine("File not found.");
    //                    break;
    //                }
    //                Image<Rgba32> encryptedMessage = EncryptorRGB.GetEncrypt(message);
    //                encryptedMessage.Save("m.png");
    //                break;
    //            case "exit": case "3":
    //                return;
    //            default:
    //                break;
    //        }
    //    }
    //}
}
