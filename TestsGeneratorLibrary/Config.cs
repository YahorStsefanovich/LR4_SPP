namespace TestsGeneratorLibrary
{
     public class Config
     {
          private int readingTasksCount;
          private int processingTasksCount;
          private int writingTasksCount;

          public int ReadingTasksCount
          {
               get
               {
                    return readingTasksCount;
               }
          }

          public int ProcessingTasksCount
          {
               get
               {
                    return processingTasksCount;
               }
          }

          public int WritingTasksCount
          {
               get
               {
                    return writingTasksCount;
               }
          }

          public Config(int readingTasksCount, int processingTasksCount, int writingTasksCount)
          {
               this.ReadingTasksCount = readingTasksCount;
               this.ProcessingTasksCount = processingTasksCount;
               this.WritingTasksCount = writingTasksCount;
          }
     }
}
