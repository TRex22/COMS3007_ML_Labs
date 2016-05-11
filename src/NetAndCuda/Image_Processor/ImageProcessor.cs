using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace ImageProcessor
{
    /*
     * Jason Chalom 2016, Image Processor
     * Provide reference to this project when using it for research
     * TODO: rename since http://www.hanselman.com/blog/NuGetPackageOfTheWeekImageProcessorLightweightImageManipulationInC.aspx exists
     * TODO: Make into class library
     * TODO: Make Unit Tests
     * TODO: Convert all messages to a correct config structure
     * TODO: Global vars for things
     * TODO: add ascii options ie row/column, for now using row
     * TODO: ascii include A from RGBA ie allow transparency
     * TODO: Make a random destruction option either dark or light
     * TODO: open folder of images
     * TODO: allow merge to convert as well - not in first version
     * TODO: add crop functionality
     * TODO: add scaling functionality in convert
     * TODO: add more file formats
     * TODO: convert from ASCII
     * TODO: add filters
     * TODO: add inversion of colours -simple enough
     * TODO: proper error messages
     * TODO: reflections
     * TODO: rotations
     * TODO: Batch processing
     * TODO: Pixel destruction
     * TODO: noise addition gauss, poisson etc...
     * TODO: batch script i.e run convert and scale
     * TODO: refactor code
     * TODO: refactor how args are processed
     * TODO: rgb rbg have a thing which converts all possible inputs into readable form
     * TODO: weka support
     * TODO: scripting language for filters and using multiple arguments with file or batch
     * TODO: csv, JSON ouput
     * 
     * TODO: http://stackoverflow.com/questions/568968/does-any-one-know-of-a-faster-method-to-do-string-split
     * TODO: http://stackoverflow.com/questions/399798/memory-efficiency-and-performance-of-string-replace-net-framework/400065#400065
     * 
     * TODO: add this
     * graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

     * TODO: fix scaling batch cropping issue
     * TODO: check image memory cleanup
     * 
     * RGB / RGBA problem
     * 
     * ASCII Channels will always be stored per column/row (depeneding on future updates to code) in the RGBA way
     * i.e. rg (or gr) will have red channel first then green or RA will be red then alpha
     */

    public class ImageProcessor
    {
        private static readonly ImageProcessorHelpers Helpers = new ImageProcessorHelpers();

        public static void Main(string[] args)
        {
            Console.Out.WriteLine("Image Processor Commandline Application by Jason Chalom 2016, Version " +
                                  Assembly.GetExecutingAssembly().GetName().Version);

            if (args.Length > 0 && args[0].ToLower().Equals("batch"))
            {
                RunBatch(args);
            }
            else
            {
                RunArguments(args);
            }


            //Console.WriteLine("Press any key to continue...");
            //Console.ReadLine();
        }

        private static void RunBatch(string[] args)
        {
            //USAGE for batch: ImageProcessor batch [input folder] [output folder] scale bmp 100 100 <(height width)> 
            Console.WriteLine("Running Batch Process...");
            var failDueToInputOutputMatch = false;

            var inputFolder = args[1];
            var outputFolder = args[2];
            var mainArg = args[3];

            if (inputFolder.Contains(outputFolder) || outputFolder.Contains(inputFolder))
            {
                //cannot be the same folder
                failDueToInputOutputMatch = true;
                Console.WriteLine("Error. Cannot have the same input and output folder.");
            }

            var inputFiles = Helpers.GetFileNames(inputFolder, "*.*");

            var newArgs = new string[8];
            // [convert][rndImage][scale][crop][compare] 

            if (mainArg.ToLower().Equals("convert") && !failDueToInputOutputMatch)
            {
                //ImageProcessor convert [input filename and location] rgb [output foldername] [filename] bmp
                //USAGE for batch: ImageProcessor batch [input folder] [output folder] convert rgb bmp 100 100
                newArgs[0] = "convert";

                newArgs[2] = args[4];
                newArgs[3] = outputFolder;
                newArgs[5] = args[5];

                foreach (string t in inputFiles)
                {
                    if (t.Contains(".png") || t.Contains(".bmp") ||
                        t.Contains(".jpg") || t.Contains(".jpeg") ||
                        t.Contains(".dat") || t.Contains(".arff") ||
                        t.Contains(".json"))//TODO make an enum to store all used filetypes, complete refactor rquired here
                    {
                        var fileName = t.Substring(0, t.IndexOf(".", StringComparison.Ordinal));
                        var fileLocation = string.Format("{0}\\{1}", inputFolder, t);

                        newArgs[1] = fileLocation;
                        newArgs[4] = fileName; //TODO namescheme
                        
                        RunArguments(newArgs);
                    }//else ignore file
                    else
                    {
                        Console.WriteLine("Ignored File: " + t);
                    }
                }
            }
            else if (mainArg.ToLower().Equals("rndimage") && !failDueToInputOutputMatch)
            {
                Console.WriteLine("Error. rndImage does not work with batch argument.");
            }
            else if (mainArg.ToLower().Equals("scale") && !failDueToInputOutputMatch)
            {
                //ImageProcessor scale [input filename and location] [output foldername] [filename] bmp 100 100 <(height width)>
                //USAGE for batch: ImageProcessor batch [input folder] [output folder] scale bmp 100 100 <(height width)>
                newArgs[0] = "scale";
                newArgs[2] = outputFolder;
                newArgs[4] = args[4];
                newArgs[5] = args[5];
                newArgs[6] = args[6];

                foreach (string t in inputFiles)
                {
                    if (t.Contains(".png") || t.Contains(".bmp") ||
                        t.Contains(".jpg") || t.Contains(".jpeg") ||
                        t.Contains(".dat") || t.Contains(".arff") || 
                        t.Contains(".json")) //TODO make an enum to store all used filetypes, complete refactor rquired here
                    {
                        var fileName = t.Substring(0, t.IndexOf(".", StringComparison.Ordinal));
                        var fileLocation = string.Format("{0}\\{1}", inputFolder, t);

                        newArgs[1] = fileLocation;
                        newArgs[3] = fileName;
                        
                        RunArguments(newArgs);
                    }//else ignore file
                    else
                    {
                        Console.WriteLine("Ignored File: " + t);
                    }
                }
            }
            else if (mainArg.ToLower().Equals("crop") && !failDueToInputOutputMatch)
            {
                //ImageProcessor crop [input filename and location] [output foldername] [filename] bmp 100 100 <(height width)>
                //USAGE for batch: ImageProcessor batch [input folder] [output folder] crop bmp 100 100 <(height width)>
                newArgs[0] = "crop";
                newArgs[2] = outputFolder;
                newArgs[4] = args[4];
                newArgs[5] = args[5];
                newArgs[6] = args[6];

                foreach (string t in inputFiles)
                {
                    if (t.Contains(".png") || t.Contains(".bmp") ||
                        t.Contains(".jpg") || t.Contains(".jpeg") ||
                        t.Contains(".dat") || t.Contains(".arff") ||
                        t.Contains(".json")) //TODO make an enum to store all used filetypes, complete refactor rquired here
                    {
                        var fileName = t.Substring(0, t.IndexOf(".", StringComparison.Ordinal));
                        var fileLocation = string.Format("{0}\\{1}", inputFolder, t);

                        newArgs[1] = fileLocation;
                        newArgs[3] = fileName;

                        RunArguments(newArgs);
                    }//else ignore file
                    else
                    {
                        Console.WriteLine("Ignored File: "+t);
                    }
                }
            }
            else if (mainArg.ToLower().Equals("compare") && !failDueToInputOutputMatch)
            {
                //ImageProcessor compare [input filename and location of image 1] [input filename and location of image 2] 
                //USAGE for batch: ImageProcessor batch [input folder] [output folder] 
                newArgs[0] = "compare";
                throw new NotImplementedException(); //TODO implement batch compare
            }
            else
            {
                Console.WriteLine("Error: Missing Inputs In First Argument.");
                PrintHelp();
            }
        }

        private static void RunArguments(string[] args)
        {
            //All single arguments, batches are handled in main
            var UnknownArgs = false;

            if (args.Length > 0 && args[0].ToLower().Equals("convert"))
            {
                Console.WriteLine("Convert Image...");
                //USAGE: ImageProcessor convert [input filename and location] rgb [output foldername] [filename] bmp //100 100 - thats for crop a bit complicated to implement

                var fileLocation = args[1];
                var colourType = args[2];
                var outputFolder = args[3];
                var outputFilename = args[4];
                var outputFormat = args[5];

                var fileExtension = Path.GetExtension(fileLocation);

                var bmpImage = Helpers.OpenImageFile(fileExtension, fileLocation);

                //crop - not in this version

                //check if ascii - not in this version
                //run merge - not in this version

                if (bmpImage != null)
                {
                    //split if required
                    var convertedImage = Helpers.ConvertImageColourScale(bmpImage, colourType);

                    //save as required format
                    Helpers.SaveImage(convertedImage, colourType, outputFormat, outputFolder, outputFilename);

                    Console.WriteLine("Completed Operation, Image converted.");
                }
                else
                {
                    Console.WriteLine("Error Occured Converting Image.");
                }
            }
            else if (args.Length > 0 && args[0].ToLower().Equals("rndimage"))
            {
                Console.WriteLine("Generate Random Image...");
                //ImageProcessor rndImage rgb [output foldername] [filename] png 100 100 1

                var areAllInputsThere = AreAllInputsThere(args, true);
                UnknownArgs = !areAllInputsThere;

                if (areAllInputsThere)
                {
                    //check if folder, has to be folder for rnd
                    //if so run for all items in folder
                    //since rnd create folder if it does not exist

                    //filename
                    var filename = args[3];
                    var outputFolder = args[2];
                    var colourType = args[1];
                    var outputFormat = args[4];

                    var noImg = Convert.ToInt32(args[7]); // will fail if not int
                    var height = Convert.ToInt32(args[5]);
                    var width = Convert.ToInt32(args[6]);

                    //create rnd images
                    for (var i = 0; i < noImg; i++)
                    {
                        var rndImage = Helpers.CreateRndImage(height, width);

                        //split if required
                        var convertedImage = Helpers.ConvertImageColourScale(rndImage, colourType);

                        //save as required format
                        var filenameNumbered = string.Format("{0}_{1}", filename, i + 1);
                        Helpers.SaveImage(convertedImage, colourType, outputFormat, outputFolder, filenameNumbered);
                    }
                    Console.WriteLine("Completed Operation, {0} random images created.", noImg);
                }
                else
                {
                    Console.WriteLine("Error: Missing Inputs in rndImage.");
                    UnknownArgs = true;
                }
            }
            else if (args.Length > 0 && args[0].ToLower().Equals("scale"))
            {
                //scaling
                Console.WriteLine("Scaling Image Using Given Dimensions...");
                //ImageProcessor scale [input filename and location] [output foldername] [filename] bmp 100 100 (height width)

                if (args.Length >= 7)
                {
                    var fileLocation = args[1];
                    var outputFolder = args[2];
                    var outputFilename = args[3];
                    var fileFormat = args[4];
                    var height = Convert.ToInt32(args[5]);
                    var width = Convert.ToInt32(args[6]);

                    var fileExtension = Path.GetExtension(fileLocation);
                    var bmpImage = Helpers.OpenImageFile(fileExtension, fileLocation);

                    if (bmpImage != null)
                    {
                        var resizedImage = Helpers.ScaleImage(bmpImage, height, width);

                        //save as required format
                        Helpers.SaveImage(resizedImage, "RGB", fileFormat, outputFolder, outputFilename);
                        resizedImage.Dispose();
                        Console.WriteLine("Completed Operation, Image Scaled.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Missing Inputs In Scaling.");
                    UnknownArgs = true;
                }
            }
            else if (args.Length > 0 && args[0].ToLower().Equals("crop"))
            {
                //cropping
                Console.WriteLine("Crop Image Using Given Dimensions...");
                //ImageProcessor crop [input filename and location] [output foldername] [filename] bmp 100 100 (height width)
                //TODO: make sure dimensions are less than image size, this is not scaling
                if (args.Length >= 7)
                {
                    var fileLocation = args[1];
                    var outputFolder = args[2];
                    var outputFilename = args[3];
                    var fileFormat = args[4];
                    var height = Convert.ToInt32(args[5]);
                    var width = Convert.ToInt32(args[6]);

                    var fileExtension = Path.GetExtension(fileLocation);
                    var bmpImage = Helpers.OpenImageFile(fileExtension, fileLocation);

                    if (bmpImage != null)
                    {
                        var croppedImage = Helpers.CropImage(bmpImage, height, width);

                        //save as required format
                        Helpers.SaveImage(croppedImage, "RGB", fileFormat, outputFolder, outputFilename);

                        Console.WriteLine("Completed Operation, Image Cropped.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Missing Inputs In Cropping.");
                    UnknownArgs = true;
                }
            }
            else if (args.Length > 0 && args[0].ToLower().Equals("compare"))
            {
                //USAGE: //ImageProcessor compare [input filename and location of image 1] [input filename and location of image 2]
                var file1Location = args[1];
                var file1Extension = Path.GetExtension(file1Location);
                var file2Location = args[2];
                var file2Extension = Path.GetExtension(file2Location);

                Console.WriteLine("\nImage 1: " + file1Location);
                Console.WriteLine("Image 2: " + file2Location + "\n");

                var bmpImage1 = Helpers.OpenImageFile(file1Extension, file1Location);
                var bmpImage2 = Helpers.OpenImageFile(file2Extension, file2Location);

                Helpers.CompareTwoImages(bmpImage1, bmpImage2);
            }
            else
            {
                Console.WriteLine("Error: Missing Inputs In First Argument.");
                UnknownArgs = true;
            }

            if (UnknownArgs || args.Length == 0)
            {
                //help
                PrintHelp();
            }
        }

        //only checks if they exist, corectness is done when saving etc ...
        private static bool AreAllInputsThere(string[] args, bool requireNoImg)
        {
            //arg 0 has been checked by main
            if (args.Length < 7)
            {
                return false; //dont need to continue
            }

            if (args.Length >= 7)
            {
                if (string.IsNullOrWhiteSpace(args[1]))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(args[2]))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(args[3]))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(args[4]))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(args[5]))
                {
                    return false;
                }
                if (string.IsNullOrWhiteSpace(args[6]))
                {
                    return false;
                }

                if (requireNoImg)
                {
                    if (string.IsNullOrWhiteSpace(args[7]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private static void PrintHelp()
        {
            //TODO: JMC Make sure it does not wrap
            var helpText = "Processes Images for Use in Applications Such as Deep Learning.\n\n";
            helpText += "ImageProcessor [batch] [batch size]" +
                        " [convert][rndImage][scale][crop][compare] [input filename and location]\n" +
                        "  [rgba][rgb][r][g][b][bw][gs] [output foldername] [filename]\n" +
                        "  [png][jpg][jpeg][bmp][ascii] [height] [width] [numberImages]";
            helpText += "\n\nSwitches:\n";
/*
            helpText += "\t convert \t\t This will just convert an image from one format to another. Also split the image if thats whats chosen.\n";
            helpText += "\t merge \t\t\t This will merge back something previously split up. \n";
            helpText += "\t rndImage \t\t This will generate a noisey image randomly. Must use inputFolder. \n";
            
            helpText += "\t folderName \t\t This specifies the input folder path, cannot be used in conjuction with input. Has to be used with rndImage. \n";
            helpText += "\t filename \t\t This specifies the input filename. (Use $Many to make program open all files in folder, make sure only images are in specified folder) \n";
            
            helpText += "\t rgb/r/g/b/bw/gs \t This Specifies the colours to use in one of the methods. Only one can be chosen at a time. \n";

            helpText += "\t output \t\t This specifies the output folder (Will use input filenames). \n";
            
            helpText += "\t png/jpg/jpeg/bmp/ascii  This specifies the output type. ASCII will produce a file using an ASCII based format with the first line containing id info. This is saved as a .dat but its just a text file. \n";

            helpText += "\t height \t\t This specifies the height that the image must conform to. For merge and split this will internally call the crop function. \n";
            helpText += "\t width \t\t\t This specifies the width that the image must conform to. Fpr merge and split this will internally call the crop function. \n";
            
            helpText += "\t numberImages \t\t Only used for rndImage. Defines number of images to produce. Will be asked by the program during operation. \n";*/
            helpText += "Note: Batch does not work with rndImage. \n";
            //TODO: JMC Update and fix these Exmaples
            helpText += "\nUsage Examples:\n\n";
            helpText +=
                "ImageProcessor convert [input filename and location] rgb [output foldername] [filename] bmp 100 100 \n";
            helpText += "ImageProcessor rndImage rgb [output foldername] [filename] png 100 100 1 \n";
            helpText +=
                "ImageProcessor scale [input filename and location] [output foldername] [filename] bmp 100 100 <(height width)> \n";
            helpText +=
                "ImageProcessor crop [input filename and location] [output foldername] [filename] bmp 100 100 <(height width)> \n";
            helpText +=
                "ImageProcessor compare [input filename and location of image 1] [input filename and location of image 2] \n";
            helpText += "ImageProcessor batch [input folder] [output folder] scale bmp 100 100 <(height width)> \n";

            Console.Out.WriteLine(helpText);
        }
    }
}
