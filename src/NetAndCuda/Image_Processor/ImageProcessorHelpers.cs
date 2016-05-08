﻿using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageProcessor
{
    public class ImageProcessorHelpers
    {
        public ImageProcessorHelpers()
        {
        }

        public void SaveImage(Bitmap image, string colourType, string outputFormat, string outputFolder,
            string outputFile)
        {
            //TODO: check if folder is actually real, no input foldername
            System.IO.Directory.CreateDirectory(outputFolder);

            //[png][jpg][jpeg][bmp][ascii]
            if (outputFormat.ToLower().Equals("png"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".png");
                image.Save(path, ImageFormat.Png);
            }
            else if (outputFormat.ToLower().Equals("jpg"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".jpg");
                image.Save(path, ImageFormat.Jpeg);
            }
            else if (outputFormat.ToLower().Equals("jpeg"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".jpeg");
                image.Save(path, ImageFormat.Jpeg);
            }
            else if (outputFormat.ToLower().Equals("bmp"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".bmp");
                image.Save(path, ImageFormat.Bmp);
            }
            else if (outputFormat.ToLower().Equals("ascii"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".dat");
                SaveToAscii(path, image, colourType);
            }
            else
            {
                Console.WriteLine("Incorrect file format, please specify the correct file type to save as.");
            }
        }

        private void SaveToAscii(string path, Bitmap image, String colourType)
        {
            var strOutput = ConvertImageToString(image, colourType, "ascii");
            System.IO.File.WriteAllText(path, strOutput);
        }

        private string ConvertImageToString(Bitmap image, string colourType, string formatType)
        {
            int height = image.Height;
            int width = image.Width;
            String output = String.Format("ConvertedImage {0} {1} dimensions(height/width): {2} {3}\n", formatType,
                colourType, height, width);

            if (colourType.ToLower().Equals("r"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output += String.Format("{0}\n", pixel.R);
                    }
                }
            }
            else if (colourType.ToLower().Equals("g"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output += String.Format("{0}\n", pixel.G);
                    }
                }
            }
            else if (colourType.ToLower().Equals("b"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output += String.Format("{0}\n", pixel.B);
                    }
                }
            }
            else if (colourType.ToLower().Equals("rg") || colourType.ToLower().Equals("gr"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output += String.Format("{0} {1}\n", pixel.R, pixel.G);
                    }
                }
            }
            else if (colourType.ToLower().Equals("rb") || colourType.ToLower().Equals("br"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output += String.Format("{0} {1}\n", pixel.R, pixel.B);
                    }
                }
            }
            else if (colourType.ToLower().Equals("gb") || colourType.ToLower().Equals("bg"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output += String.Format("{0} {1}\n", pixel.G, pixel.B);
                    }
                }
            }
            else if (colourType.ToLower().Equals("bw") || colourType.ToLower().Equals("gs") ||
                     colourType.ToLower().Equals("rgb") || colourType.ToLower().Equals("rgba"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output += String.Format("{0} {1} {2}\n", pixel.R, pixel.G, pixel.B);
                    }
                }
            }
            else
            {
                //do nothing rgb and also incorrect option
                //TODO JMC: add error here
            }

            return output;
        }

        public Bitmap ConvertImageColourScale(Bitmap image, string colourType)
        {
            //[rgb][r][g][b][bw][gs]
            //if (colourType.ToLower().Equals("rgb")) //do nothing will be final case
            //TODO: more elegant option

            if (colourType.ToLower().Equals("r"))
            {
                image = ConvertToRedScale(image);
            }
            else if (colourType.ToLower().Equals("g"))
            {
                image = ConvertToGreenScale(image);
            }
            else if (colourType.ToLower().Equals("b"))
            {
                image = ConvertToBlueScale(image);
            }
            else if (colourType.ToLower().Equals("rg") || colourType.ToLower().Equals("gr"))
            {
                image = ConvertToRedGreenScale(image);
            }
            else if (colourType.ToLower().Equals("rb") || colourType.ToLower().Equals("br"))
            {
                image = ConvertToRedBlueScale(image);
            }
            else if (colourType.ToLower().Equals("gb") || colourType.ToLower().Equals("bg"))
            {
                image = ConvertToGreenBlueScale(image);
            }
            else if (colourType.ToLower().Equals("bw"))
            {
                image = ConvertToBlackWhiteReduction(image);
            }
            else if (colourType.ToLower().Equals("gs"))
            {
                image = ConvertToGreyScale(image);
            }
            else
            {
                //do nothing rgb and also incorrect option
                //TODO JMC: add error here
            }

            return image;
        }

        //http://stackoverflow.com/questions/21527518/converting-rgb-images-to-grayscale
        //TODO: Make all solutions the same using a better method set
        public Bitmap ConvertToGreyScale(Bitmap input)
        {
            Bitmap greyscale = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    Color pixelColor = input.GetPixel(x, y);
                    //  0.3 · r + 0.59 · g + 0.11 · b
                    int grey = (int) (pixelColor.R*0.3 + pixelColor.G*0.59 + pixelColor.B*0.11);
                    greyscale.SetPixel(x, y, Color.FromArgb(255, grey, grey, grey));
                }
            }
            return greyscale;
        }

        //Using my own nasty light/dark codition
        public Bitmap ConvertToBlackWhiteReduction(Bitmap input)
        {
            Bitmap bwBitmap = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    Color pixelColor = input.GetPixel(x, y);
                    int R = 0;
                    int G = 0;
                    int B = 0;

                    //R
                    if ((pixelColor.R + pixelColor.G + pixelColor.B) >= 383) //255+3=765
                    {
                        //white
                        R = 255;
                        G = 255;
                        B = 255;
                    }
                    bwBitmap.SetPixel(x, y, Color.FromArgb(255, R, G, B));
                }
            }
            return bwBitmap;
        }

        public Bitmap ConvertToRedScale(Bitmap input)
        {
            Bitmap outputBmp = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    outputBmp.SetPixel(x, y, Color.FromArgb(255, input.GetPixel(x, y).R, 0, 0));
                }
            }
            return outputBmp;
        }

        public Bitmap ConvertToGreenScale(Bitmap input)
        {
            Bitmap outputBmp = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    outputBmp.SetPixel(x, y, Color.FromArgb(255, 0, input.GetPixel(x, y).G, 0));
                }
            }
            return outputBmp;
        }

        public Bitmap ConvertToBlueScale(Bitmap input)
        {
            Bitmap outputBmp = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    outputBmp.SetPixel(x, y, Color.FromArgb(255, 0, 0, input.GetPixel(x, y).B));
                }
            }
            return outputBmp;
        }

        public Bitmap ConvertToRedGreenScale(Bitmap input)
        {
            Bitmap outputBmp = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    outputBmp.SetPixel(x, y, Color.FromArgb(255, input.GetPixel(x, y).R, input.GetPixel(x, y).G, 0));
                }
            }
            return outputBmp;
        }

        public Bitmap ConvertToRedBlueScale(Bitmap input)
        {
            Bitmap outputBmp = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    outputBmp.SetPixel(x, y, Color.FromArgb(255, input.GetPixel(x, y).R, 0, input.GetPixel(x, y).B));
                }
            }
            return outputBmp;
        }

        public Bitmap ConvertToGreenBlueScale(Bitmap input)
        {
            Bitmap outputBmp = new Bitmap(input.Width, input.Height);
            for (int x = 0; x < input.Width; x++)
            {
                for (int y = 0; y < input.Height; y++)
                {
                    outputBmp.SetPixel(x, y, Color.FromArgb(255, 0, input.GetPixel(x, y).G, input.GetPixel(x, y).B));
                }
            }
            return outputBmp;
        }

        public Bitmap CreateRndImage(int height, int width)
        {
            var image = new Bitmap(width, height);
            Random rnd = new Random(DateTime.UtcNow.Millisecond); //TODO make better

            //TODO: JMC make this faster by implementing a better bitmap class, setpixel locks the image at every step
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    int colourCode1 = rnd.Next(0, 256);
                    int colourCode2 = rnd.Next(0, 256);
                    int colourCode3 = rnd.Next(0, 256);
                    image.SetPixel(j, i, Color.FromArgb(255, colourCode1, colourCode2, colourCode3));
                }
            }

            return image;
        }

        public Bitmap LoadAsciiFile(string fileLocation)
        {
            Bitmap bmpImage;
            String[] asciiFile;
            String[] firstLine = null;
            String rawString = "";

            String fileFormat = null;
            String colourType = null;
            int height = 0;
            int width = 0;

            using (StreamReader sr = new StreamReader(fileLocation))
            {
                while (sr.Peek() >= 0)
                {
                    //ConvertedImage ascii rb dimensions(height/width): 100 100
                    String str = sr.ReadLine();
                    if (firstLine == null)
                    {
                        //this is the first line (5 items)
                        firstLine = str.Split(' ');

                        fileFormat = firstLine[1];
                        colourType = firstLine[2];
                        height = Convert.ToInt32(firstLine[4]);
                        width = Convert.ToInt32(firstLine[5]);
                    }
                    else
                    {
                        rawString += str + "\n";
                    }
                }
            }

            asciiFile = rawString.Split('\n');

            bmpImage = ConvertFromAsciiToBitmap(asciiFile, height, width, colourType);

            return bmpImage;
        }

        private Bitmap ConvertFromAsciiToBitmap(string[] asciiFile, int height, int width, String colourType)
        {
            //format rows = height
            Bitmap image = new Bitmap(width, height);

            int currentWidth = 0;
            int currentHeight = 0;

            //loop lines
            for (int i = 0; i < asciiFile.Length-1; i++)
            {
                //loop columns
                if (colourType.ToLower().Equals("r"))
                {
                    //one column
                    String[] strVal = asciiFile[i].Split('\n');
                    int R = Convert.ToInt32(strVal[0]);

                    image.SetPixel(currentWidth, currentHeight, Color.FromArgb(255, R, 0, 0));

                    //check and count
                    if (currentWidth < width-1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (colourType.ToLower().Equals("g"))
                {
                    //one column
                    String[] strVal = asciiFile[i].Split('\n');
                    int G = Convert.ToInt32(strVal[0]);

                    image.SetPixel(currentWidth, currentHeight, Color.FromArgb(255, 0, G, 0));

                    //check and count
                    if (currentWidth < width-1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (colourType.ToLower().Equals("b"))
                {
                    //one column
                    String[] strVal = asciiFile[i].Split('\n');
                    int B = Convert.ToInt32(strVal[0]);

                    image.SetPixel(currentWidth, currentHeight, Color.FromArgb(255, B, 0, B));

                    //check and count
                    if (currentWidth < width-1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (colourType.ToLower().Equals("rg") || colourType.ToLower().Equals("gr"))
                {
                    //two column
                    String[] strVal = asciiFile[i].Split(' ');
                    int R = Convert.ToInt32(strVal[0]);
                    int G = Convert.ToInt32(strVal[1].Trim('\n'));

                    image.SetPixel(currentWidth, currentHeight, Color.FromArgb(255, R, G, 0));

                    //check and count
                    if (currentWidth < width-1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (colourType.ToLower().Equals("rb") || colourType.ToLower().Equals("br"))
                {
                    //two column
                    String[] strVal = asciiFile[i].Split(' ');
                    int R = Convert.ToInt32(strVal[0]);
                    int B = Convert.ToInt32(strVal[1].Trim('\n'));

                    //Console.WriteLine("cW: "+currentWidth+" width: "+width+" cH: "+currentHeight+" height: "+height); //TODO Cleanup when done
                    image.SetPixel(currentWidth, currentHeight, Color.FromArgb(255, R, 0, B));

                    //check and count
                    if (currentWidth < width-1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (colourType.ToLower().Equals("gb") || colourType.ToLower().Equals("bg"))
                {
                    //two column
                    String[] strVal = asciiFile[i].Split(' ');
                    int G = Convert.ToInt32(strVal[0]);
                    int B = Convert.ToInt32(strVal[1].Trim('\n'));

                    image.SetPixel(currentWidth, currentHeight, Color.FromArgb(255, 0, G, B));

                    //check and count
                    if (currentWidth < width-1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (colourType.ToLower().Equals("bw") || colourType.ToLower().Equals("gs") ||
                     colourType.ToLower().Equals("rgb") || colourType.ToLower().Equals("rgba"))
                {
                    //all three columns
                    //two column
                    String[] strVal = asciiFile[i].Split(' ');
                    int R = Convert.ToInt32(strVal[0]);
                    int G = Convert.ToInt32(strVal[1]);
                    int B = Convert.ToInt32(strVal[2].Trim('\n'));

                    image.SetPixel(currentWidth, currentHeight, Color.FromArgb(255, R, G, B));

                    //check and count
                    if (currentWidth < width-1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else
                {
                    //do nothing rgb and also incorrect option
                    //TODO JMC: add error here
                }
            }

            return image;
        }

        public Bitmap OpenImageFile(String fileExtension, String fileLocation)
        {
            Bitmap bmpImage = null;
            //open file/s
            if (fileExtension != null && fileExtension.Contains(".dat"))
            {
                //TODO: fix this it rotates and reflects the image 90 deg anti-clockwise and about the y-axis
                bmpImage = LoadAsciiFile(fileLocation);
                //quick fix
                bmpImage.RotateFlip(RotateFlipType.Rotate270FlipY); //fixes reflection about y-axis
            }
            else if (fileExtension != null) //try an image
            {
                var image = Image.FromFile(fileLocation, true); //TODO add failure if fail to find 
                bmpImage = new Bitmap(image);//TODO optimise
            }

            return bmpImage;
        }

        //got it from: http://stackoverflow.com/questions/2031217/what-is-the-fastest-way-i-can-compare-two-equal-size-bitmaps-to-determine-whethe
        [DllImport("msvcrt.dll")]
        public static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        public bool CompareMemCmp(Bitmap b1, Bitmap b2)
        {
            if ((b1 == null) != (b2 == null)) return false;
            if (b1.Size != b2.Size) return false;

            var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            try
            {
                IntPtr bd1scan0 = bd1.Scan0;
                IntPtr bd2scan0 = bd2.Scan0;

                int stride = bd1.Stride;
                int len = stride * b1.Height;

                return memcmp(bd1scan0, bd2scan0, len) == 0;
            }
            finally
            {
                b1.UnlockBits(bd1);
                b2.UnlockBits(bd2);
            }
        }
    }
}
