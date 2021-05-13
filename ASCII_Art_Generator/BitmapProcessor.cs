using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCII_Art_Generator
{
    public class BitmapProcessor
    {
        public string GetAsciiString(Bitmap bmp)
        {
            var resultString = "";

            var chars = GetCharacterIntensities();

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    var color = bmp.GetPixel(x, y);

                    var intensity = (color.R + color.G + color.B + color.A) / 8;

                    if (intensity < chars.Count)
                        resultString += chars[intensity];
                    else
                        resultString += chars.Last();

                    resultString += ' ';
                }

                resultString += "\n";
            }

            return resultString;
        }

        private List<char> GetCharacterIntensities()
        {
            var result = new List<char>();

            for (int i = 32; i < 127; i++)
            {
                var asciiChar = Convert.ToChar(i);

                if (char.IsControl(asciiChar)) continue; // TODO: Handle the missing dictionary entries resulting from this

                result.Add(asciiChar);
            }

            result = result.OrderBy(ch => GetCharDensity(ch)).ToList();

            return result;
        }

        private double GetCharDensity(char character)
        {
            var charBmp = new Bitmap(10, 10);
            var brush = new SolidBrush(Color.Black);

            Graphics graphics = Graphics.FromImage(charBmp);
            var size = graphics.MeasureString(character.ToString(), SystemFonts.DefaultFont);

            charBmp = new Bitmap((int)size.Width, (int)size.Height);

            graphics.DrawString(character.ToString(), SystemFonts.DefaultFont, brush, new PointF(size.Width / 2, size.Height / 2));
            graphics.Save();

            var blackCount = 0;
            var whiteCount = 0;

            for (int y = 0; y < (int)size.Height; y++)
            {
                for (int x = 0; x < (int)size.Width; x++)
                {
                    var pixel = charBmp.GetPixel(x, y);

                    if (pixel == Color.Black)
                        blackCount++;
                    else
                        whiteCount++;
                }
            }

            return blackCount / whiteCount;
        }

    }
}
