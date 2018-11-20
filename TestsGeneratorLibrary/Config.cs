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
