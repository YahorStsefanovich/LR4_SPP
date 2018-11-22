using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGeneratorLibrary;

namespace LR4_SPP
{
     class Program
     {
          static void Main(string[] args)
          {
               string outputPath = "D:\\Tests";
               string path = "D:\\LR\\sem5(NOW)\\SPP\\LR4_SPP\\LR4_SPP\\TestsGeneratorLibrary\\Writer.cs";

               List<string> pathes = new List<string>();
               pathes.Add(path);

               Config config = new Config(3, 3, 3);
               TestsGenerator generator = new TestsGenerator(config);

               generator.Generate(new Reader(), new Writer(outputPath), pathes).Wait();
          }
     }
}
