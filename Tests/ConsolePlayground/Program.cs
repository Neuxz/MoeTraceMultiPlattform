﻿using TraceMoe.NET;
using TraceMoe.NET.DataStructures;
using TraceMoe.NET.ImageProcessing;
using System;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using System.Collections.Generic;

namespace ConsolePlayground
{
    class Program
    {
        static void Main(string[] args)
        {

            do
            {
                ApiConversion apicon = new ApiConversion();
                string path;
                do
                {
                    path = Console.ReadLine();
                } while (File.Exists(path) == false);

                byte[] imagebyte = File.ReadAllBytes(path);
                float mp = ImageCompression.CalculateSize(imagebyte);
                imagebyte = ImageCompression.CompressImage(imagebyte, (1f / mp));
                mp = ImageCompression.CalculateSize(imagebyte);
                File.WriteAllBytes("imag2.jpg", imagebyte);

                SearchResponse sr = apicon.TraceAnimeByImageAsync(imagebyte).GetAwaiter().GetResult();

                Console.WriteLine(sr);
            } while (Console.ReadLine().ToLower().Equals("exit"));
        }
    }
}
