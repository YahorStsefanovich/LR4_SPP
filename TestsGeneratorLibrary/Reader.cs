using System.IO;
using System.Threading.Tasks;

namespace TestsGeneratorLibrary
{
     public class Reader
     {
          public async Task<string> ReadAsync(string path)
          {
               using (StreamReader streamReader = new StreamReader(path))
               {
                    return await streamReader.ReadToEndAsync();
               }
          }
     }
}
