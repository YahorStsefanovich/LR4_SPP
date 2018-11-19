using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestsGeneratorLibrary.Structure;

namespace TestsGeneratorLibrary
{
    public class TestClassTemplate
    {
          private readonly TreeStructure treeStructure;

          public TestClassTemplate(TreeStructure treeStructure)
          {
               this.treeStructure = treeStructure;
          }
    }
}
