using Microsoft.VisualStudio.Coverage.Analysis;
using System;
using System.IO;

namespace CoverageConverter
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (args == null || args.Length != 2 )
            {
                Console.WriteLine("Coverage Convert - reads VStest binary code coverage data, and outputs it in XML format.");
                Console.WriteLine("Usage:  ConverageConvert <sourcepath> <destinationfile>");
                return 1;
            }

            const string searchPattern = "*.coverage";
            Console.WriteLine($"searching all directories in {args[0]} for files matching: {searchPattern}");
            //check if directory exists
            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine($"Error opening directory or missing file: {args[0]}");
                return 1;
            }

            foreach (var item in Directory.GetFiles(args[0], searchPattern, SearchOption.AllDirectories))
            {
                Console.WriteLine($"Processing {item}");
                var data = GetCoverageDsFromFile(item);
                if (data == null)
                {
                    return 1;
                }

                if (ConvertCoverageDsToXml(data, args[1]) > 1)
                    return 1;


            }
          
            return 0;
        }


        private static CoverageDS GetCoverageDsFromFile(string codeCoverageFilePath)
        {
            try
            {
                var path = Path.GetDirectoryName(codeCoverageFilePath);
                var info = CoverageInfo.CreateFromFile(codeCoverageFilePath, new[] { path }, new string[] { });
                var data = info.BuildDataSet();
                return data;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error opening coverage data: {e.Message}");
                return null;
            }
            
            
        }

        private static int ConvertCoverageDsToXml(CoverageDS data, string outputFilePath)
        {
            try
            {
                data.WriteXml(outputFilePath);
            }
            catch (Exception e)
            {
                Console.WriteLine($"sError writing to output file: {e.Message}");
                return 1;
                
            }

            return 0;

        }


    }
}
