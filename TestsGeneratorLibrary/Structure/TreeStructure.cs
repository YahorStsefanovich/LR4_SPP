using System.Collections.Generic;

namespace TestsGeneratorLibrary.Structure
{
     public class TreeStructure
     {
          private List<ClassInfo> classes;
          public List<ClassInfo> Classes { get; }

          public TreeStructure(List<ClassInfo> classes)
          {
               this.Classes = classes;
          }
     }
}
