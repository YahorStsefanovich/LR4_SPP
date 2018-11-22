namespace TestsGeneratorLibrary
{
     public class Config
     {
          private int readingTasksCount;
          private int processingTasksCount;
          private int writingTasksCount;

          public int ReadingTasksCount
          {
               get { return readingTasksCount; }
               set { readingTasksCount = value; }
          }

          public int ProcessingTasksCount
          {
               get { return processingTasksCount; }
               set { processingTasksCount = value; }
          }

          public int WritingTasksCount
          {
               get { return writingTasksCount; }
               set { writingTasksCount = value; }
          }

          public Config(int readingTasksCount, int processingTasksCount, int writingTasksCount)
          {
               this.readingTasksCount = readingTasksCount;
               this.processingTasksCount = processingTasksCount;
               this.writingTasksCount = writingTasksCount;
          }
     }
}
