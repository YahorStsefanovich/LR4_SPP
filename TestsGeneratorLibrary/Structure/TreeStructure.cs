using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
