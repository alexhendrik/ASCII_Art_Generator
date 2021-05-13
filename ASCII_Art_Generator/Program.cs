using System;
using System.Collections.Generic;
using System.Drawing;

namespace ASCII_Art_Generator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter file path:");

            var path = Console.ReadLine();

            var processor = new BitmapProcessor();

            Console.WriteLine(processor.GetAsciiString(new Bitmap(path)));
        }
    }
}
