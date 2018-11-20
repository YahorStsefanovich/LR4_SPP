namespace TestsGeneratorLibrary.Structure
{
     public class TestClassStructure
     {
          public string TestClassName { get; }
          public string TestClassData { get; }

          public TestClassStructure(string className, string classData)
          {
               TestClassName = className;
               TestClassData = classData;
          }
     }
}
