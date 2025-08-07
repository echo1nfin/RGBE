// Encryptor.cs

using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Memory;
using SixLabors.ImageSharp.PixelFormats;

namespace rgbEncryptor
{
    internal static class RgbEncryptor
    {
        private static List<byte[]> ChunkBytes(byte[] data, int chunkSize)
        {
            List<byte[]> chunks = new List<byte[]>();
            for (int i = 0; i < data.Length; i += chunkSize)
            {
                byte[] chunk = new byte[chunkSize];
                int length = Math.Min(chunkSize, data.Length - i);
                Array.Copy(data, i, chunk, 0, length);

                // fill free bytes with 0
                chunks.Add(chunk);
            }
            return chunks;
        }

        private static Image<Rgba32> EncryptTextAsImage(string message, List<byte>? shiftKey = null)
        {
            byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(message);


            var byteChunks = ChunkBytes(utf8Bytes, 4);
            int totalPixels = byteChunks.Count;
            int side = (int)Math.Ceiling(Math.Sqrt(totalPixels));

            // Fill void pixels with 0s
            while (byteChunks.Count < side * side)
            {
                byteChunks.Add(new byte[4] { 0, 0, 0, 0 });
            }

            Image<Rgba32> image = new Image<Rgba32>(side, side);

            image.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < side; y++)
                {
                    var row = accessor.GetRowSpan(y);
                    for (int x = 0; x < side; x++)
                    {
                        // row by row, pixel by pixel
                        var chunk = byteChunks[y * side + x];

                        byte r = chunk[0];
                        byte g = chunk[1];
                        byte b = chunk[2];
                        byte a = chunk[3];

                        if (shiftKey != null)
                        {
                            r = HelpMethods.CircleNumber(r + shiftKey[0]);
                            g = HelpMethods.CircleNumber(g + shiftKey[1]);
                            b = HelpMethods.CircleNumber(b + shiftKey[2]);
                            a = HelpMethods.CircleNumber(a + shiftKey[3]);
                        }

                        row[x] = new Rgba32(r, g, b, a);
                    }
                }
            });

            return image;
        }

        public static Image<Rgba32> GetEncrypt(string message)
        {
            List<byte> key = RgbDecryptor.DecryptKey(
                                 RgbDecryptor.DecryptMessage(
                                     Image.Load<Rgba32>("k.png")
                                 )
                             );
            return EncryptTextAsImage(message, key);
        }
    }

}
