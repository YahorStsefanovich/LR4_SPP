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
               int maxProcessing = 3;
               int maxWrite = 3;

               string outputPath = "D:\\Tests";
               string path = "D:\\LR\\sem5(NOW)\\SPP\\LR4_SPP\\LR4_SPP\\TestsGeneratorLibrary\\Writer.cs";

               List<string> list = new List<string>();
               list.Add(path);

               Config config = new Config(maxProcessing, maxWrite);
               TestsGenerator generator = new TestsGenerator(config);

               generator.Generate(new Reader(), new Writer(outputPath), list).Wait();
          }
     }
}
