/*using Newtonsoft.Json;
using Newtonsoft.Json.Schema;*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

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
            else if (outputFormat.ToLower().Equals("ascii") || outputFormat.ToLower().Equals("dat"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".dat");
                SaveToAscii(path, image, colourType);
            }
            else if (outputFormat.ToLower().Equals("weka") || outputFormat.ToLower().Equals("arff"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".arff");
                SaveToWeka(path, image, colourType);
            }
            else if (outputFormat.ToLower().Equals("json") || outputFormat.ToLower().Equals("arff"))
            {
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".json");
                SaveToJson(path, image, colourType);
            }
            else if (outputFormat.ToLower().Equals("csv") || outputFormat.ToLower().Equals("csv"))
            {
                //slightly modified csv to allow for extra info line
                var path = String.Format("{0}\\{1}{2}", outputFolder, outputFile, ".csv");
                SaveToCSV(path, image, colourType);
            }
            else
            {
                Console.WriteLine("Incorrect file format, please specify the correct file type to save as.");
            }
        }

        private void SaveToCSV(string path, Bitmap image, string colourType)
        {
            var strBuilderOutput = ConvertImageToCsvString(image, colourType, "csv");
            var strOutput = strBuilderOutput.ToString();
            System.IO.File.WriteAllText(path, strOutput);
        }

        private StringBuilder ConvertImageToCsvString(Bitmap image, string colourType, string formatType)
        {
            int height = image.Height;
            int width = image.Width;

            StringBuilder output = new StringBuilder();
            output.Append(String.Format("%ConvertedImage {0} {1} dimensions(width/height): {2} {3}\n", formatType, colourType, width, height));

            //data is csv
            //todo fix using substring
            if (colourType.ToLower().Equals("r") || colourType.ToLower().Equals("g") || colourType.ToLower().Equals("b"))
            {
                output = GetPixelColourValueCSV(image, colourType, output);
            }
            else if (colourType.ToLower().Equals("rg") || colourType.ToLower().Equals("gr"))
            {
                output = GetPixelColourValueCSV(image, "r", output);
                output = GetPixelColourValueCSV(image, "g", output);
            }
            else if (colourType.ToLower().Equals("rb") || colourType.ToLower().Equals("br"))
            {
                output = GetPixelColourValueCSV(image, "r", output);
                output = GetPixelColourValueCSV(image, "b", output);
            }
            else if (colourType.ToLower().Equals("gb") || colourType.ToLower().Equals("bg"))
            {
                output = GetPixelColourValueCSV(image, "g", output);
                output = GetPixelColourValueCSV(image, "b", output);
            }
            else if (colourType.ToLower().Equals("bw") || colourType.ToLower().Equals("gs") ||
                     colourType.ToLower().Equals("rgb") || colourType.ToLower().Equals("rgba"))
            {
                output = GetPixelColourValueCSV(image, "r", output);
                output = GetPixelColourValueCSV(image, "g", output);
                output = GetPixelColourValueCSV(image, "b", output);
            }
            else
            {
                //do nothing rgb and also incorrect option
                //TODO JMC: add error here
            }


            return output;
        }

        private void SaveToJson(string path, Bitmap image, string colourType)
        {
            throw new NotImplementedException();
            /*JsonSchema schema = new JsonSchema();
            schema.Type = JsonSchemaType.Object;
            schema.Properties = new Dictionary<string, JsonSchema>
            {
                { "formatType", new JsonSchema { Type = JsonSchemaType.String } },
                { "colourType", new JsonSchema { Type = JsonSchemaType.String } },
                { "width", new JsonSchema { Type = JsonSchemaType.Integer } },
                { "height", new JsonSchema { Type = JsonSchemaType.Integer } },
                { "pixels", new JsonSchema { Type = JsonSchemaType.Array } }
            };*/ //TODO: Implement this

            /*string json = JsonConvert.SerializeObject(_data.ToArray());

            //write string to file
            System.IO.File.WriteAllText(@"D:\path.txt", json);
            */

        }

        private void SaveToAscii(string path, Bitmap image, String colourType)
        {
            var strBuilderOutput = ConvertImageToString(image, colourType, "ascii");
            var strOutput = strBuilderOutput.ToString();
            System.IO.File.WriteAllText(path, strOutput);
        }

        private void SaveToWeka(string path, Bitmap image, string colourType)
        {
            var strBuilderOutput = ConvertImageToWekaString(image, colourType, "weka");
            var strOutput = strBuilderOutput.ToString();
            System.IO.File.WriteAllText(path, strOutput);
        }
        
        private object ConvertImageToWekaString(Bitmap image, string colourType, string formatType)
        {
            int height = image.Height;
            int width = image.Width;

            StringBuilder output = new StringBuilder();
            output.Append(String.Format("%ConvertedImage {0} {1} dimensions(width/height): {2} {3}\n", formatType, colourType, width, height));//TODO fix up width height fuck up...
            output.Append("@RELATION 'image'\n\n");

            //create attributes
            for (int i = 0; i < height * width; i++)
            {
                output.Append("@ATTRIBUTE pixel" + i + " numeric\n");
            }

            output.Append("\n@DATA\n");

            //data is csv
            //todo fix using substring
            if (colourType.ToLower().Equals("r") || colourType.ToLower().Equals("g") || colourType.ToLower().Equals("b"))
            {
                output = GetPixelColourValueCSV(image, colourType, output);
            }
            else if (colourType.ToLower().Equals("rg") || colourType.ToLower().Equals("gr"))
            {
                output = GetPixelColourValueCSV(image, "r", output);
                output = GetPixelColourValueCSV(image, "g", output);
            }
            else if (colourType.ToLower().Equals("rb") || colourType.ToLower().Equals("br"))
            {
                output = GetPixelColourValueCSV(image, "r", output);
                output = GetPixelColourValueCSV(image, "b", output);
            }
            else if (colourType.ToLower().Equals("gb") || colourType.ToLower().Equals("bg"))
            {
                output = GetPixelColourValueCSV(image, "g", output);
                output = GetPixelColourValueCSV(image, "b", output);
            }
            else if (colourType.ToLower().Equals("bw") || colourType.ToLower().Equals("gs") ||
                     colourType.ToLower().Equals("rgb") || colourType.ToLower().Equals("rgba"))
            {
                output = GetPixelColourValueCSV(image, "r", output);
                output = GetPixelColourValueCSV(image, "g", output);
                output = GetPixelColourValueCSV(image, "b", output);
            }
            else
            {
                //do nothing rgb and also incorrect option
                //TODO JMC: add error here
            }


            return output;
        }

        private StringBuilder GetPixelColourValueCSV(Bitmap image, string channel, StringBuilder output)
        {
            //TODO: USe below
            if (channel.ToLower().Equals("r"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        if (x == image.Height - 1 && y == image.Width - 1)
                        {
                            output.Append(String.Format("{0}", pixel.R));
                        }
                        else
                        {
                            output.Append(String.Format("{0},", pixel.R));
                        }
                    }
                }
                output.Append("\n");
            }
            else if (channel.ToLower().Equals("g"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        if (x == image.Height - 1 && y == image.Width - 1)
                        {
                            output.Append(String.Format("{0}", pixel.G));
                        }
                        else
                        {
                            output.Append(String.Format("{0},", pixel.G));
                        }
                    }
                }
                output.Append("\n");
            }
            else if (channel.ToLower().Equals("b"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        if (x == image.Height -1  && y == image.Width -1)
                        {
                            output.Append(String.Format("{0}", pixel.B));
                        }
                        else
                        {
                            output.Append(String.Format("{0},", pixel.B));
                        }
                    }
                }
                //output.Append("\n");
                //TODO: get proper new line stuff to work
            }
            return output;
        }

        private StringBuilder ConvertImageToString(Bitmap image, string colourType, string formatType)
        {
            int height = image.Height;
            int width = image.Width;

            StringBuilder output = new StringBuilder();
            output.Append(String.Format("ConvertedImage {0} {1} dimensions(width/height): {2} {3}\n", formatType, colourType, width, height));//TODO fix up width height fuck up...

            if (colourType.ToLower().Equals("r"))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        var pixel = image.GetPixel(x, y); //TODO: use a faster method
                        output.Append(String.Format("{0}\n", pixel.R));
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
                        output.Append(String.Format("{0}\n", pixel.G));
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
                        output.Append(String.Format("{0}\n", pixel.B));
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
                        output.Append(String.Format("{0} {1}\n", pixel.R, pixel.G));
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
                        output.Append(String.Format("{0} {1}\n", pixel.R, pixel.B));
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
                        output.Append(String.Format("{0} {1}\n", pixel.G, pixel.B));
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
                        output.Append(String.Format("{0} {1} {2}\n", pixel.R, pixel.G, pixel.B));
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

        public Bitmap LoadAsciiFile(string fileLocation, bool? isWeka)
        {
            Bitmap bmpImage;
            String[] asciiFile;
            String[] firstLine = null;
            StringBuilder builder = new StringBuilder();

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

                        //fileFormat = firstLine[1]; //not needed
                        colourType = firstLine[2];
                        height = Convert.ToInt32(firstLine[4]);
                        width = Convert.ToInt32(firstLine[5]);
                    }
                    else
                    {
                        builder.Append(str + "\n");
                    }
                }
            }
            String file = builder.ToString();
            asciiFile = file.Split('\n');

            bmpImage = isWeka == true ? ConvertFromWekaToBitmap(asciiFile, height, width, colourType) : ConvertFromAsciiToBitmap(asciiFile, height, width, colourType);

            return bmpImage;
        }

        private Bitmap ConvertFromWekaToBitmap(string[] wekaFile, int height, int width, string colourType)
        {
            //Seek file to find @data, lines below each specifiy RGBA in order
            //format rows = height
            Bitmap image = new Bitmap(width, height);

            bool foundData = false;
            int lineCount = 0;

            for (int i = 0; i < wekaFile.Length - 1; i++)
            {
                if (foundData)
                {
                    //read data
                    if (!String.IsNullOrWhiteSpace(wekaFile[i]))
                    {
                        lineCount++;
                        image = ProcessCSVLineToImage(image, wekaFile[i], lineCount);
                    }
                    
                }

                if (wekaFile[i].ToLower().Contains("@data"))
                {
                    foundData = true;
                }
            }

            return image;
        }

        private Bitmap ProcessCSVLineToImage(Bitmap image, string line, int lineCount)
        {
            int currentWidth = 0;
            int currentHeight = 0;

            //Console.WriteLine("Line: " + line);
            var dataPoints = line.Split(','); //TODO Use something better
            //Console.WriteLine("dataPointssssss: " + dataPoints[0]);
            //check if data is doubles

            dataPoints = dataPoints.Skip(1).ToArray(); //TODO: Remove this hack

            //todo: better solution
            
            if (line.Contains("0."))
            {
                //Console.WriteLine("asf dataPoints.Length " + dataPoints.Length);
                //sigmoid and decimal
                for (int i = 0; i < dataPoints.Length; i++)
                {
                    //Console.WriteLine("data: "+i);
                    //double.Parse("3.5", CultureInfo.InvariantCulture)
                    //Console.WriteLine("dataPoint " + i + "\n");
                    var dataPoint = float.Parse(dataPoints[i], CultureInfo.InvariantCulture);
                    int dataInt = (int)Math.Round(dataPoint, 0, MidpointRounding.AwayFromZero); // Output: 2 from 1.5

                    /*Console.WriteLine("dataInt: " + dataInt);*/

                    if (dataInt == 0){
                        dataPoints[i] = "0";
                    }
                    else if (dataInt == 1)
                    {
                        dataPoints[i] = "255";
                    }
                    else
                    {
                        //todo something here
                        Console.WriteLine("FDSGDSG: " + dataPoints[i]);
                    }
                }
            }

            for (int i = 0; i < dataPoints.Length-1; i++)
            {
                if (lineCount == 1)
                {
                    //TODO: for R optimise by just defaulting everything
                    //TODO: fix datapoints array starting at 1 and ending at length -1
                    //Console.WriteLine("FDSGDSG: " + dataPoints[i]);
                    int R = int.Parse(dataPoints[i], CultureInfo.InvariantCulture); //Convert.ToInt32(dataPoints[i]);

                    var pixel = image.GetPixel(currentWidth, currentHeight);
                    var colour = Color.FromArgb(255, R, pixel.G, pixel.B);

                    image.SetPixel(currentWidth, currentHeight, colour);

                    //check and count
                    if (currentWidth < image.Width - 1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (lineCount == 2)
                {
                    int G = int.Parse(dataPoints[i], CultureInfo.InvariantCulture);

                    var pixel = image.GetPixel(currentWidth, currentHeight);
                    var colour = Color.FromArgb(255, pixel.R, G, pixel.B);

                    image.SetPixel(currentWidth, currentHeight, colour);

                    //check and count
                    if (currentWidth < image.Width - 1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (lineCount == 3)
                {
                    int B = int.Parse(dataPoints[i], CultureInfo.InvariantCulture);

                    var pixel = image.GetPixel(currentWidth, currentHeight);
                    var colour = Color.FromArgb(255, pixel.R, pixel.G, B);

                    image.SetPixel(currentWidth, currentHeight, colour);
                    
                    //check and count
                    if (currentWidth < image.Width - 1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
                else if (lineCount == 4)
                {
                    //Transpaerncy for a later date TODO: implement this feature
                    //Will just do it what the hell
                    int A = int.Parse(dataPoints[i], CultureInfo.InvariantCulture);

                    var pixel = image.GetPixel(currentWidth, currentHeight);
                    var colour = Color.FromArgb(A, pixel.R, pixel.G, pixel.B);

                    image.SetPixel(currentWidth, currentHeight, colour);

                    //check and count
                    if (currentWidth < image.Width - 1)
                    {
                        currentWidth++;
                    }
                    else
                    {
                        currentWidth = 0;
                        currentHeight++;
                    }
                }
            }

            return image;
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
                //TODO: refactor
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
                bmpImage = LoadAsciiFile(fileLocation, false);
                //quick fix
                bmpImage.RotateFlip(RotateFlipType.Rotate270FlipY); //fixes reflection about y-axis
            }
            else if (fileExtension != null && fileExtension.Contains(".arff")) //weka
            {
                //TODO: fix this it rotates and reflects the image 90 deg anti-clockwise and about the y-axis
                bmpImage = LoadAsciiFile(fileLocation, true);
                //quick fix
                bmpImage.RotateFlip(RotateFlipType.Rotate270FlipY); //fixes reflection about y-axis
            }
            else if (fileExtension != null && fileExtension.Contains(".json")) //weka
            {
                throw new NotImplementedException();
                //TODO: fix this it rotates and reflects the image 90 deg anti-clockwise and about the y-axis
                bmpImage = LoadAsciiFile(fileLocation, true);
                //quick fix
                bmpImage.RotateFlip(RotateFlipType.Rotate270FlipY); //fixes reflection about y-axis
            }
            else if (fileExtension != null && fileExtension.Contains(".json")) 
            {
                throw new NotImplementedException();
                //TODO: fix this it rotates and reflects the image 90 deg anti-clockwise and about the y-axis
                bmpImage = LoadAsciiFile(fileLocation, true);
                //quick fix
                bmpImage.RotateFlip(RotateFlipType.Rotate270FlipY); //fixes reflection about y-axis
            }
            else if (fileExtension != null && fileExtension.Contains(".csv"))
            {
                //TODO: fix this it rotates and reflects the image 90 deg anti-clockwise and about the y-axis
                bmpImage = LoadCSVFile(fileLocation);
                //quick fix
                bmpImage.RotateFlip(RotateFlipType.Rotate270FlipY); //fixes reflection about y-axis
            }
            else if (fileExtension != null) //try an image //TODO add failure
            {
                var image = Image.FromFile(fileLocation, true); //TODO add failure if fail to find 
                bmpImage = new Bitmap(image);//TODO optimise
            }

            return bmpImage;
        }

        private Bitmap LoadCSVFile(string fileLocation)
        {
            Bitmap bmpImage;
            String[] asciiFile;
            String[] firstLine = null;
            StringBuilder builder = new StringBuilder();

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

                        //fileFormat = firstLine[1]; //not needed
                        colourType = firstLine[2];
                        height = Convert.ToInt32(firstLine[4]);
                        width = Convert.ToInt32(firstLine[5]);
                    }
                    else
                    {
                        builder.Append(str + "\n");
                    }
                }
            }
            String file = builder.ToString();
            asciiFile = file.Split('\n');

            bmpImage = ConvertFromCSVToBitmap(asciiFile, height, width, colourType); //TODO ascii check like weka

            return bmpImage;
        }

        private Bitmap ConvertFromCSVToBitmap(string[] csvFile, int height, int width, string colourType)
        {
            //format rows = height
            Bitmap image = new Bitmap(width, height);

            int lineCount = 0;

            var lines = GetCsvLines(image, csvFile);

            for (int i = 0; i < lines.Length - 1; i++)
            {
                //read data
                if (!String.IsNullOrWhiteSpace(lines[i]))
                {
                    lineCount++;
                    image = ProcessCSVLineToImage(image, lines[i], lineCount);
                }
            }

            return image;
        }

        private string[] GetCsvLines(Bitmap image, string[] csvFile)
        {
            int noDataPointsPerStream = image.Width * image.Height;
            var dataPoints = csvFile[0].Split(',');

            if (csvFile.Length >= 1 && dataPoints.Length == noDataPointsPerStream)
            {
                return csvFile;
            }
            else
            {
                //TODO refactor to include bottom stuff
                //quick hack to get this to work
                //Console.WriteLine("File contains " + dataPoints.Length + " datapoints.\n");

                int noLines = dataPoints.Length / noDataPointsPerStream;
                //TODO: 
                //Console.WriteLine("Sanity Check: " + noLines + " number of lines.");

                string[] lines = new string[noLines];

                int count = 0;
                int totalCount = 0;
                int currentLine = 0;

                for (int i = 0; i < dataPoints.Length; i++)
                {
                    //Console.WriteLine("currentLine: " +currentLine);
                    if (!string.IsNullOrWhiteSpace(dataPoints[i]))
                    {
                        lines[currentLine] += "," + dataPoints[i];
                        //Console.WriteLine(lines[currentLine]);
                    }

                    totalCount++;
                    count++;
                    

                    if (count == noDataPointsPerStream && currentLine != noLines)
                    {
                        //Console.WriteLine("count: " + totalCount);
                        count = 0;
                        currentLine++;
                    }
                }
                return lines;
            }
            return null;            
        }

        public Bitmap ScaleImage(Bitmap image, int height, int width)
        {
            Bitmap resizedImage = new Bitmap(image, new Size(width, height));
            /*Bitmap resizedImage = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }*/

            return resizedImage;
        }

        public Bitmap CropImage(Bitmap bmpImage, int height, int width)
        {
            int x = bmpImage.Width / 2 - width / 2;
            int y = bmpImage.Height / 2 - height / 2;

            Rectangle cropArea = new Rectangle(x, y, width, height);

            Bitmap croppedImage = bmpImage.Clone(cropArea, bmpImage.PixelFormat);

            return croppedImage;
        }

        public void CompareTwoImages(Bitmap image1, Bitmap image2)
        {
            if (image1 == null || image2 == null)
            {
                Console.WriteLine(
                    "One or both the images are either null or have not been loaded correctly due to an error.");
            }
            else if (CompareMemCmp(image1, image2))
            {
                //they are the same YAY!
                Console.WriteLine("Image 1 is identical to Image 2.");
            }
            else
            {
                Console.WriteLine("Image 1 is not identical to Image 2.");
                ImagePercentCompare(image1, image2);
            }
        }
        
        //TODO: change the calcs a bit ... kind of not entirely happy here 
        //adapted from: https://rosettacode.org/wiki/Percentage_difference_between_images#C.23
        public void ImagePercentCompare(Bitmap image1, Bitmap image2)
        {
            //TODO: optimise using better method than getPixel
            if (image1.Size != image2.Size)
            {
                Console.Error.WriteLine("Images are of different sizes");
                return;
            }
 
            float diff = 0;
 
            for (int y = 0; y < image1.Height; y++)
            {
                for (int x = 0; x < image1.Width; x++)
                {
                    diff += (float)Math.Abs(image1.GetPixel(x, y).R - image2.GetPixel(x, y).R) / 255;
                    diff += (float)Math.Abs(image1.GetPixel(x, y).G - image2.GetPixel(x, y).G) / 255;
                    diff += (float)Math.Abs(image1.GetPixel(x, y).B - image2.GetPixel(x, y).B) / 255;
                }
            }
 
            Console.WriteLine("Difference: {0} %", 100 * diff / (image1.Width * image1.Height * 3));
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

        //http://stackoverflow.com/questions/7140081/how-to-get-only-filenames-within-a-directory-using-c
        public string[] GetFileNames(string path, string filter)
        {
            string[] files = Directory.GetFiles(path, filter)
                                     .Select(Path.GetFileName)
                                     .ToArray();
            return files;
        }
    }
}
