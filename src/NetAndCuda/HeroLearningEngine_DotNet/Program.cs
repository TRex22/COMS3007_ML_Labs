using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HeroLearningEngine_DotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hero Learning Engine C# Version by Jason Chalom 2016, Version " + Assembly.GetExecutingAssembly().GetName().Version);
            Console.WriteLine("Test version using external neural net library.");



            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
        }
    }
}
