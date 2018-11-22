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

          public async Task Generate(Reader reader, Writer writer, List<string> source)
          {
               //для уведомления о завершении считывания
               DataflowLinkOptions linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

               //количество одновременно считываемых файлов
               ExecutionDataflowBlockOptions maxReading = new ExecutionDataflowBlockOptions
               {
                    MaxDegreeOfParallelism = config.ReadingTasksCount
               };

               //количество одновременно обрабатываемых файлов
               ExecutionDataflowBlockOptions maxProcessing = new ExecutionDataflowBlockOptions {
                    MaxDegreeOfParallelism = config.ProcessingTasksCount
               };

               //количество одновременно записываемых файлов
               ExecutionDataflowBlockOptions maxWriting = new ExecutionDataflowBlockOptions
               {
                    MaxDegreeOfParallelism = config.WritingTasksCount
               };

               TransformBlock<string, string> readingBlock =
                    new TransformBlock<string, string>(fileName => reader.ReadAsync(fileName), maxReading);

               TransformBlock<string, TestClassStructure> producingBlock =
                    new TransformBlock<string, TestClassStructure>(sourceCode => Produce(sourceCode), maxProcessing);

               ActionBlock<TestClassStructure> writingBlock = 
                    new ActionBlock<TestClassStructure>(generatedClass => writer.WriteAsync(generatedClass), maxWriting);

               readingBlock.LinkTo(producingBlock, linkOptions);
               producingBlock.LinkTo(writingBlock, linkOptions);

               foreach (string path in source)
               {
                    await readingBlock.SendAsync(path);
               }

               //сообщение о завершении считываемых файлов
               readingBlock.Complete();

               await writingBlock.Completion;
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
