using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileDeleter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("No arguments provided");
                return;
            }

            string fileType = string.Empty;
            string folderPath = string.Empty;
            int daysBack = 0;
            DateTime? deletedFileDate = null;

            int flagCount = 0;

            for (int i = 0; i < args.Length; i++)
            {
                string curr = args[i].ToLower();
                switch (curr)
                {
                    case "-t":
                        fileType = args[i + 1];
                        flagCount++;
                        i++;
                        break;
                    case "-p":
                        folderPath = args[i + 1];
                        flagCount++;
                        i++;
                        break;
                    case "-d":
                        if (args[i + 1].Contains('/'))
                        {
                            deletedFileDate = Convert.ToDateTime(args[i + 1]);
                        }
                        else
                        {
                            deletedFileDate = DateTime.Now.AddDays(-daysBack);
                        }
                        flagCount++;
                        i++;
                        break;
                    default:
                        break;
                }
            }

            if (flagCount != 3)
            {
                Console.WriteLine("Missing required flags");
                return;
            }

            string[] files = Directory.GetFiles(folderPath, "*." + fileType);
            foreach (string file in files)
            {
                DateTime fileCreationDate = File.GetCreationTime(file);
                if (fileCreationDate <= deletedFileDate)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
