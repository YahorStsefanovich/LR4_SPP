using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
     public class Config
     {
          public int MaxProcessingTasksCount { get; }
          public int MaxWritingTasksCount { get; }

          public Config(int processingTasksCount, int writingTasksCount)
          {
               MaxProcessingTasksCount = processingTasksCount;
               MaxWritingTasksCount = writingTasksCount;
          }
     }
}
