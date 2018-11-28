using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestsGeneratorLibrary;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Tests
{
     [TestClass]
     public class UnitTests
     {
          private CompilationUnitSyntax root;

          [TestInitialize]
          public void Initialize()
          {
               string pathToTests = Path.GetFullPath(@"..\Tests");
               string outputPath = pathToTests + @"\GeneratedTests";

               List<string> pathes = new List<string>();
               pathes.Add(pathToTests + @"\UnitTests.cs");

               TestsGenerator generator = new TestsGenerator(new Config(3, 3, 3));
               generator.Generate(new Reader(), new Writer(outputPath), pathes).Wait();

               string sourceCode = File.ReadAllText(outputPath + @"\UnitTestsTest.cs.test");
               root = CSharpSyntaxTree.ParseText(sourceCode).GetCompilationUnitRoot();
          }

          [TestMethod]
          public void CheckDirectives()
          {
               Assert.AreEqual("System", root.Usings[0].Name.ToString());
               Assert.AreEqual("Microsoft.VisualStudio.TestTools.UnitTesting", root.Usings[1].Name.ToString());
               Assert.AreEqual("Tests", root.Usings[2].Name.ToString());
          }

          [TestMethod]
          public void CheckNamecpace()
          {
               IEnumerable<NamespaceDeclarationSyntax> namespaces = root.DescendantNodes().OfType<NamespaceDeclarationSyntax>();

               Assert.AreEqual(1, namespaces.Count());
               Assert.AreEqual("Tests.Tests", namespaces.ElementAt<NamespaceDeclarationSyntax>(0).Name.ToString());
          }

          [TestMethod]
          public void CheckClasses()
          {
               IEnumerable<ClassDeclarationSyntax> classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

               Assert.AreEqual(1, classes.Count());
               Assert.AreEqual("UnitTestsTests", classes.ElementAt<ClassDeclarationSyntax>(0).Identifier.ToString());
          }

          [TestMethod]
          public void TestClassAtribute()
          {
               IEnumerable<ClassDeclarationSyntax> classes = root.DescendantNodes().OfType<ClassDeclarationSyntax>();

               Assert.AreEqual(1, classes.ElementAt<ClassDeclarationSyntax>(0).AttributeLists.Count);
               Assert.AreEqual("TestClass", classes.ElementAt<ClassDeclarationSyntax>(0).AttributeLists[0].Attributes[0].Name.ToString());
          }

          [TestMethod]
          public void CheckMethods()
          {
               IEnumerable<MethodDeclarationSyntax> methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

               Assert.IsTrue(methods.Count() > 1);
               Assert.AreEqual("InitializeTest", methods.ElementAt<MethodDeclarationSyntax>(1).Identifier.ToString());
          }

          [TestMethod]
          public void TestMethodAttributes()
          {
               IEnumerable<MethodDeclarationSyntax> methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
               Assert.AreEqual("TestInitialize", methods.ElementAt(0).AttributeLists[0].Attributes[0].Name.ToString());
               for (int i = 1; i < methods.Count(); i++)
               {
                    Assert.AreEqual("TestMethod", methods.ElementAt(i).AttributeLists[0].Attributes[0].Name.ToString());
               }
               
          }

          [TestMethod]
          public void AssertFailTest()
          {
               IEnumerable<MethodDeclarationSyntax> methods = root.DescendantNodes().OfType<MethodDeclarationSyntax>();
               int actual = methods.ElementAt<MethodDeclarationSyntax>(1).Body.Statements.OfType<ExpressionStatementSyntax>().Where((statement) => statement.ToString().Contains("Assert.Fail")).Count();
               Assert.AreEqual(1, actual);
          }
     }
}
