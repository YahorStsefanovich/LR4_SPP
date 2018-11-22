using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace TestsGeneratorLibrary.Structure
{
     public class TreeBuilder
     {
          private readonly string fileText;

          public TreeBuilder(string fileText)
          {
               this.fileText = fileText;
          }

          public TreeStructure GetTreeStructure()
          {
               SyntaxTree programSyntaxTree = CSharpSyntaxTree.ParseText(fileText);
               //корень синтаксического дерева
               CompilationUnitSyntax root = programSyntaxTree.GetCompilationUnitRoot();
               return new TreeStructure(GetClasses(root));
          }

          private List<ClassInfo> GetClasses(CompilationUnitSyntax root)
          {
               List<ClassInfo> classes = new List<ClassInfo>();

               foreach (ClassDeclarationSyntax classDeclaration in root.DescendantNodes().OfType<ClassDeclarationSyntax>())
               {
                    string namespaceName = ((NamespaceDeclarationSyntax)classDeclaration.Parent).Name.ToString();
                    string className = classDeclaration.Identifier.ValueText;
                    classes.Add(new ClassInfo(namespaceName, className, GetMethods(classDeclaration)));
               }

               return classes;
          }

          private List<MethodInfo> GetMethods(ClassDeclarationSyntax classDeclaration)
          {
               List<MethodInfo> methods = new List<MethodInfo>();

               foreach (MethodDeclarationSyntax methodDeclaration in 
                    classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>()
                .Where((methodDeclaration) => 
                methodDeclaration.Modifiers.Any((modifier) => 
                modifier.IsKind(SyntaxKind.PublicKeyword))))
               {
                    string methodName = methodDeclaration.Identifier.ValueText;
                    methods.Add(new MethodInfo(methodName));
               }

               return methods;
          }
     }
}
