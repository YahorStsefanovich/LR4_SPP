using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
     public class Writer
     {
          private readonly string pathToOutputDirectory;

          public Writer(string pathToOutputDirectory)
          {
               this.pathToOutputDirectory = pathToOutputDirectory;
               if (!Directory.Exists(pathToOutputDirectory))
               {
                    Directory.CreateDirectory(pathToOutputDirectory);
               }
          }

     }
}
