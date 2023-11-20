using System.IO;
using System.Threading.Tasks;

namespace kwld.CoreUtil.Streams
{
    /// <summary>
    /// Sugar for text writer 
    /// </summary>
    public static class TextWriterExtensions
    {
        /// <summary>
        /// Write multiple lines to text writer.
        /// </summary>
        public static TextWriter WriteLines(this TextWriter self, params string[] lines)
        {
            foreach (var line in lines)
            {
                self.WriteLine(line);    
            }

            return self;
        }

        /// <summary>
        /// Write multiple lines to text writer.
        /// </summary>
        public static async Task<TextWriter> WriteLinesAsync(this TextWriter self, 
            params string[] lines)
        {
            foreach (var line in lines)
            {
                await self.WriteLineAsync(line);
            }

            return self;
        }
    }
}