using System.Collections.Generic;

namespace TestsGeneratorLibrary.Structure
{
     public class ClassInfo
     {
          public string NamespaceName { get; }
          public string ClassName { get; }
          public List<MethodInfo> Methods { get; }

          public ClassInfo(string namespaceName, string className, List<MethodInfo> methods)
          {
               NamespaceName = namespaceName;
               ClassName = className;
               Methods = methods;
          }
     }
}
