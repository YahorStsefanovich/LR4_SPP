using System.Collections.Generic;

namespace TestsGeneratorLibrary.Structure
{
     public class TreeStructure
     {
          public IEnumerable<ClassInfo> Classes { get; }

          public TreeStructure(IEnumerable<ClassInfo> classes)
          {
               Classes = classes;
          }
     }
}
