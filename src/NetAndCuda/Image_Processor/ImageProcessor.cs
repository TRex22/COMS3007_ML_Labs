﻿using System;
using System.Reflection;

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
     * RGB / RGBA problem
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
                throw new NotImplementedException();
                //open file/s

                //crop

                //check if ascii
                //run merge

                //convert file

                //save file
            }
            else if (args.Length > 0 && args[0].ToLower().Equals("merge"))
            {
                throw new NotImplementedException();
                //open file/s specified

                //check if ascii
                //get values

                //run convert

                //save
            }
            else if (args.Length > 0 && args[0].ToLower().Equals("rndImage"))
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
                    
                    //TODO: check if folder is actually real, no input foldername
                    System.IO.Directory.CreateDirectory(args[4]);

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
                        helpers.SaveImage(convertedImage, outputFormat, outputFolder, filename);
                    }
                    Console.WriteLine("Completed Operation, %s random images created.", noImg);
                }
                else
                {
                    Console.WriteLine("Error: Missing Inputs.");
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
            if (args.Length < 8)
            {
                return false; //dont need to continue
            }

            else //(args.Length >= 8)
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
                }
                if (String.IsNullOrWhiteSpace(args[7]))
                {
                    return false;
                }
                if (requireNoImg)
                {
                    if (String.IsNullOrWhiteSpace(args[8]))
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
            helpText += "ImageProcessor [convert][merge][rndImage] [folderName] [filename]\n" +
                "  [rgba][rgb][r][g][b][bw][gs] [output]\n" +
                "  [png][jpg][jpeg][bmp][ascii] [height] [width] [numberImages]";
            helpText += "\n\nSwitches:\n";

            helpText += "\t convert \t\t This will just convert an image from one format to another. Also split the image if thats whats chosen.\n";
            helpText += "\t merge \t\t\t This will merge back something previously split up. \n";
            helpText += "\t rndImage \t\t This will generate a noisey image randomly. Must use inputFolder. \n";
            
            helpText += "\t folderName \t\t This specifies the input folder path, cannot be used in conjuction with input. Has to be used with rndImage. \n";
            helpText += "\t filename \t\t This specifies the input filename. (Use $Many to make program open all files in folder, make sure only images are in specified folder) \n";
            
            helpText += "\t rgb/r/g/b/bw/gs \t This Specifies the colours to use in one of the methods. Only one can be chosen at a time. \n";

            helpText += "\t output \t\t This specifies the output folder (Will use input filenames). \n";
            
            helpText += "\t png/jpg/jpeg/bmp/ascii  This specifies the output type. ASCII will produce a file using an ASCII based format with the first line containing id info. \n";

            helpText += "\t height \t\t This specifies the height that the image must conform to. For merge and split this will internally call the crop function. \n";
            helpText += "\t width \t\t\t This specifies the width that the image must conform to. Fpr merge and split this will internally call the crop function. \n";
            
            helpText += "\t numberImages \t\t Only used for rndImage. Defines number of images to produce. Will be asked by the program during operation. \n";

            //TODO: JMC Update and fix these Exmaples

            //ImageProcessor convert [input filename] rgb output [output filename] bmp 100 100
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
