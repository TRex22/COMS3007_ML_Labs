using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessor
{
    /*
     TODO:
     * Cropper
     *  T/B
     *  L/R
     *  size - check if possible
     *  
     * split image colors
     * b/w
     * greyscale
     * convert single into nodes -first line is config
     * convert triple into nodes -first line is config
     * generate noisey image
     * convert to bmp, png, jpg, jpeg
     * convert to ascii
     * convert from ascii ie nodes to image either RGB or just one
     * 
     * Another class library
     * Open image into memory array

        6 layers 5 required - for switches
     
     */
    public class ImageProcessor
    {
        public static void Main(string[] args)
        {
            bool UnknownArgs = true;
            
            Console.Out.WriteLine("Image Processor Commandline Application by Jason Chalom 2016, Version "+ Assembly.GetExecutingAssembly().GetName().Version);

            if (args.Length > 0 && args[0].ToLower().IndexOf("split", StringComparison.Ordinal) >= 0)
            {
                throw new NotImplementedException();
            }
            else if (args.Length > 0 && args[0].ToLower().IndexOf("merge", StringComparison.Ordinal) >= 0)
            {
                throw new NotImplementedException();
            }
            else if (args.Length > 0 && args[0].ToLower().IndexOf("rndImage", StringComparison.Ordinal) >= 0)
            {
                bool areAllInputsThere = AreAllInputsThere(args, true);

                if (areAllInputsThere)
                {
                    //check if folder
                    //if so run for all items in folde
                }
                else
                {
                    Console.WriteLine("Error: Missing Inputs.");
                    UnknownArgs = true;
                }
            }
            else if (args.Length > 0 && args[0].ToLower().IndexOf("convert", StringComparison.Ordinal) >= 0)
            {
                throw new NotImplementedException();
            }

            if (UnknownArgs || args.Length == 0 || args[0].ToLower().IndexOf("?", StringComparison.Ordinal) >= 0 || args[0].ToLower().IndexOf("help", StringComparison.Ordinal) >= 0)
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

            if (args.Length < 6)
            {
                return false; //dont need to continue
            }

            if (args.Length >= 6)
            {
                //check input file / folder exists 1
                if (String.IsNullOrWhiteSpace(args[1]))
                {
                    return false;
                }
                //check if colourOption exists 2
                if (String.IsNullOrWhiteSpace(args[2]))
                {
                    return false;
                }
                //check if output / folder exists 3
                if (String.IsNullOrWhiteSpace(args[3]))
                {
                    return false;
                }
                //check for output format 4
                if (String.IsNullOrWhiteSpace(args[4]))
                {
                    return false;
                }
                //check height 5
                if (String.IsNullOrWhiteSpace(args[5]))
                {
                    return false;
                }
                //check width 6
                if (String.IsNullOrWhiteSpace(args[6]))
                {
                    return false;
                }

                //check number images if required 7
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
            String helpText = "Processes Images for Use in Applications Such as Deep Learning.\n\n";
            helpText += "ImageProcessor [split][merge][rndImage][convert] [input filename][inputFolder folderName]\n"+
                "  [rgb][r][g][b][bw][gs] [output filename][outputFolder foldername]\n" +
                "  [png][jpg][jpeg][bmp][ascii] [height] [width] [namescheme]";
            helpText += "\n\nSwitches:\n";

            helpText += "\t split \t\t\t This will split the image into RGB or B/W or Greyscale. \n";
            helpText += "\t merge \t\t\t This will merge back something previously split up. \n";
            helpText += "\t rndImage \t\t This will generate a noisey image randomly. \n";
            helpText += "\t convert \t\t This will just convert an image from one format to another. \n";

            helpText += "\t input \t\t\t This specifies the input file, cannot be used in conjuction with inputFolder. \n";
            helpText += "\t inputFolder \t\t This specifies the input folder path, cannot be used in conjuction with input. \n";

            helpText += "\t rgb/r/g/b/bw/gs \t This Specifies the colours to use in one of the methods. Only one can be chosen at a time. \n";

            helpText += "\t output \t\t This specifies the output file, cannot be used in conjuction with outputFolder. (inputFolder has to be used with outputFolder). \n";
            helpText += "\t outputFolder \t\t This specifies the output folder, cannot be used in conjuction with output. (inputFolder has to be used with outputFolder). \n";

            helpText += "\t png/jpg/jpeg/bmp/ascii  This specifies the output type. ASCII will produce a file using an ASCII based format with the first line containing id info. \n";

            helpText += "\t height \t\t This specifies the height that the image must conform to. For merge and split this will internally call the crop function. \n";
            helpText += "\t width \t\t\t This specifies the width that the image must conform to. Fpr merge and split this will internally call the crop function. \n";

            helpText += "\t namescheme \t\t Defines the namescheme for the output files. A single file will contain that name. If nonw is specified defaults will be used. \n";

            helpText += "\t numberImages \t\t Only used for rndImage. Defines number of images to produce. Will be asked by the program during operation. \n";

            //TODO: JMC Update and fix these Exmaples
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
