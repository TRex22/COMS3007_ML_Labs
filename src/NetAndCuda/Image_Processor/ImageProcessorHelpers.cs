using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ImageProcessor
{
    public class ImageProcessorHelpers
    {
        public ImageProcessorHelpers()
        {

        }

        public void SaveImage(Bitmap image, string outputFormat, string outputFolder, string outputFile)
        {
            //[png][jpg][jpeg][bmp][ascii]
            if (outputFolder.ToLower().Equals("png"))
            {
                var path = String.Format("{0}{1}{2}",outputFolder, outputFile, ".png");
                image.Save(path, ImageFormat.Png);
            }
            else if (outputFolder.ToLower().Equals("jpg"))
            {
                var path = String.Format("{0}{1}{2}", outputFolder, outputFile, ".jpg");
                image.Save(path, ImageFormat.Jpeg);
            }
            else if (outputFolder.ToLower().Equals("jpeg"))
            {
                var path = String.Format("{0}{1}{2}", outputFolder, outputFile, ".jpeg");
                image.Save(path, ImageFormat.Jpeg);
            }
            else if (outputFolder.ToLower().Equals("bmp"))
            {
                var path = String.Format("{0}{1}{2}", outputFolder, outputFile, ".bmp");
                image.Save(path, ImageFormat.Bmp);
            }
            else if (outputFolder.ToLower().Equals("ascii"))
            {
                throw new NotImplementedException();
            }
            else
            {
                Console.WriteLine("Incorrect file format, please specify the correct file type to save as.");
            }
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
                    int grey = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                    greyscale.SetPixel(x, y, Color.FromArgb(pixelColor.A, grey, grey, grey));
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
                    if (pixelColor.R >= 128)
                    {
                        //white
                        R = 255;
                    }
                    //G
                    if (pixelColor.G >= 128)
                    {
                        //white
                        G = 255;
                    }
                    //B
                    if (pixelColor.B >= 128)
                    {
                        //white
                        B = 255;
                    }
                    bwBitmap.SetPixel(x, y, Color.FromArgb(pixelColor.A, R, G, B));
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
                    outputBmp.SetPixel(x, y, Color.FromArgb(input.GetPixel(x, y).A, input.GetPixel(x, y).R, 0, 0));
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
                    outputBmp.SetPixel(x, y, Color.FromArgb(input.GetPixel(x, y).A, 0, input.GetPixel(x, y).G, 0));
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
                    outputBmp.SetPixel(x, y, Color.FromArgb(input.GetPixel(x, y).A, 0, 0, input.GetPixel(x, y).B));
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
                    outputBmp.SetPixel(x, y, Color.FromArgb(input.GetPixel(x, y).A, input.GetPixel(x, y).R, input.GetPixel(x, y).G, 0));
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
                    outputBmp.SetPixel(x, y, Color.FromArgb(input.GetPixel(x, y).A, input.GetPixel(x, y).R, 0, input.GetPixel(x, y).B));
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
                    outputBmp.SetPixel(x, y, Color.FromArgb(input.GetPixel(x, y).A, 0, input.GetPixel(x, y).G, input.GetPixel(x, y).B));
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
    }
}
