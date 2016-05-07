using System;
using System.Reflection;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageProcessor
{
    /*
     * Jason Chalom 2016, Image Processor
     * Provide reference to this project when using it for research
     * 
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
     * TODO: add scaling functionality
     * TODO: add more file formats
     * TODO: convert from ASCII
     * TODO: add filters
     * TODO: add inversion of colours -simple enough
     * TODO: proper error messages
     * RGB / RGBA problem
     * 
     * ASCII Channels will always be stored per column/row (depeneding on future updates to code) in the RGBA way
     * i.e. rg (or gr) will have red channel first then green or RA will be red then alpha
     */

    public class ImageProcessor
    {
        public static void Main(string[] args)
        {
            var helpers = new ImageProcessorHelpers();

            bool UnknownArgs = false;
            Console.Out.WriteLine("Image Processor Commandline Application by Jason Chalom 2016, Version "+ Assembly.GetExecutingAssembly().GetName().Version);
            
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

                Bitmap bmpImage = null;

                //open file/s
                if (fileExtension != null && fileExtension.Contains(".dat"))
                {
                    bmpImage = helpers.LoadAsciiFile(fileLocation);
                }
                else if (fileExtension != null) //try an image
                {
                    var image = Image.FromFile(fileLocation, true); //TODO add failure if fail to find 
                    bmpImage = new Bitmap(image);//TODO optimise
                }

                //crop - not in this version

                //check if ascii - not in this version
                //run merge - not in this version

                if (bmpImage != null)
                {
                    //split if required
                    var convertedImage = helpers.ConvertImageColourScale(bmpImage, colourType);

                    //save as required format
                    helpers.SaveImage(convertedImage, colourType, outputFormat, outputFolder, outputFilename);

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

                bool areAllInputsThere = AreAllInputsThere(args, true);
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

                    int noImg = Convert.ToInt32(args[7]); // will fail if not int
                    int height = Convert.ToInt32(args[5]);
                    int width = Convert.ToInt32(args[6]);

                    //create rnd images
                    for (int i = 0; i < noImg; i++)
                    {
                        var rndImage = helpers.CreateRndImage(height, width);
                        
                        //split if required
                        var convertedImage = helpers.ConvertImageColourScale(rndImage, colourType);

                        //save as required format
                        var filenameNumbered = String.Format("{0}_{1}", filename, i+1);
                        helpers.SaveImage(convertedImage, colourType, outputFormat, outputFolder, filenameNumbered);
                    }
                    Console.WriteLine("Completed Operation, {0} random images created.", noImg);
                }
                else
                {
                    Console.WriteLine("Error: Missing Inputs in rndImage.");
                    UnknownArgs = true;
                }
            }
            else
            {
                Console.WriteLine("Error: Missing Inputs.");
                UnknownArgs = true;
            }

            if (UnknownArgs || args.Length == 0)
            {
                //help
                PrintHelp();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }

        //only checks if they exist, corectness is done when saving etc ...
        private static bool AreAllInputsThere(string[] args, bool requireNoImg)
        {
            //arg 0 has been checked by main
            if (args.Length < 7)
            {
                return false; //dont need to continue
            }

            else if (args.Length >= 7)
            {
                if (String.IsNullOrWhiteSpace(args[1]))
                {
                    return false;
                }
                if (String.IsNullOrWhiteSpace(args[2]))
                {
                    return false;
                }
                if (String.IsNullOrWhiteSpace(args[3]))
                {
                    return false;
                }
                if (String.IsNullOrWhiteSpace(args[4]))
                {
                    return false;
                }
                if (String.IsNullOrWhiteSpace(args[5]))
                {
                    return false;
                }
                if (String.IsNullOrWhiteSpace(args[6]))
                {
                    return false;
                }/*
                if (String.IsNullOrWhiteSpace(args[7]))
                {
                    return false;
                }*/
                if (requireNoImg)
                {
                    if (String.IsNullOrWhiteSpace(args[7]))
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
            String helpText = "Processes Images for Use in Applications Such as Deep Learning.\n\n";
            helpText += "ImageProcessor [convert][rndImage] [folderName] [filename]\n" +
                "  [rgba][rgb][r][g][b][bw][gs] [output]\n" +
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

            //TODO: JMC Update and fix these Exmaples

            //ImageProcessor convert [input filename and location] rgb [output foldername] [filename] bmp 100 100
            //ImageProcessor rndImage rgb [output foldername] [filename] png 100 100 1

            /*helpText += "\nUsage Examples:\n\n";
            helpText += "ImageProcessor split RGB input [input filename] b output [output filename] ascii\n";
            helpText += "ImageProcessor split BW inputFolder [input foldername] b output [output foldername] png\n\n";
            helpText += "ImageProcessor merge RGB input [input filename] b output [output filename] ascii\n";
            helpText += "ImageProcessor merge BW inputFolder [input foldername] b output [output foldername] png\n\n";
            helpText += "ImageProcessor rndImage [number of files] output [output foldername] name [namescheme]\n\n";
            helpText += "ImageProcessor convert [input filename] output [output filename] bmp\n";
            helpText += "ImageProcessor convert [input filename] output [output filename] bmp file1\n";*/
            Console.Out.WriteLine(helpText);
        }

    }
}
