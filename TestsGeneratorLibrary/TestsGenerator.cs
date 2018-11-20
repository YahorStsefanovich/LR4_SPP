using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestsGeneratorLibrary.Structure;

namespace TestsGeneratorLibrary
{
     public class TestsGenerator
     {
          private readonly Config config;

          public TestsGenerator(Config config)
          {
               this.config = config;
          }

          public Task Generate(Reader reader, Writer writer, List<string> source)
          {
               DataflowLinkOptions linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

               ExecutionDataflowBlockOptions processingTaskRestriction = new ExecutionDataflowBlockOptions {
                    MaxDegreeOfParallelism = config.MaxProcessingTasksCount
               };

               ExecutionDataflowBlockOptions outputTaskRestriction = new ExecutionDataflowBlockOptions
               {
                    MaxDegreeOfParallelism = config.MaxWritingTasksCount
               };

               TransformBlock<string, string> readingBlock =
                    new TransformBlock<string, string>(new Func<string, Task<string> >(reader.ReadAsync), processingTaskRestriction);
               TransformBlock<string, TestClassStructure> producingBlock =
                new TransformBlock<string, TestClassStructure>(new Func<string, TestClassStructure>(Produce), processingTaskRestriction);

               ActionBlock<TestClassStructure> writingBlock = new ActionBlock<TestClassStructure>(
                    ((generatedClass)=>writer.WriteAsync(generatedClass).Wait()), outputTaskRestriction);

               readingBlock.LinkTo(producingBlock, linkOptions);
               producingBlock.LinkTo(writingBlock, linkOptions);

               foreach (string path in source)
               {
                    readingBlock.SendAsync(path);
               }

               readingBlock.Complete();

               return writingBlock.Completion;
          }

          private TestClassStructure Produce(string sourceCode)
          {
               TreeBuilder treeBuilder = new TreeBuilder(sourceCode);
               TreeStructure treeStructure = treeBuilder.GetTreeStructure();

               TestClassTemplate testTemplatesGenerator = new TestClassTemplate(treeStructure);
               List<TestClassStructure> testTemplates = testTemplatesGenerator.GetTestTemplates().ToList();

               return new TestClassStructure(testTemplates.First().TestClassName, testTemplates.First().TestClassData); 
          }
     }
}
