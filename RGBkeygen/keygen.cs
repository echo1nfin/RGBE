using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace RGBkeygen
{
    internal class Keygen
    {
        private static List<byte> GenerateKey(byte length = 4)
        {
            Random random = new Random();
            List<byte> keyBytes = new List<byte>();
            for (int i = 0; i < length; i++)
            {
                keyBytes.Add((byte)random.Next(0, 256));
            }
            return keyBytes;
        }
        private static string GenerateStringKey(List<byte> keyBytes)
        {
            return $"Rgba32({string.Join(", ", keyBytes)})";
        }

        public static Image<Rgba32> GenerateImageKey()
        {
            List<byte> keyBytes = GenerateKey();
            string stringKey = GenerateStringKey(keyBytes);
            return EncryptTextAsImage(stringKey);
        }

        private static List<byte[]> ChunkBytes(byte[] data, int chunkSize)
        {
            List<byte[]> chunks = new List<byte[]>();
            for (int i = 0; i < data.Length; i += chunkSize)
            {
                byte[] chunk = new byte[chunkSize];
                int length = Math.Min(chunkSize, data.Length - i);
                Array.Copy(data, i, chunk, 0, length);
                // если не хватает до 4 байт, оставшиеся байты останутся 0
                chunks.Add(chunk);
            }
            return chunks;
        }


        private static Image<Rgba32> EncryptTextAsImage(string message)
        {
            byte[] utf8Bytes = System.Text.Encoding.UTF8.GetBytes(message);


            var byteChunks = ChunkBytes(utf8Bytes, 4);
            int totalPixels = byteChunks.Count;
            int side = (int)Math.Ceiling(Math.Sqrt(totalPixels));

            // Дополнение пустыми пикселями
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
                        var chunk = byteChunks[y * side + x];

                        byte r = chunk[0];
                        byte g = chunk[1];
                        byte b = chunk[2];
                        byte a = chunk[3];

                        row[x] = new Rgba32(r, g, b, a);
                    }
                }
            });

            return image;
        }
    }
}
