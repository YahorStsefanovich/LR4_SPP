using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGeneratorLibrary.Structure;

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

          public async Task WriteAsync(TestClassStructure generatedCode)
          {
               string filePath = String.Format("{0}\\{1}",  pathToOutputDirectory, generatedCode.TestClassName);
               using (StreamWriter streamWriter = new StreamWriter(filePath))
               {
                    await streamWriter.WriteAsync(generatedCode.TestClassData);
               }
          }
     }
}
