using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace kwd.CoreUtil.FileSystem
{
    /// <summary>
    /// Extensions for <see cref="FileInfo"/> to include missing function from <see cref="File"/>.
    /// </summary>
    public static class FromFileExtensions
    {
        ///<summary>
        /// See <see cref="File.AppendAllLines(string, IEnumerable{string})"/> <br />
        /// Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
        /// </summary>
        public static void AppendAllLines(this FileInfo fileInfo, IEnumerable<string> contents)
            => File.AppendAllLines(fileInfo.FullName, contents);

        /// <inheritdoc cref="AppendAllLines(FileInfo,IEnumerable{string})"/>
        public static void AppendAllLines(this IFileInfo fileInfo, IEnumerable<string> contents)
            => fileInfo.FileSystem.File.AppendAllLines(fileInfo.FullName, contents);

        /// <summary>
        /// See <see cref="File.AppendAllLines(string, IEnumerable{string}, Encoding)"/> <br />
        /// Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
        /// </summary>
        public static void AppendAllLines(this FileInfo fileInfo, IEnumerable<string> contents, Encoding encoding)
            => File.AppendAllLines(fileInfo.FullName, contents, encoding);

        /// <inheritdoc cref="AppendAllLines(FileInfo,IEnumerable{string},Encoding)"/>
        public static void AppendAllLines(this IFileInfo fileInfo, IEnumerable<string> contents, Encoding encoding)
            => fileInfo.FileSystem.File.AppendAllLines(fileInfo.FullName, contents, encoding);

        /// <summary>
        /// See <see cref="File.AppendAllLinesAsync(string, IEnumerable{string}, CancellationToken)"/>
        /// </summary>
        public static Task AppendAllLinesAsync(this FileInfo fileInfo, IEnumerable<string> contents,
            CancellationToken cancellationToken = default)
            => File.AppendAllLinesAsync(fileInfo.FullName, contents, cancellationToken);

        /// <inheritdoc cref="AppendAllLinesAsync(FileInfo,IEnumerable{string},CancellationToken)"/>
        public static Task AppendAllLinesAsync(this IFileInfo fileInfo, IEnumerable<string> contents,
            CancellationToken cancellationToken = default)
            => fileInfo.FileSystem.File.AppendAllLinesAsync(fileInfo.FullName, contents, cancellationToken);

        /// <summary>
        /// See <see cref="File.AppendAllLinesAsync(string, IEnumerable{string}, Encoding, CancellationToken)"/>
        /// </summary>
        public static Task AppendAllLinesAsync(this FileInfo fileInfo, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.AppendAllLinesAsync(fileInfo.FullName, contents, encoding, cancellationToken);

        /// <inheritdoc cref="AppendAllLinesAsync(FileInfo,IEnumerable{string}, CancellationToken, Encoding)"/>
        public static Task AppendAllLinesAsync(this IFileInfo fileInfo, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = default) 
            => fileInfo.FileSystem.File.AppendAllLinesAsync(fileInfo.FullName, contents, encoding, cancellationToken);

        /// <summary>
        /// See <see cref="File.AppendAllText(string, string)"/> <br />
        /// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
        /// </summary>
        public static void AppendAllText(this FileInfo fileInfo, string contents) =>
            File.AppendAllText(fileInfo.FullName, contents);

        /// <inheritdoc cref="AppendAllText(FileInfo,string)"/>
        public static void AppendAllText(this IFileInfo fileInfo, string contents) =>
            fileInfo.FileSystem.File.AppendAllText(fileInfo.FullName, contents);

        /// <summary>
        /// See <see cref="File.AppendAllText(string, string, Encoding)"/> <br />
        /// Appends the specified string to the file, creating the file if it does not already exist.
        /// </summary>
        public static void AppendAllText(this FileInfo fileInfo, string contents, Encoding encoding) =>
            File.AppendAllText(fileInfo.FullName, contents, encoding);

        /// <inheritdoc cref="AppendAllText(FileInfo,string,Encoding)"/>
        public static void AppendAllText(this IFileInfo fileInfo, string contents, Encoding encoding) =>
            fileInfo.FileSystem.File.AppendAllText(fileInfo.FullName, contents, encoding);

        /// <summary>
        /// See <see cref="File.AppendAllTextAsync(string,string,System.Threading.CancellationToken)"/>
        /// </summary>
        public static Task AppendAllTextAsync(this FileInfo fileInfo, string contents,
            CancellationToken cancellationToken = default) =>
            File.AppendAllTextAsync(fileInfo.FullName, contents, cancellationToken);

        /// <inheritdoc cref="AppendAllTextAsync(FileInfo,string,CancellationToken)"/>
        public static Task AppendAllTextAsync(this IFileInfo fileInfo, string contents,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.AppendAllTextAsync(fileInfo.FullName, contents, cancellationToken);

        /// <summary>
        /// See <see cref="File.AppendAllTextAsync(string, string, Encoding, CancellationToken)"/>
        /// </summary>
        public static Task AppendAllTextAsync(this FileInfo fileInfo, string contents, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.AppendAllTextAsync(fileInfo.FullName, contents, encoding, cancellationToken);

        /// <inheritdoc cref="AppendAllTextAsync(FileInfo,string,Encoding,CancellationToken)"/>
        public static Task AppendAllTextAsync(this IFileInfo fileInfo, string contents, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.AppendAllTextAsync(fileInfo.FullName, contents, encoding, cancellationToken);
        
        /// <summary>
        /// See <see cref="File.AppendText(string)"/> <br />
        /// Creates a <see cref="T:System.IO.StreamWriter"></see> that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist.
        /// </summary>
        public static StreamWriter AppendText(this FileInfo fileInfo) =>
            File.AppendText(fileInfo.FullName);

        /// <inheritdoc cref="AppendText(FileInfo)"/>
        public static StreamWriter AppendText(this IFileInfo fileInfo) =>
            fileInfo.FileSystem.File.AppendText(fileInfo.FullName);

        /// <summary>
        /// See <see cref="File.ReadAllBytes(string)"/> <br />
        /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.</summary>
        public static byte[] ReadAllBytes(this FileInfo fileInfo) =>
            File.ReadAllBytes(fileInfo.FullName);

        /// <inheritdoc cref="ReadAllBytes(FileInfo)"/>
        public static byte[] ReadAllBytes(this IFileInfo fileInfo) =>
            fileInfo.FileSystem.File.ReadAllBytes(fileInfo.FullName);

        /// <summary>
        /// See <see cref="File.ReadAllBytesAsync(string, CancellationToken)"/>
        /// </summary>
        public static Task<byte[]> ReadAllBytesAsync(this FileInfo fileInfo,
            CancellationToken cancellationToken = default) =>
            File.ReadAllBytesAsync(fileInfo.FullName, cancellationToken);

        /// <inheritdoc cref="ReadAllBytesAsync(FileInfo,CancellationToken)"/>
        public static Task<byte[]> ReadAllBytesAsync(this IFileInfo fileInfo,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.ReadAllBytesAsync(fileInfo.FullName, cancellationToken);

        /// <summary>
        /// See <see cref="File.ReadAllLines(string)"/> <br />
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        public static string[] ReadAllLines(this FileInfo fileInfo) =>
            File.ReadAllLines(fileInfo.FullName);

        /// <inheritdoc cref="ReadAllLines(FileInfo)"/>
        public static string[] ReadAllLines(this IFileInfo fileInfo) =>
            fileInfo.FileSystem.File.ReadAllLines(fileInfo.FullName);

        /// <summary>
        /// See <see cref="File.ReadAllLines(string, Encoding)"/> <br />
        /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        public static string[] ReadAllLines(this FileInfo fileInfo, Encoding encoding) =>
            File.ReadAllLines(fileInfo.FullName, encoding);

        /// <inheritdoc cref="ReadAllLines(FileInfo, Encoding)"/>
        public static string[] ReadAllLines(this IFileInfo fileInfo, Encoding encoding) =>
            fileInfo.FileSystem.File.ReadAllLines(fileInfo.FullName, encoding);

        /// <summary>
        /// See <see cref="File.ReadAllLinesAsync(string, CancellationToken)"/>
        /// </summary>
        public static Task<string[]> ReadAllLinesAsync(this FileInfo fileInfo,
            CancellationToken cancellationToken = default) =>
            File.ReadAllLinesAsync(fileInfo.FullName, cancellationToken);

        /// <inheritdoc cref="ReadAllLinesAsync(FileInfo,CancellationToken)"/>
        public static Task<string[]> ReadAllLinesAsync(this IFileInfo fileInfo,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.ReadAllLinesAsync(fileInfo.FullName, cancellationToken);

        /// <summary>
        /// See <see cref="File.ReadAllLinesAsync(string, Encoding, CancellationToken)"/>
        /// </summary>
        public static Task<string[]> ReadAllLinesAsync(this FileInfo fileInfo, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.ReadAllLinesAsync(fileInfo.FullName, encoding, cancellationToken);

        /// <inheritdoc cref="ReadAllLinesAsync(FileInfo, Encoding, CancellationToken)"/>
        public static Task<string[]> ReadAllLinesAsync(this IFileInfo fileInfo, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.ReadAllLinesAsync(fileInfo.FullName, encoding, cancellationToken);

        /// <summary>
        /// See <see cref="File.ReadAllText(string)"/> <br />
        /// Opens a text file, reads all lines of the file, and then closes the file.
        /// </summary>
        public static string ReadAllText(this FileInfo fileInfo) =>
            File.ReadAllText(fileInfo.FullName);

        /// <inheritdoc cref="ReadAllText(FileInfo)"/>
        public static string ReadAllText(this IFileInfo fileInfo) =>
            fileInfo.FileSystem.File.ReadAllText(fileInfo.FullName);

        /// <summary>
        /// See <see cref="File.ReadAllText(string, Encoding)"/> <br />
        /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
        /// </summary>
        public static string ReadAllText(this FileInfo fileInfo, Encoding encoding) =>
            File.ReadAllText(fileInfo.FullName, encoding);

        /// <inheritdoc cref="ReadAllText(FileInfo)"/>
        public static string ReadAllText(this IFileInfo fileInfo, Encoding encoding) =>
            fileInfo.FileSystem.File.ReadAllText(fileInfo.FullName, encoding);

        /// <summary>
        /// See <see cref="File.ReadAllTextAsync(string, CancellationToken)"/>
        /// </summary>
        public static Task<string> ReadAllTextAsync(this FileInfo fileInfo,
            CancellationToken cancellationToken = default) =>
            File.ReadAllTextAsync(fileInfo.FullName, cancellationToken);

        /// <inheritdoc cref="ReadAllTextAsync(FileInfo,CancellationToken)"/>
        public static Task<string> ReadAllTextAsync(this IFileInfo fileInfo,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.ReadAllTextAsync(fileInfo.FullName, cancellationToken);

        /// <summary>
        /// See <see cref="File.ReadAllText(string, Encoding)"/>
        /// </summary>
        public static Task<string> ReadAllTextAsync(this FileInfo fileInfo, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.ReadAllTextAsync(fileInfo.FullName, encoding, cancellationToken);
        
        /// <inheritdoc cref="ReadAllTextAsync(FileInfo,Encoding,CancellationToken)"/>
        public static Task<string> ReadAllTextAsync(this IFileInfo fileInfo, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.ReadAllTextAsync(fileInfo.FullName, encoding, cancellationToken);

        /// <summary>
        /// See <see cref="File.ReadAllLines(string)"/><br/>
        /// Reads the lines of a file.
        /// </summary>
        public static IEnumerable<string> ReadLines(this FileInfo fileInfo) =>
            File.ReadLines(fileInfo.FullName);

        /// <inheritdoc cref="ReadLines(FileInfo)"/>
        public static IEnumerable<string> ReadLines(this IFileInfo fileInfo) =>
            fileInfo.FileSystem.File.ReadLines(fileInfo.FullName);

        /// <summary>
        /// See <see cref="File.ReadAllLines(string, Encoding)"/> <br/>
        /// Read the lines of a file that has a specified encoding.
        /// </summary>
        public static IEnumerable<string> ReadLines(this FileInfo fileInfo, Encoding encoding) =>
            File.ReadLines(fileInfo.FullName, encoding);

        /// <inheritdoc cref="ReadLines(FileInfo,Encoding)"/>
        public static IEnumerable<string> ReadLines(this IFileInfo fileInfo, Encoding encoding) =>
            fileInfo.FileSystem.File.ReadLines(fileInfo.FullName, encoding);

        /// <summary>
        /// See <see cref="File.WriteAllBytes(string, byte[])"/> <br />
        /// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        public static void WriteAllBytes(this FileInfo fileInfo, byte[] bytes) =>
            File.WriteAllBytes(fileInfo.FullName, bytes);

        /// <inheritdoc cref="WriteAllBytes(FileInfo,byte[])"/>
        public static void WriteAllBytes(this IFileInfo fileInfo, byte[] bytes) =>
            fileInfo.FileSystem.File.WriteAllBytes(fileInfo.FullName, bytes);

        /// <summary>
        /// See <see cref="File.WriteAllBytesAsync(string, byte[], CancellationToken)"/>
        /// </summary>
        public static Task WriteAllBytesAsync(this FileInfo fileInfo, byte[] bytes,
            CancellationToken cancellationToken = default) =>
            File.WriteAllBytesAsync(fileInfo.FullName, bytes, cancellationToken);

        /// <inheritdoc cref="WriteAllBytesAsync(FileInfo,byte[], CancellationToken)"/>
        public static Task WriteAllBytesAsync(this IFileInfo fileInfo, byte[] bytes,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.WriteAllBytesAsync(fileInfo.FullName, bytes, cancellationToken);

        /// <summary>
        /// See <see cref="File.WriteAllLines(string,string[])"/> <br />
        /// Creates a new file, write the specified string array to the file, and then closes the file.
        /// </summary>
        public static void WriteAllLines(this FileInfo fileInfo, string[] contents) =>
            File.WriteAllLines(fileInfo.FullName, contents);

        /// <inheritdoc cref="WriteAllLines(FileInfo,string[])"/>
        public static void WriteAllLines(this IFileInfo fileInfo, string[] contents) =>
            fileInfo.FileSystem.File.WriteAllLines(fileInfo.FullName, contents);

        /// <summary>
        /// See <see cref="File.WriteAllLines(string,IEnumerable{string})"/> <br />
        /// Creates a new file, writes a collection of strings to the file, and then closes the file.
        /// </summary>
        public static void WriteAllLines(this FileInfo fileInfo, IEnumerable<string> contents) =>
            File.WriteAllLines(fileInfo.FullName, contents);

        /// <inheritdoc cref="WriteAllLines(FileInfo,string[])"/>
        public static void WriteAllLines(this IFileInfo fileInfo, IEnumerable<string> contents) =>
            fileInfo.FileSystem.File.WriteAllLines(fileInfo.FullName, contents);
        
        /// <summary>
        /// See <see cref="File.WriteAllLines(string,string[], Encoding)"/> <br />
        /// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
        /// </summary>
        public static void WriteAllLines(this FileInfo fileInfo, string[] contents, Encoding encoding) =>
            File.WriteAllLines(fileInfo.FullName, contents, encoding);

        /// <inheritdoc cref="WriteAllLines(FileInfo,string[],Encoding)"/>
        public static void WriteAllLines(this IFileInfo fileInfo, string[] contents, Encoding encoding) =>
            fileInfo.FileSystem.File.WriteAllLines(fileInfo.FullName, contents, encoding);

        /// <summary>
        /// See <see cref="File.WriteAllLines(string, string[], Encoding)"/> <br />
        /// Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.
        /// </summary>
        public static void WriteAllLines(this FileInfo fileInfo, IEnumerable<string> contents, Encoding encoding) =>
            File.WriteAllLines(fileInfo.FullName, contents, encoding);

        /// <inheritdoc cref="WriteAllLines(FileInfo,string[],Encoding)"/>
        public static void WriteAllLines(this IFileInfo fileInfo, IEnumerable<string> contents, Encoding encoding) =>
            fileInfo.FileSystem.File.WriteAllLines(fileInfo.FullName, contents, encoding);

        /// <summary>
        /// See <see cref="File.WriteAllLinesAsync(string, IEnumerable{string}, CancellationToken)"/>
        /// </summary>
        public static Task WriteAllLinesAsync(this FileInfo fileInfo, IEnumerable<string> contents,
            CancellationToken cancellationToken = default) =>
            File.WriteAllLinesAsync(fileInfo.FullName, contents, cancellationToken);

        /// <inheritdoc cref="WriteAllLinesAsync(FileInfo,IEnumerable{string},CancellationToken)"/>
        public static Task WriteAllLinesAsync(this IFileInfo fileInfo, IEnumerable<string> contents,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.WriteAllLinesAsync(fileInfo.FullName, contents, cancellationToken);

        /// <summary>
        /// See <see cref="File.WriteAllLinesAsync(string, IEnumerable{string}, Encoding, CancellationToken)"/>
        /// </summary>
        public static Task WriteAllLinesAsync(this FileInfo fileInfo, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.WriteAllLinesAsync(fileInfo.FullName, contents, encoding, cancellationToken);

        /// <inheritdoc cref="WriteAllLinesAsync(FileInfo,IEnumerable{string},Encoding,CancellationToken)"/>
        public static Task WriteAllLinesAsync(this IFileInfo fileInfo, IEnumerable<string> contents, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.WriteAllLinesAsync(fileInfo.FullName, contents, encoding, cancellationToken);

        /// <summary>
        /// See <see cref="File.WriteAllText(string,string)"/> <br />
        /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        public static void WriteAllText(this FileInfo fileInfo, string contents) =>
            File.WriteAllText(fileInfo.FullName, contents);

        /// <inheritdoc cref="WriteAllText(FileInfo,string)"/>
        public static void WriteAllText(this IFileInfo fileInfo, string contents) =>
            fileInfo.FileSystem.File.WriteAllText(fileInfo.FullName, contents);

        /// <summary>
        /// See <see cref="File.WriteAllText(string,string, Encoding)"/> <br />
        /// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.
        /// </summary>
        public static void WriteAllText(this FileInfo fileInfo, string contents, Encoding encoding) =>
            File.WriteAllText(fileInfo.FullName, contents, encoding);

        /// <inheritdoc cref="WriteAllText(FileInfo,string,Encoding)"/>
        public static void WriteAllText(this IFileInfo fileInfo, string contents, Encoding encoding) =>
            File.WriteAllText(fileInfo.FullName, contents, encoding);

        /// <summary>
        /// See <see cref="File.WriteAllTextAsync(string, string, CancellationToken)"/>
        /// </summary>
        public static Task WriteAllTextAsync(this FileInfo fileInfo, string contents,
            CancellationToken cancellationToken = default) =>
            File.WriteAllTextAsync(fileInfo.FullName, contents, cancellationToken);

        /// <inheritdoc cref="WriteAllTextAsync(FileInfo,string,CancellationToken)"/>
        public static Task WriteAllTextAsync(this IFileInfo fileInfo, string contents,
            CancellationToken cancellationToken = default) =>
            fileInfo.FileSystem.File.WriteAllTextAsync(fileInfo.FullName, contents, cancellationToken);

        /// <summary>
        /// See <see cref="File.WriteAllTextAsync(string,string, Encoding, CancellationToken)"/>
        /// </summary>
        public static Task WriteAllTextAsync(this FileInfo fileInfo, string contents, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.WriteAllTextAsync(fileInfo.FullName, contents, encoding, cancellationToken);

        /// <inheritdoc cref="WriteAllTextAsync(FileInfo,string,Encoding,CancellationToken)"/>
        public static Task WriteAllTextAsync(this IFileInfo fileInfo, string contents, Encoding encoding,
            CancellationToken cancellationToken = default) =>
            File.WriteAllTextAsync(fileInfo.FullName, contents, encoding, cancellationToken);
    }
}