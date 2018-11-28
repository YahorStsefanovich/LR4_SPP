using System;
using System.Collections.Generic;
using TestsGeneratorLibrary.Structure;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace TestsGeneratorLibrary
{
     internal class TestClassTemplate
     {
          private readonly TreeStructure treeStructure;

          public TestClassTemplate(TreeStructure treeStructure)
          {
               this.treeStructure = treeStructure;
          }

          public List<TestClassStructure> GetTestTemplates()
          {
               List<TestClassStructure> testTemplates = new List<TestClassStructure>();

               foreach (ClassInfo classInfo in treeStructure.Classes)
               {
                    CompilationUnitSyntax compilationUnit = CompilationUnit()
                         //используемые директивы
                         .WithUsings(GetUsingDirectives(classInfo))
                         //namespace
                         .WithMembers(SingletonList<MemberDeclarationSyntax>(GetNamespaceDeclaration(classInfo)
                         //имена тестовых классов
                         .WithMembers(SingletonList<MemberDeclarationSyntax>(ClassDeclaration(classInfo.ClassName + "Tests")
                              .WithAttributeLists(
                                   SingletonList<AttributeListSyntax>(
                                   AttributeList(
                                        SingletonSeparatedList<AttributeSyntax>(
                                             Attribute(
                                                  IdentifierName("TestClass"))))))
                              //модификаторы доступа
                              .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                              //публичные методы
                              .WithMembers(GetMembersDeclarations(classInfo))))
                        )
                     );


                    string fileName = String.Format("{0}Test.cs.test", classInfo.ClassName);
                    string fileData = compilationUnit.NormalizeWhitespace().ToFullString();

                    testTemplates.Add(new TestClassStructure(fileName, fileData));
               }

               return testTemplates;
          }

          private NamespaceDeclarationSyntax GetNamespaceDeclaration(ClassInfo classInfo)
          {
               NamespaceDeclarationSyntax namespaceDeclaration = 
                    NamespaceDeclaration(QualifiedName(
                         IdentifierName(classInfo.NamespaceName), IdentifierName("Tests")));

               return namespaceDeclaration;
          }

          private SyntaxList<MemberDeclarationSyntax> GetMembersDeclarations(ClassInfo classInfo)
          {
               List<MemberDeclarationSyntax> methods = new List<MemberDeclarationSyntax>();

               methods.Add(GetMethodDeclaration("Initialize", "TestInitialize", new List<StatementSyntax>()));
               foreach (MethodInfo method in classInfo.Methods)
               {
                    List<StatementSyntax> bodyMembers = new List<StatementSyntax>();
                    bodyMembers.Add(
                    ExpressionStatement(
                         InvocationExpression(
                              GetAssertFail())
                                   .WithArgumentList(GetMemberArgs())));
                    methods.Add(GetMethodDeclaration(method.Name + "Test", "TestMethod", bodyMembers));
               }
               return new SyntaxList<MemberDeclarationSyntax>(methods);
          }       

          private MemberDeclarationSyntax GetMethodDeclaration(String methodName, String atribute, List<StatementSyntax> bodyMembers )
          {              
               MethodDeclarationSyntax methodDeclaration = MethodDeclaration(
                    PredefinedType(
                         Token(SyntaxKind.VoidKeyword)),
                              Identifier(methodName))
                                   .WithAttributeLists(
                                        SingletonList<AttributeListSyntax>(
                              AttributeList(
                                   SingletonSeparatedList<AttributeSyntax>(
                                        Attribute(
                                             IdentifierName(atribute))))))
                              .WithModifiers(TokenList(Token(SyntaxKind.PublicKeyword)))
                              .WithBody(Block(bodyMembers));

               return methodDeclaration;
          }

          private ArgumentListSyntax GetMemberArgs()
          {
               ArgumentListSyntax args = ArgumentList(
                   SingletonSeparatedList(
                       Argument(
                           LiteralExpression(
                               SyntaxKind.StringLiteralExpression,
                               Literal("Generated")))));

               return args;
          }

          private MemberAccessExpressionSyntax GetAssertFail()
          {
               MemberAccessExpressionSyntax assertFail = MemberAccessExpression(
                   SyntaxKind.SimpleMemberAccessExpression,
                   IdentifierName("Assert"),
                   IdentifierName("Fail"));

               return assertFail;
          }

          private SyntaxList<UsingDirectiveSyntax> GetUsingDirectives(ClassInfo classInfo)
          {
               List<UsingDirectiveSyntax> usingDirectives = new List<UsingDirectiveSyntax>();

               usingDirectives.Add(UsingDirective(IdentifierName("System")));
               usingDirectives.Add(UsingDirective(IdentifierName("Microsoft.VisualStudio.TestTools.UnitTesting")));
               usingDirectives.Add(UsingDirective(IdentifierName(classInfo.NamespaceName)));

               return new SyntaxList<UsingDirectiveSyntax>(usingDirectives);
          }
     }
}
